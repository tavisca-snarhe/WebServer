namespace WebServer
{
    public interface IApp
    {
        void HandleRequest(HTTPContext context);
    }
}