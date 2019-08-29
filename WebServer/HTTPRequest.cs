using System;
using System.Net.Sockets;
using System.Text;

namespace WebServer
{
    public class HTTPRequest
    {
        //POST /index.html HTTP/1.1
        //Content-Type: application/json
        //User-Agent: PostmanRuntime/7.15.2
        //Accept: */*
        //Cache-Control: no-cache
        //Postman-Token: 0810745b-2222-4c0f-b6c9-49f206cabe04
        //Host: myserver.com
        //Accept-Encoding: gzip, deflate
        //Content-Length: 17
        //Connection: keep-alive

        //{
        //        "year": 2004
        //}

        public string AbsoluteURL;
        public string Method;
        public string ContentType;
        public string Host;
        public string Body;

        public HTTPRequest(Socket socket)
        {
            Parse(socket);
            Console.WriteLine("Done Parsing in HTTPRequest");
            Console.WriteLine("Host = " + Host);
            Console.WriteLine("AbsoluteURL = " + AbsoluteURL);
            Console.WriteLine("Method = " + Method);
            Console.WriteLine("ContentType = " + ContentType);
            Console.WriteLine("Body = " + Body);
        }

        public void Parse(Socket socket)
        {
            string request = ExtractRequest(socket);
            Method = GetProperty(request, "", " ");
            Host = GetProperty(request, "Host: ", "\n");
            Host = Host.Substring(0, Host.IndexOf((char)13));
            AbsoluteURL = GetProperty(request, " ", " ");
            ContentType = GetProperty(request, "Content-Type: ", "\n");
            Body = "{" + GetProperty(request, "{", "}") + "}";
        }

        private string GetProperty(string request, string property, string end)
        {
            if (request.Length == 0 || request.IndexOf(property) == -1)
                return "";
            int pFrom = request.IndexOf(property) + property.Length;
            int pTo = request.IndexOf(end, pFrom);
            if ((pFrom == 0 && pTo == 0) || (pFrom > pTo))
                return "";
            return request.Substring(pFrom, pTo - pFrom);
        }

        private static string ExtractRequest(Socket socket)
        {
            NetworkStream communicationChannel = new NetworkStream(socket);
            byte[] byteData = new byte[1024];
            int byteDataCount = communicationChannel.Read(byteData, 0, byteData.Length);
            return Encoding.ASCII.GetString(byteData, 0, byteDataCount);
        }
    }
}