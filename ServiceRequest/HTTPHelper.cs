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
using ServiceRequest;

namespace DoYourJob
{
    class HTTPHelper
    {
        HttpClient client;
        string BaseUrl = "https://5w3bsgqwxg.execute-api.us-east-1.amazonaws.com/prod/rekog";

        public HTTPHelper()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Add("x-api-key", "0pVrHkMC5ZmwE7vCqA1N6a4qTdfAsXy1it0WJ6Sj");
        }

        //Query DB for one Staff
        public string RefreshDataAsync()
        {
            var uri = new Uri(string.Format(BaseUrl, string.Empty));
            var response = client.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsStringAsync().Result;
            return null;
        }

        public void AddStaff(Staff staff)
        {
            var endpoint = BaseUrl + "?name=" + staff.StaffName + "&ServicesJson=" + staff.ServicesJson;
            var uri = new Uri(endpoint);
            var response = client.GetAsync(uri).Result;
            var result = response.Content.ReadAsStringAsync().Result;
           
        }

        public void AddStaff(string name, string requests)
        {
            Staff staff = new Staff(name, requests);
            AddStaff(staff);
        }


        public List<Staff> QueryStaffs()
        {
            var uri = new Uri(string.Format(BaseUrl, string.Empty));
            var response = client.GetAsync(uri).Result;
            var text = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<List<Staff>>(text);
            return null;
        }

        public List<ServiceRequest.Service> GetMyServices(string staffName)
        {
            var uri = new Uri(BaseUrl + "?name=" + staffName);
            var response = client.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<List<ServiceRequest.Service>>(response.Content.ReadAsStringAsync().Result);
            return null;
        }
    }
}
