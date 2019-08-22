using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace WebServer
{
    public class RequestListener
    {
        private Socket _serverSocket;
        private IPEndPoint _localEndPoint;

        public RequestListener()
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _localEndPoint = new IPEndPoint(IPAddress.Any, 80);
            _serverSocket.Bind(_localEndPoint);
        }

        public void Start()
        {
            _serverSocket.Listen(10);
            Console.WriteLine("Server listening on port:" + 80);
        }

        public Socket GetSocket()
        {
            Task<Socket> getSocket = Task.Run(() => {
                                            while (true)
                                            {
                                                Socket senderSocket = _serverSocket.Accept();
                                                Console.WriteLine("connected");
                                                return senderSocket;
                                            }
                                       });
            
            return getSocket.GetAwaiter().GetResult();
        }

        public void Stop()
        {
            _serverSocket.Shutdown(SocketShutdown.Both);
            _serverSocket.Close();
        }
    }
}
