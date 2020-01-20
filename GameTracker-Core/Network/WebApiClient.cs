using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace GameTracker_Core.Network
{
    class WebApiClient
    {
        private static readonly string URL = "";
        private static string UrlParameters = "";

        private static HttpClient client = new HttpClient();

        static WebApiClient() {
            client.BaseAddress = new Uri(URL);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            }

        public static HttpResponseMessage PostToServer(string json)
        {
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            return client.PostAsync(UrlParameters, httpContent).Result;
        }
    }
}
