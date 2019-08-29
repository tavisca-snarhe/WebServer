using System.Text;

namespace WebServer
{
    public class HTTPResponse
    {
        public HTTPHeader Header;
        public string Response;

        public HTTPResponse(string data, int statusCode, string status, string contentType)
        {
            Header = new HTTPHeader(statusCode, status, contentType);
            Response = data;
        }

        public byte[] GetBytes()
        {
            return Encoding.ASCII.GetBytes(Header.GetString() + Response);
        }

    }
}
