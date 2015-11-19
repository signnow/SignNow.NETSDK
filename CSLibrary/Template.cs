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
    public class Template
    {

        /// <summary>
        /// Create a Template by Flattening an Existing Document
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <param name="DocumentId">The ID of the Document to make a Template</param>
        /// <param name="DocumentName">New Template Name</param>
        /// <returns>The ID of the new Template</returns>
        public static JObject Create(string AccessToken, string DocumentId, string DocumentName = "")
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            var request = new RestRequest("/template", Method.POST)
                .AddHeader("Accept", "application/json")
                .AddHeader("Authorization", "Bearer " + AccessToken);

            dynamic reqObj;

            if (DocumentName != "")
            {
                reqObj = new { document_id = DocumentId, document_name = DocumentName };
            }
            else
            {
                reqObj = new { document_id = DocumentId };
            }

            request.RequestFormat = DataFormat.Json;
            request.AddBody(reqObj);

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
        /// Create a New Document by Copying a Flattened Template
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <param name="DocumentId"></param>
        /// <param name="DocumentName"></param>
        /// <returns>New Document ID and Name</returns>
        public static JObject Copy(string AccessToken, string DocumentId, string DocumentName = "")
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            var request = new RestRequest("/template/" + DocumentId + "/copy", Method.POST)
                .AddHeader("Accept", "application/json")
                .AddHeader("Authorization", "Bearer " + AccessToken);

            if (DocumentName != "")
            {
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new { document_name = DocumentName });
            }            

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
