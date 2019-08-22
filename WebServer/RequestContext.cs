using System;
using System.Net.Sockets;
using System.Text;

namespace WebServer
{
    public class RequestContext
    {
        public Socket Socket;
        public HTTPRequest HttpRequest;

        public RequestContext(Socket socket)
        {
            Socket = socket;
            HttpRequest = new HTTPRequest(Socket);
        }
    }
}