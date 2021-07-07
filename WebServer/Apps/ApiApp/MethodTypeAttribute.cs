using System;

namespace WebServer
{
    internal class MethodTypeAttribute : Attribute
    {
        public MethodTypeAttribute(string HTTPMethod, string Route)
        {
            this.HTTPMethod = HTTPMethod;
            this.Route = Route;
        }

        public string HTTPMethod { get; set; }
        public string Route { get; set; }
    }
}