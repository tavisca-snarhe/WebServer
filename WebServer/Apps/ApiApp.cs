using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace WebServer
{
    internal class ApiApp : IApp
    {
        public void HandleRequest(HttpListenerContext context)
        {
            Console.WriteLine(context.Request.RawUrl);
            Console.WriteLine(context.Request.HttpMethod);

            if (context.Request.RawUrl == "/year" && context.Request.HttpMethod == "POST")
            {
                var data_text = new StreamReader(context.Request.InputStream,
                                                 context.Request.ContentEncoding)
                                                 .ReadToEnd();
                var json =  System.Web.HttpUtility.UrlDecode(data_text);
                JObject requestBody = JObject.Parse(json);
                Console.WriteLine(requestBody);
                string year = (string) requestBody.SelectToken("year");

                if (year!=null)
                {
                    string value = Int32.Parse(year) % 4 == 0 ? "true" : "false";
                    JObject responseJSON = JObject.Parse(@"{'IsLeap': '" + value + @"'}");
                    Console.WriteLine(responseJSON);
                    byte[] buf = Encoding.UTF8.GetBytes(responseJSON.ToString());
                    context.Response.ContentLength64 = buf.Length;
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.OutputStream.Write(buf, 0, buf.Length);
                }
                else
                {
                    byte[] buf = Encoding.UTF8.GetBytes("missing required data in request.");
                    context.Response.ContentLength64 = buf.Length;
                    context.Response.OutputStream.Write(buf, 0, buf.Length);
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }
            else
            {
                byte[] buf = Encoding.UTF8.GetBytes("invalid request.");
                context.Response.ContentLength64 = buf.Length;
                context.Response.OutputStream.Write(buf, 0, buf.Length);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            
            context.Response.OutputStream.Close();
        }
    }
}