using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DoYourJob
{
    class HTTPHelper
    {
        HttpClient client;
        string RestUrl = "https://5w3bsgqwxg.execute-api.us-east-1.amazonaws.com/prod/rekog";

        public HTTPHelper()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Add("x-api-key", "0pVrHkMC5ZmwE7vCqA1N6a4qTdfAsXy1it0WJ6Sj");
        }

        //Query DB for one Staff
        public string RefreshDataAsync()
        {
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var response = client.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsStringAsync().Result;
            return null;
        }
    }
}