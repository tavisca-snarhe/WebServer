using System.Collections.Generic;

namespace WebServer
{
    public class ServerConfig
    {
        private Dictionary<string, IApp> _apps = new Dictionary<string, IApp>();

        public void AddNewApp(string domain, IApp app)
        {
            _apps.Add(domain, app);
        }

        public IApp GetApp(string domain)
        {
            if (_apps.ContainsKey(domain))
                return _apps[domain];

            throw new InvalidDomainException();
        }
    }
}