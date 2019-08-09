using System.Net;

namespace WebServer
{
    public class Dispatcher
    {
        private ServerConfig _serverConfig;
        public Dispatcher(ServerConfig configs)
        {
            _serverConfig = configs;
        }
        public IApp GetApp(string UserHostName)
        {
            return _serverConfig.GetApp(UserHostName);
        }
    }
}