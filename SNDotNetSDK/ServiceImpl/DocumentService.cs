using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SNDotNetSDK.Configuration;
using SNDotNetSDK.Models;
using SNDotNetSDK.Service;
using System;
using System.Collections;
using System.Collections.Generic;
namespace SNDotNetSDK.ServiceImpl
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This class is used to perform operations on the Documents. This class is provides the guidle lines on how to call the SignNow API
     * for several operations like Create (POST), GetDocuemnt (GET), Update Document (PUT), Get Document History, etc.,
     */
    public class DocumentService : IDocumentService
    {
        private Config config;
        public DocumentService(Config config)
        {
            this.config = config;
        }
        /*
         * This method is used to create  or POST the document for a given user in the SignNow Application
         */
        public Document Create(Oauth2Token token, Document documentPath)
        {
            Document document = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(documentPath.FilePath, Formatting.Indented);
            var client = new RestClient();
            client.BaseUrl = config.GetApiBase();

            var request = new RestRequest("/document", Method.POST)
                    .AddHeader("Accept", "application/json")
                    .AddHeader("Authorization", "Bearer " + token.AccessToken)
                    .AddHeader("Content-Type","multipart/form-data");
                request.AddFile("file", documentPath.FilePath);

            var httpResponse = client.Execute(request);
       
            string json = httpResponse.Content.ToString();
            document = JsonConvert.DeserializeObject<Document>(json);
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
            return document;
        }

        /*
         * This method retrieves all the uploaded documents for the specified user.
         */
        public Document[] GetDocuments(Oauth2Token token)
        {
            Document[] docs = new Document[100];
            try
            {
                string requestBody = JsonConvert.SerializeObject(token, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = config.GetApiBase();
                var request = new RestRequest("/user/documentsv2", Method.GET)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.AccessToken);

                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();

                if(httpResponse.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    JArray jar = JArray.Parse(json);
                    int i = 0;
                    foreach (JObject jobj in jar)
                    {
                        docs[i] = JsonConvert.DeserializeObject<Document>(jobj.ToString());
                        i++;
                    }
                }
                else if (httpResponse.StatusCode.Equals(System.Net.HttpStatusCode.BadRequest))
                {
                    docs[0] = JsonConvert.DeserializeObject<Document>(json);
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
            return docs;
        }

        /*
            This method is used to GET the document for a given user from the SignNow Application
        */
        public Document GetDocumentbyId(Oauth2Token token, string id)
        {
            Document document = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(token, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = config.GetApiBase();
                var request = new RestRequest("/document" + "/" +id, Method.GET)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.AccessToken);

                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();

                document = JsonConvert.DeserializeObject<Document>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
            return document;
        }

        /*
            This method is used to update [PUT] the document for a given user from the SignNow Application
        */
        public Document UpdateDocument(Oauth2Token token, Dictionary<string, List<Fields>> fieldsMap, string id)
        {
            Document document = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(fieldsMap, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = config.GetApiBase();

                var request = new RestRequest("/document" + "/" +id, Method.PUT)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Content-Type", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.AccessToken);
                request.AddParameter("text/json", requestBody, ParameterType.RequestBody);
                

                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();

                document = JsonConvert.DeserializeObject<Document>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
            return document;
        }

        /*
         * This method is used to (POST) invite the signers to sign on  the document in the SignNow Application
         */
        public string Invite(Oauth2Token token, Invitation invitation, string id)
        {
            string result = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(invitation, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = config.GetApiBase();

                var request = new RestRequest("/document" + "/" + id + "/invite?email=disable", Method.POST)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Content-Type", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.AccessToken);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(invitation);


                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();
                JObject res = JObject.Parse(json);
                if (httpResponse.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    result = res["result"].ToString();
                }
                else if (httpResponse.StatusCode.Equals(System.Net.HttpStatusCode.BadRequest))
                {
                    result = res["error"].ToString();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
            return result;
        }

        /*
        This method is used to (POST)perform rolebased  to invite the signers to sign on  the document in the SignNow Application
        */
        public string RoleBasedInvite(Oauth2Token token, EmailSignature emailSignature, string id)
        {
            string result = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(emailSignature, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = config.GetApiBase();

                var request = new RestRequest("/document" + "/" + id + "/invite", Method.POST)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Content-Type", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.AccessToken);
                request.AddParameter("text/json", requestBody, ParameterType.RequestBody);


                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();
                JObject job = JObject.Parse(json);
                if (httpResponse.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    result = job["status"].ToString();
                }
                else if (httpResponse.StatusCode.Equals(System.Net.HttpStatusCode.BadRequest))
                {
                    result = job["error"].ToString();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
            return result;
        }

        /*
         * This method Cancels an invite to a document.
         */
        public string CancelInvite(Oauth2Token token, string id)
        {
            string result = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(token, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = config.GetApiBase();

                var request = new RestRequest("/document" + "/" + id + "/fieldinvitecancel", Method.PUT)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Content-Type", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.AccessToken);

                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();
                JObject job = JObject.Parse(json);
                if (httpResponse.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    result = job["status"].ToString();
                }
                else if (httpResponse.StatusCode.Equals(System.Net.HttpStatusCode.BadRequest))
                {
                    result = job["error"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
            return result;
        }

        /*
         * This method is used to download (POST) the document as PDF for a given user from the SignNow Application
         */
        public Document ShareDocument(Oauth2Token token, string id)
        {
            Document document = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(token, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = config.GetApiBase();

                var request = new RestRequest("/document" + "/" + id + "/download/link", Method.POST)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.AccessToken);

                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();
                document = JsonConvert.DeserializeObject<Document>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
            return document;
        }

        /*
        This method is used to (GET) get the Document History for a given Document and for a given user from the SignNow Application
        */
        public DocumentHistory[] GetDocumentHistory(Oauth2Token token, string id)
        {
            DocumentHistory[] docshistory = new DocumentHistory[100];
            try
            {
                string requestBody = JsonConvert.SerializeObject(token, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = config.GetApiBase();

                var request = new RestRequest("/document" + "/" + id + "/history", Method.GET)
                                   .AddHeader("Authorization", "Bearer " + token.AccessToken);

                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();

                if (httpResponse.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    JArray jarr = JArray.Parse(json);
                    int i = 0;
                    foreach (JObject jobj in jarr)
                    {
                        docshistory[i] = JsonConvert.DeserializeObject<DocumentHistory>(jobj.ToString());
                        i++;
                    }
                }
                else if (httpResponse.StatusCode.Equals(System.Net.HttpStatusCode.BadRequest))
                {
                    docshistory[0] = JsonConvert.DeserializeObject<DocumentHistory>(json); ;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
            return docshistory;
        }

        /*
         * This method is used to (POST)  create the template from a document in the SignNow Application
         */
        public Template CreateTemplate(Oauth2Token token, Template template)
        {
            Template templ = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(template, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = config.GetApiBase();

                var request = new RestRequest("/template", Method.POST)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Content-Type", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.AccessToken);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(template);

                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();
                templ = JsonConvert.DeserializeObject<Template>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
            return templ;
        }

        /*
        This method is used to (POST) create a new document from the given template id in the SignNow Application
        */
        public Template CreateNewDocumentFromTemplate(Oauth2Token token, Template template)
        {
            Template templ = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(template, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = config.GetApiBase();

                var request = new RestRequest("/template" + "/" + template.Id + "/copy", Method.POST)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Content-Type", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.AccessToken);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(template);

                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();
                templ = JsonConvert.DeserializeObject<Template>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
            return templ;
        }

        /*
        This method is used to Download a collapsed document(Response Content = application/pdf)
        */
        public byte[] DownloadCollapsedDocument(Oauth2Token token, string id)
        {
            byte[] arr = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(token, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = config.GetApiBase();

                var request = new RestRequest("/document" + "/" + id + "/download?type=collapsed", Method.GET)
                        .AddHeader("Authorization", "Bearer " + token.AccessToken);
                var httpResponse = client.Execute(request);
                arr = httpResponse.RawBytes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
            return arr;
        }

        /*
         This method is used to Deletes a previously uploaded document
         */
        public string DeleteDocument(Oauth2Token token, string id)
        {
            string message = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(token, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = config.GetApiBase();

                var request = new RestRequest("/document" + "/" + id, Method.DELETE)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.AccessToken);
                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();
                JObject obj = JObject.Parse(json);
                if (httpResponse.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    message = obj["status"].ToString();
                }
                else if (httpResponse.StatusCode.Equals(System.Net.HttpStatusCode.BadRequest))
                {
                    message = obj["error"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
            return message;
        }

        /*
         This method is used to (POST) merge the new document from the given template id in the SignNow Application
         */
        public byte[] MergeDocuments(Oauth2Token token, Hashtable myMergeMap)
        {
            byte[] arr = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(myMergeMap, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = config.GetApiBase();

                var request = new RestRequest("/document/merge", Method.POST)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Content-Type", "application/pdf")
                        .AddHeader("Authorization", "Bearer " + token.AccessToken);
                request.AddParameter("text/json", requestBody, ParameterType.RequestBody);

                var httpResponse = client.Execute(request);
                arr = httpResponse.RawBytes;  
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
            return arr;
        }

        /*
         This method is Used for creating webhooks that will be triggered when the specified event takes place.
         */
        public EventSubscription CreateEventSubscription(Oauth2Token token, EventSubscription events)
        {
            EventSubscription result = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(events, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = config.GetApiBase();

                var request = new RestRequest("/event_subscription", Method.POST)
                        .AddHeader("Content-Type", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.AccessToken);
                request.AddParameter("text/json", requestBody, ParameterType.RequestBody);

                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();
                result = JsonConvert.DeserializeObject<EventSubscription>(json);
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
            return result;
        }

        /*
         This method is used to Delete an event subscription.
         */
        public EventSubscription DeleteEventSubscription(Oauth2Token token, string id)
        {
            EventSubscription result = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(token, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = config.GetApiBase();

                var request = new RestRequest("/event_subscription" + "/" +id, Method.DELETE)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.AccessToken);

                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();
                result = JsonConvert.DeserializeObject<EventSubscription>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
            return result;
        }

        /*
         This method Uploads a file that contains SignNow Document Field Tags (Simple Field tags only)
         */
        public Document CreateSimpleFieldTag(Oauth2Token token, Document documentPath)
        {
            Document document = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(documentPath.FilePath, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = config.GetApiBase();

                var request = new RestRequest("/document/fieldextract", Method.POST)
                        .AddHeader("Authorization", "Bearer " + token.AccessToken)
                        .AddHeader("Content-Type", "multipart/form-data");
                request.AddFile("file", documentPath.FilePath);

                var httpResponse = client.Execute(request);

                string json = httpResponse.Content.ToString();
                document = JsonConvert.DeserializeObject<Document>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
            return document;
        }

        /*
         * This method is used to create  or POST the document that contains SignNow Document Field Tags.
         */
        public Document CreateDocumentFieldExtract(Oauth2Token token, Document documentPath)
        {
            Document document = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(documentPath.FilePath, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = config.GetApiBase();

                var request = new RestRequest("/document/fieldextract", Method.POST)
                        .AddHeader("Authorization", "Bearer " + token.AccessToken)
                        .AddHeader("Content-Type", "multipart/form-data");
                request.AddFile("file", documentPath.FilePath);

                var httpResponse = client.Execute(request);

                string json = httpResponse.Content.ToString();
                document = JsonConvert.DeserializeObject<Document>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
            return document;
        }
    }
}