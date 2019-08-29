using Newtonsoft.Json.Linq;
using System;

namespace WebServer
{
    internal class Routes
    {
        [MethodType("POST", "/year")]
        public JObject Year(JObject requestBody)
        {
            string year = (string)requestBody.SelectToken("year");
            if (year == null)
                return new JObject(new JProperty("Error", "Missing Required field."));
            if (Int32.Parse(year) % 4 == 0)
                return new JObject(new JProperty("year", true));
            else
                return new JObject(new JProperty("year", false));
        }

        [MethodType("POST", "/age")]
        public JObject Age(JObject requestBody)
        {
            string age = (string)requestBody.SelectToken("age");
            if (age == null)
                return new JObject(new JProperty("Error", "Missing Required field."));
            if (Int32.Parse(age) > 18)
                return new JObject(new JProperty("Adult", true));
            else
                return new JObject(new JProperty("Adult", false));
        }


    }
}