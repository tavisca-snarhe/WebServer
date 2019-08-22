using System;
using System.Net;
using System.Net.Sockets;

namespace WebServer
{

    public class WebServer
    {
        private RequestListener _listener;
        private Dispatcher _dispatcher;
        private ServerConfig _configs;
        private RequestContext _requestContext;

        public WebServer()
        {
            ConfigureServer();
            _listener = new RequestListener();
            _dispatcher = new Dispatcher(_configs);
        }


        private void ConfigureServer()
        {
            _configs = new ServerConfig();
            _configs.AddNewApp("myserver.com", new StaticApp("C:/Users/snarhe/source/htdocs"));
            _configs.AddNewApp("api.com", new ApiApp());
        }
        
        public void Start()
        {
            _listener.Start();
            Console.WriteLine("Listening on " + "http://" + GetMyIP() + "/");

            while (true)
            {
                try
                {
                    Socket context = _listener.GetSocket();
                    Console.WriteLine("Got new request");
                    ProcessRequest(context);
                }
                catch (HttpListenerException) { break; }
                catch (InvalidOperationException) { break; }
            }  
        }

        private void ProcessRequest(object listenerContext)
        {
            Socket context = (Socket)listenerContext;
            _requestContext = new RequestContext(context);
            IApp app = _dispatcher.GetApp(_requestContext.HttpRequest.Host);
            app.HandleRequest(context);
        }

        public void Stop()
        {
            _listener.Stop();
        }
        private static string GetMyIP()
        {
            string IPv4 = Dns.GetHostEntry(Dns.GetHostName()).AddressList[2].ToString();
            return IPv4;
        }
    }
}
