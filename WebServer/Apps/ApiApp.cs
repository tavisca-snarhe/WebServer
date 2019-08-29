using Newtonsoft.Json.Linq;
using System;

namespace WebServer
{
    internal class ApiApp : IApp
    {

        Routes routes;

        public ApiApp()
        {
            routes = new Routes();
        }

        public void HandleRequest(HTTPContext context)
        {
            string HttpMethod = context.Request.Method;
            string Route = context.Request.AbsoluteURL;
            string RouteHandler = GetRouteHandler(HttpMethod, Route);

            if(RouteHandler != "No Method Found")
            {
                JObject requestBody = JObject.Parse(context.Request.Body);
                JObject result = (JObject)routes.GetType().GetMethod(RouteHandler).Invoke(routes, new object[] { requestBody });
                context.Response = new HTTPResponse(result.ToString(), 200, "OK", "application/json");
                context.SendResponse();
            }
            else
            {
                context.Response = new HTTPResponse("Error", 500, "NOT_FOUND", "application/json");
                context.SendResponse();
            }

        }

        private string GetRouteHandler(string HTTPMethod, string Route)
        {
            foreach (var prop in typeof(Routes).GetMethods())
            {
                var attrs = (MethodTypeAttribute[])prop.GetCustomAttributes (typeof(MethodTypeAttribute), false);
                foreach (var attr in attrs)
                {

                    if (attr.HTTPMethod == HTTPMethod && Route == attr.Route)
                        return prop.Name;
                }
            }
            return "No Method Found";
        }
    }
}