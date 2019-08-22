using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace WebServer
{
    public class StaticApp : IApp
    {
        private string _rootDirectory;

        public StaticApp(string rootDirectory)
        {
            _rootDirectory = rootDirectory;
        }

        public void HandleRequest(HttpListenerContext context)
        {
            var request = context.Request;
            string filename = _rootDirectory + request.RawUrl.ToString();
            Console.WriteLine(_rootDirectory + "\n");
            Console.WriteLine(filename);

            if (File.Exists(filename))
            {
                Console.WriteLine("exists");
                try
                {
                    Stream input = new FileStream(filename, FileMode.Open);

                    byte[] buffer = new byte[1024 * 32];
                    int nbytes;
                    while ((nbytes = input.Read(buffer, 0, buffer.Length)) > 0)
                        context.Response.OutputStream.Write(buffer, 0, nbytes);

                    input.Close();

                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                }
                catch (Exception)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }

            context.Response.OutputStream.Close();
        }

        public void HandleRequest(Socket context)
        {
        }
    }
}