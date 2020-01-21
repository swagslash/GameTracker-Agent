using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace GameTracker_Core.Network
{
    class WebApiClient
    {
        private static readonly string URL = "http://localhost:8080";
        private static string UrlParameters = "/api/agent/b9eebd97-6244-488a-88a8-1592de03cad7";

        private static readonly HttpClient client = new HttpClient();

        static WebApiClient() {
            client.BaseAddress = new Uri(URL);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            }

        public static HttpResponseMessage PostGamesToServer(string json, string token)
        {
            UrlParameters = "/api/agent/" + token;
            return PostToServer(json);
        }
        private static HttpResponseMessage PostToServer(string json)
        {
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            return client.PostAsync(UrlParameters, httpContent).Result;
        }
    }
}
