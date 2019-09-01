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
        }

        public HTTPContext GetContext()
        {
            Socket senderSocket = _serverSocket.Accept();
            HTTPContext context = new HTTPContext(senderSocket);
            return context;
        }

        public void Stop()
        {
            _serverSocket.Shutdown(SocketShutdown.Both);
            _serverSocket.Close();
        }
    }
}
