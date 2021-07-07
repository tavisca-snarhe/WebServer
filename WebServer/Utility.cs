using System.Net;

namespace WebServer
{
    public static class Utility
    {
        public static string GetMyIPv4()
        {
            string IPv4 = Dns.GetHostEntry(Dns.GetHostName()).AddressList[2].ToString();
            return IPv4;
        }
    }
}
