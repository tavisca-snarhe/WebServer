using Newtonsoft.Json.Linq;
using System;

namespace WebServer
{
    public class ApiApp : IApp
    {
        Routes routes;

        public ApiApp()
        {
            routes = new Routes();
        }

        public void HandleRequest(HTTPContext context)
        {
            string RouteHandler = TryGetRouteHandler(context.Request.Method, context.Request.AbsoluteURL, out bool found);
            
            if (found)
            {
                try
                {
                    JObject requestBody = JObject.Parse(context.Request.Body);
                    JObject result = (JObject)routes.GetType().GetMethod(RouteHandler).Invoke(routes, new object[] { requestBody });
                    context.Response = new HTTPResponse(result.ToString(), 200, "OK", "application/json");
                }
                catch (Exception)
                {
                    JObject result = new JObject(new JProperty("Message", "Inalid JSON in request body."));
                    context.Response = new HTTPResponse(result.ToString(), 500, "INVALID_REQUEST", "application/json");
                }
            }
            else
            {
                JObject result = new JObject(new JProperty("Message", "Method not found"));
                context.Response = new HTTPResponse(result.ToString(), 404, "NOT_FOUND", "application/json");
            }

            context.SendResponse();
        }

        private string TryGetRouteHandler(string HTTPMethod, string Route, out bool found)
        {
            foreach (var prop in typeof(Routes).GetMethods())
            {
                var attrs = (MethodTypeAttribute[])prop.GetCustomAttributes (typeof(MethodTypeAttribute), false);
                foreach (var attr in attrs)
                    if (attr.HTTPMethod == HTTPMethod && Route == attr.Route)
                    {
                        found = true;
                        return prop.Name;
                    }
            }
            found = false;
            return "NOT_FOUND";
        }
    }
}