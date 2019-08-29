using System.Text;

namespace WebServer
{
    public class HTTPHeader
    {
        private StringBuilder _header;

        public HTTPHeader(int statusCode=200, string status="OK", string contentType="text/html")
        {
            _header = new StringBuilder();
            _header.AppendLine($"HTTP/1.1 {statusCode} {status}");
            _header.AppendLine($"Content-Type: {contentType}" + ";charset=UTF-8");
            _header.AppendLine();
        }

        public string GetString()
        {
            return _header.ToString();
        }
    }
}