using System;
using System.Net;

namespace WebServer
{
    public class WebServer
    {
        private HttpListener _listener;
        private Dispatcher _dispatcher;
        private ServerConfig _configs;

        public WebServer()
        {
            _listener = new HttpListener();
            _configs = new ServerConfig();
            _dispatcher = new Dispatcher(_configs);

            _listener.Prefixes.Add("http://" + GetMyIP() + "/");
            _listener.Prefixes.Add("http://localhost/");

            _configs.AddNewApp("myserver.com", new StaticApp("C:/Users/snarhe/source/htdocs"));
            _configs.AddNewApp("api.com", new ApiApp());
        }

        private static string GetMyIP()
        {
            string IPv4 = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
            return IPv4;
        }

        private void ProcessRequest(object listenerContext)
        {
            HttpListenerContext context = (HttpListenerContext)listenerContext;
            IApp app = _dispatcher.GetApp(context.Request.UserHostName);
            app.HandleRequest(context);
        }

        public void Start()
        {
            _listener.Start();
            Console.WriteLine("Listening on " + "http://" + GetMyIP() + "/");

            while (true)
            {
                try
                {
                    HttpListenerContext context = _listener.GetContext();
                    Console.WriteLine("Got new request");
                    ProcessRequest(context);
                }
                catch (HttpListenerException) { break; }
                catch (InvalidOperationException) { break; }
            }  
        }

        public void Stop()
        {
            _listener.Stop();
        }

    }
}
