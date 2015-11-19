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
    public class Folder
    {
        /// <summary>
        /// Gets a List of Folders
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <returns>Folders, Document & Template Counts</returns>
        public static JObject List(string AccessToken)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            var request = new RestRequest("/folder", Method.GET)
                .AddHeader("Accept", "application/json")
                .AddHeader("Authorization", "Bearer " + AccessToken);

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <param name="FolderId">ID of the Folder to Get</param>
        /// <param name="Params">Option Filter and Sort By Params</param>
        /// <returns>List of documents in the folder.</returns>
        public static JObject Get(string AccessToken, string FolderId, string Params = "")
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            string qsParams = (Params != "") ? "?" + Params : "";

            var request = new RestRequest("/folder/" + FolderId + qsParams, Method.GET)
                .AddHeader("Accept", "application/json")
                .AddHeader("Authorization", "Bearer " + AccessToken);

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
