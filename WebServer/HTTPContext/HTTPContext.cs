using System.Net.Sockets;

namespace WebServer
{
    public class HTTPContext
    {
        public Socket Socket;
        public HTTPRequest Request;
        public HTTPResponse Response;

        public HTTPContext(Socket socket)
        {
            Socket = socket;
            Request = new HTTPRequest(Socket);
        }

        public void SendResponse()
        {
            if(Response != null)
            {
                Socket.Send(Response.GetBytes());
            }
            CloseConnection();
        }

        public void CloseConnection()
        {
            Socket.Shutdown(SocketShutdown.Both);
            Socket.Close();
        }
    }
}