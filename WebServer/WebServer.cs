using System;

namespace WebServer
{
    public class WebServer
    {
        private HTTPListener _listener;
        private Dispatcher _dispatcher;
        private ServerConfig _configs;

        public WebServer()
        {
            _configs = new ServerConfig();
            _dispatcher = new Dispatcher(_configs);
            _listener = new HTTPListener();
        }

        public void Start()
        {
            _listener.Start();
            Console.WriteLine($"Listening on http://{Utility.GetMyIPv4()}/");
            while (true)
            {
                HTTPContext context = _listener.GetContext();
                Console.WriteLine("Got new request");
                ProcessRequest(context);
            }  
        }

        private void ProcessRequest(HTTPContext context)
        {
            IApp app = _dispatcher.GetApp(context.Request.Host);
            app.HandleRequest(context);
        }

        public void Stop()
        {
            _listener.Stop();
        }
    }
}
