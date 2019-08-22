using System.Net.Sockets;

namespace WebServer
{
    public interface IApp
    {
        void HandleRequest(Socket context);
    }
}