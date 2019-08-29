using System;
using System.IO;
using System.Net;

namespace WebServer
{
    public class StaticApp : IApp
    {
        private FileSystem _fileSystem;

        public StaticApp(string rootDirectory)
        {
            _fileSystem = new FileSystem(rootDirectory);
        }

        public void HandleRequest(HTTPContext context)
        {
            string virtualPath = context.Request.AbsoluteURL;
            string file = _fileSystem.ReadFile(virtualPath);
            context.Response = new HTTPResponse(file, 200, "OK", "text/html");
            context.SendResponse();
        }
    }
}