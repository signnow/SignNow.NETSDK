using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CudaSign
{
    public class Link
    {
        public static JObject Create(string AccessToken, string DocumentId)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            var request = new RestRequest("/link", Method.POST)
                .AddHeader("Accept", "application/json")
                .AddHeader("Authorization", "Bearer " + AccessToken);

            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { document_id = DocumentId });

            var response = client.Execute(request);
            
            if (response.StatusCode == HttpStatusCode.OK)
            {
                dynamic results = JsonConvert.DeserializeObject(response.Content);
                return results;
            }
            else
            {
                Console.WriteLine(response.Content.ToString());
                dynamic jsonObject = new JObject();
                jsonObject.error = response.Content.ToString();
                return jsonObject;
            }
        }
    }
}
