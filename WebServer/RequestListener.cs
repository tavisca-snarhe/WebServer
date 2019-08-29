using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace WebServer
{
    public class HTTPListener
    {
        private Socket _serverSocket;
        private IPEndPoint _localEndPoint;
        private int _port;

        public HTTPListener()
        {
            _port = 80;
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _localEndPoint = new IPEndPoint(IPAddress.Any, _port);
            _serverSocket.Bind(_localEndPoint);
        }

        public void Start()
        {
            _serverSocket.Listen(10);
            Console.WriteLine($"Server listening on port:{_port}");
        }

        public HTTPContext GetContext()
        {
            Task<HTTPContext> Context= Task.Run(() => {
                                            while (true)
                                            {
                                                Socket senderSocket = _serverSocket.Accept();
                                                Console.WriteLine("connected");
                                                HTTPContext context = new HTTPContext(senderSocket);
                                                Console.WriteLine(context.Request.Method);
                                                return context;
                                            }
                                       });
            return Context.GetAwaiter().GetResult();
        }

        public void Stop()
        {
            _serverSocket.Shutdown(SocketShutdown.Both);
            _serverSocket.Close();
        }
    }
}
