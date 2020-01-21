using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace GameTracker_Core.Network
{
    class WebApiClient
    {

        private static readonly HttpClient client = new HttpClient();

        static WebApiClient() {
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            }

        public static HttpResponseMessage PostGamesToServer(string json, string token, string url)
        {
            client.BaseAddress = new Uri(url);
            var UrlParameters = "/api/agent/" + token;
            return PostToServer(json,UrlParameters);
        }
        private static HttpResponseMessage PostToServer(string json,string UrlParameters)
        {
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            return client.PostAsync(UrlParameters, httpContent).Result;
        }
    }
}
