using System.Net;

namespace WebServer
{
    public interface IApp
    {
        void HandleRequest(HttpListenerContext context);
    }
}