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
    public class Document
    {
        /// <summary>
        /// Uploads a File and Creates a Document
        /// Accepted File Types: .doc, .docx, .pdf, .png
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <param name="FilePath">Local Path to the File</param>
        /// <param name="ExtractFields">If set TRUE the document will be checked for special field tags. If any exist they will be converted to fields.</param>
        /// <returns>ID of the document that was created</returns>
        public static JObject Create(string AccessToken, string FilePath, bool ExtractFields = false)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            var path = (ExtractFields) ? "/document/fieldextract" : "/document";

            var request = new RestRequest(path, Method.POST)
                .AddHeader("Accept", "application/json")
                .AddHeader("Authorization", "Bearer " + AccessToken)
                .AddFile("file", Path.GetFullPath(FilePath));

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
        /// Updates an Existing Document
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <param name="DocumentID">Document Id</param>
        /// <param name="DataObj">Data Object (ex. dynamic new { fields = new[] { new { x = 10, y = 10, width = 122... } } }</param>
        /// <returns>Document ID</returns>
        public static JObject Update(string AccessToken, string DocumentId, dynamic DataObj)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            var request = new RestRequest("/document/" + DocumentId, Method.PUT)
                .AddHeader("Accept", "application/json")
                .AddHeader("Authorization", "Bearer " + AccessToken);

                request.RequestFormat = DataFormat.Json;
                request.AddBody(DataObj);

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
        /// Retrieve a Document Resource
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <param name="DocumentId">Document Id</param>
        /// <returns>Document Information, Status, Fields...</returns>
        public static JObject Get(string AccessToken, string DocumentId)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            var request = new RestRequest("/document/" + DocumentId, Method.GET)
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
        /// Deletes an Existing Document
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <param name="DocumentId">Document Id</param>
        /// <returns>{"status":"success"}</returns>
        public static JObject Delete(string AccessToken, string DocumentId)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            var request = new RestRequest("/document/" + DocumentId, Method.DELETE)
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
        /// Downloads a Collapsed Document
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <param name="DocumentId">Document Id</param>
        /// <param name="SaveFilePath">Local Path to Save File</param>
        /// <param name="SaveFileName">File Name without Extension</param>
        /// <returns>Collapsed document in PDF format saved to a the location provided.</returns>
        public static JObject Download(string AccessToken, string DocumentId, string SaveFilePath = "", string SaveFileName = "my-collapsed-document")
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            var request = new RestRequest("/document/" + DocumentId + "/download?type=collapsed", Method.GET)
                .AddHeader("Accept", "application/json")
                .AddHeader("Authorization", "Bearer " + AccessToken);

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var path = (SaveFilePath != "") ? Path.GetDirectoryName(SaveFilePath) + "/" + SaveFileName + ".pdf" : Directory.GetCurrentDirectory() + "/" + SaveFileName + ".pdf";
                client.DownloadData(request).SaveAs(path);
                //dynamic results = JsonConvert.DeserializeObject(response.Content);
                
                dynamic jsonObject = new JObject();
                jsonObject.file = path;
                return jsonObject;
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
        /// Send a Role-based or Free Form Document Invite
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <param name="DocumentId"></param>
        /// <param name="DataObj">Data Object (ex. dynamic new { to = new[] { new { email = "name@domain.com", role_id = ... } } }</param>
        /// <returns>{"result":"success"}</returns>
        public static JObject Invite(string AccessToken, string DocumentId, dynamic DataObj)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            var request = new RestRequest("/document/" + DocumentId + "/invite", Method.POST)
                .AddHeader("Accept", "application/json")
                .AddHeader("Authorization", "Bearer " + AccessToken);

            request.RequestFormat = DataFormat.Json;
            request.AddBody(DataObj);

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
        /// Cancel Invite
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <param name="DocumentId"></param>
        /// <returns>{"status":"success"}</returns>
        public static JObject CancelInvite(string AccessToken, string DocumentId)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            var request = new RestRequest("/document/" + DocumentId + "/fieldinvitecancel", Method.PUT)
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
        /// Create a One-time Use Download URL
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <param name="DocumentId"></param>
        /// <returns>URL to download the document as a PDF</returns>
        public static JObject Share(string AccessToken, string DocumentId)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            var request = new RestRequest("/document/" + DocumentId + "/download/link", Method.POST)
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
        /// Merges Existing Documents
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <param name="DataObj">Data Object (ex. dynamic new { to = new[] { new { name = "My New Merged Doc", document_ids = ... } } }</param>
        /// <returns>Location the PDF file was saved to.</returns>
        public static JObject Merge(string AccessToken, dynamic DataObj, string SaveFilePath = "", string SaveFileName = "my-merged-document")
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            var request = new RestRequest("/document/merge", Method.POST)
                .AddHeader("Accept", "application/json")
                .AddHeader("Authorization", "Bearer " + AccessToken);

            request.RequestFormat = DataFormat.Json;
            request.AddBody(DataObj);

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var path = (SaveFilePath != "") ? Path.GetDirectoryName(SaveFilePath) + "/" + SaveFileName + ".pdf" : Directory.GetCurrentDirectory() + "/" + SaveFileName + ".pdf";
                client.DownloadData(request).SaveAs(path);

                dynamic jsonObject = new JObject();
                jsonObject.file = path;
                return jsonObject;
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
        /// Get Document History
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <param name="DocumentId"></param>
        /// <returns>Array of history for the document.</returns>
        public static JArray History(string AccessToken, string DocumentId)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            var request = new RestRequest("/document/" + DocumentId + "/history", Method.GET)
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
