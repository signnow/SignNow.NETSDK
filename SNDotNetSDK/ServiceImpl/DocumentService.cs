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
        private Config copyConfig;
        public DocumentService(Config copyConfig)
        {
            this.copyConfig = copyConfig;
        }
        /*
         * This method is used to create  or POST the document for a given user in the SignNow Application
         */
        public Document create(Oauth2Token token, Document documentPath)
        {
            Document document = null;
            try
            {
            string requestBody = JsonConvert.SerializeObject(documentPath.filePath, Formatting.Indented);
            var client = new RestClient();
            client.BaseUrl = copyConfig.getApiBase();

            var request = new RestRequest("/document", Method.POST)
                    .AddHeader("Accept", "application/json")
                    .AddHeader("Authorization", "Bearer " + token.access_token)
                    .AddHeader("Content-Type","multipart/form-data");
                request.AddFile("file", documentPath.filePath);

            var httpResponse = client.Execute(request);
       
            string json = httpResponse.Content.ToString();
            document = JsonConvert.DeserializeObject<Document>(json);
            }
            catch(Exception ex)
            {
                throw new SystemException();
            }
            return document;
        }

        /*
         * This method retrieves all the uploaded documents for the specified user.
         */
        public Document[] getDocuments(Oauth2Token token)
        {
            Document[] docs = new Document[100];
            try
            {
                string requestBody = JsonConvert.SerializeObject(token, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = copyConfig.getApiBase();
                var request = new RestRequest("/user/documentsv2", Method.GET)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.access_token);

                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();

                JArray jar = JArray.Parse(json);
                int i=0;
                foreach (JObject jobj in jar)
                {
                    docs[i] = JsonConvert.DeserializeObject<Document>(jobj.ToString());
                    i++;
                }
            }
            catch (Exception ex)
            {
                throw new SystemException();
            }
            return docs;
        }

        /*
            This method is used to GET the document for a given user from the SignNow Application
        */
        public Document getDocumentbyId(Oauth2Token token, string id)
        {
            Document document = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(token, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = copyConfig.getApiBase();
                var request = new RestRequest("/document" + "/" +id, Method.GET)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.access_token);

                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();

                document = JsonConvert.DeserializeObject<Document>(json);
            }
            catch (Exception ex)
            {
                throw new SystemException();
            }
            return document;
        }

        /*
            This method is used to update [PUT] the document for a given user from the SignNow Application
        */
        public Document updateDocument(Oauth2Token token, Dictionary<string, List<Fields>> fieldsMap, string id)
        {
            Document document = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(fieldsMap, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = copyConfig.getApiBase();

                var request = new RestRequest("/document" + "/" +id, Method.PUT)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Content-Type", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.access_token);
                request.AddParameter("text/json", requestBody, ParameterType.RequestBody);
                

                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();

                document = JsonConvert.DeserializeObject<Document>(json);
            }
            catch (Exception ex)
            {
                throw new SystemException();
            }
            return document;
        }

        /*
         * This method is used to (POST) invite the signers to sign on  the document in the SignNow Application
         */
        public string invite(Oauth2Token token, Invitation invitation, string id)
        {
            string result = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(invitation, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = copyConfig.getApiBase();

                var request = new RestRequest("/document" + "/" + id + "/invite?email=disable", Method.POST)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Content-Type", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.access_token);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(invitation);


                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();
                JObject res = JObject.Parse(json);
                result = res["result"].ToString();
            }
            catch(Exception ex)
            {
                throw new SystemException();
            }
            return result;
        }

        /*
        This method is used to (POST)perform rolebased  to invite the signers to sign on  the document in the SignNow Application
        */
        public string roleBasedInvite(Oauth2Token token, EmailSignature emailSignature, string id)
        {
            string result = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(emailSignature, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = copyConfig.getApiBase();

                var request = new RestRequest("/document" + "/" + id + "/invite", Method.POST)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Content-Type", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.access_token);
                request.AddParameter("text/json", requestBody, ParameterType.RequestBody);


                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();
                JObject job = JObject.Parse(json);
                result = job["status"].ToString();
            }
            catch(Exception ex)
            {
                throw new SystemException();
            }
            return result;
        }

        /*
         * This method Cancels an invite to a document.
         */
        public string cancelInvite(Oauth2Token token, string id)
        {
            string result = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(token, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = copyConfig.getApiBase();

                var request = new RestRequest("/document" + "/" + id + "/fieldinvitecancel", Method.PUT)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Content-Type", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.access_token);

                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();
                JObject job = JObject.Parse(json);
                result = job["status"].ToString();
            }
            catch (Exception ex)
            {
                throw new SystemException();
            }
            return result;
        }

        /*
         * This method is used to download (POST) the document as PDF for a given user from the SignNow Application
         */
        public Document shareDocument(Oauth2Token token, string id)
        {
            Document document = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(token, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = copyConfig.getApiBase();

                var request = new RestRequest("/document" + "/" + id + "/download/link", Method.POST)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.access_token);

                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();
                document = JsonConvert.DeserializeObject<Document>(json);
            }
            catch (Exception ex)
            {
                throw new SystemException();
            }
            return document;
        }

        /*
        This method is used to (GET) get the Document History for a given Document and for a given user from the SignNow Application
        */
        public DocumentHistory[] getDocumentHistory(Oauth2Token token, string id)
        {
            DocumentHistory[] docshistory = new DocumentHistory[100];
            try
            {
                string requestBody = JsonConvert.SerializeObject(token, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = copyConfig.getApiBase();

                var request = new RestRequest("/document" + "/" + id + "/history", Method.GET)
                                   .AddHeader("Authorization", "Bearer " + token.access_token);

                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();
                JArray jarr = JArray.Parse(json);
                int i = 0;
                foreach (JObject jobj in jarr)
                {
                    docshistory[i] = JsonConvert.DeserializeObject<DocumentHistory>(jobj.ToString());
                    i++;
                }
            }
            catch(Exception ex)
            {
                throw new SystemException();
            }
            return docshistory;
        }

        /*
         * This method is used to (POST)  create the template from a document in the SignNow Application
         */
        public Template createTemplate(Oauth2Token token, Template template)
        {
            Template templ = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(template, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = copyConfig.getApiBase();

                var request = new RestRequest("/template", Method.POST)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Content-Type", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.access_token);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(template);

                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();
                templ = JsonConvert.DeserializeObject<Template>(json);
            }
            catch (Exception ex)
            {
                throw new SystemException();
            }
            return templ;
        }

        /*
        This method is used to (POST) create a new document from the given template id in the SignNow Application
        */
        public Template createNewDocumentFromTemplate(Oauth2Token token, Template template)
        {
            Template templ = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(template, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = copyConfig.getApiBase();

                var request = new RestRequest("/template" + "/" + template.id + "/copy", Method.POST)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Content-Type", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.access_token);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(template);

                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();
                templ = JsonConvert.DeserializeObject<Template>(json);
            }
            catch (Exception ex)
            {
                throw new SystemException();
            }
            return templ;
        }

        /*
        This method is used to Download a collapsed document(Response Content = application/pdf)
        */
        public byte[] downloadCollapsedDocument(Oauth2Token token, string id)
        {
            byte[] arr = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(token, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = copyConfig.getApiBase();

                var request = new RestRequest("/document" + "/" + id + "/download?type=collapsed", Method.GET)
                        .AddHeader("Authorization", "Bearer " + token.access_token);
                var httpResponse = client.Execute(request);
                arr = httpResponse.RawBytes;
            }
            catch (Exception ex)
            {
                throw new SystemException();
            }
            return arr;
        }

        /*
         This method is used to Deletes a previously uploaded document
         */
        public string deleteDocument(Oauth2Token token, string id)
        {
            string message = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(token, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = copyConfig.getApiBase();

                var request = new RestRequest("/document" + "/" + id, Method.DELETE)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.access_token);
                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();
                JObject obj = JObject.Parse(json);
                message = obj["status"].ToString();
            }
            catch (Exception ex)
            {
                throw new SystemException();
            }
            return message;
        }

        /*
         This method is used to (POST) merge the new document from the given template id in the SignNow Application
         */
        public byte[] mergeDocuments(Oauth2Token token, Hashtable myMergeMap)
        {
            byte[] arr = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(myMergeMap, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = copyConfig.getApiBase();

                var request = new RestRequest("/document/merge", Method.POST)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Content-Type", "application/pdf")
                        .AddHeader("Authorization", "Bearer " + token.access_token);
                request.AddParameter("text/json", requestBody, ParameterType.RequestBody);

                var httpResponse = client.Execute(request);
                arr = httpResponse.RawBytes;  
            }
            catch(Exception ex)
            {
                throw new SystemException();
            }
            return arr;
        }

        /*
         This method is Used for creating webhooks that will be triggered when the specified event takes place.
         */
        public EventSubscription createEventSubscription(Oauth2Token token, EventSubscription events)
        {
            EventSubscription result = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(events, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = copyConfig.getApiBase();

                var request = new RestRequest("/event_subscription", Method.POST)
                        .AddHeader("Content-Type", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.access_token);
                request.AddParameter("text/json", requestBody, ParameterType.RequestBody);

                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();
                result = JsonConvert.DeserializeObject<EventSubscription>(json);
            }
            catch(Exception ex)
            {
                throw new SystemException();
            }
            return result;
        }

        /*
         This method is used to Delete an event subscription.
         */
        public EventSubscription deleteEventSubscription(Oauth2Token token, string id)
        {
            EventSubscription result = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(token, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = copyConfig.getApiBase();

                var request = new RestRequest("/event_subscription" + "/" +id, Method.DELETE)
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Authorization", "Bearer " + token.access_token);

                var httpResponse = client.Execute(request);
                string json = httpResponse.Content.ToString();
                result = JsonConvert.DeserializeObject<EventSubscription>(json);
            }
            catch (Exception ex)
            {
                throw new SystemException();
            }
            return result;
        }

        /*
         This method Uploads a file that contains SignNow Document Field Tags (Simple Field tags only)
         */
        public Document createSimpleFieldTag(Oauth2Token token, Document documentPath)
        {
            Document document = null;
            try
            {
                string requestBody = JsonConvert.SerializeObject(documentPath.filePath, Formatting.Indented);
                var client = new RestClient();
                client.BaseUrl = copyConfig.getApiBase();

                var request = new RestRequest("/document/fieldextract", Method.POST)
                        .AddHeader("Authorization", "Bearer " + token.access_token)
                        .AddHeader("Content-Type", "multipart/form-data");
                request.AddFile("file", documentPath.filePath);

                var httpResponse = client.Execute(request);

                string json = httpResponse.Content.ToString();
                document = JsonConvert.DeserializeObject<Document>(json);
            }
            catch (Exception ex)
            {
                throw new SystemException();
            }
            return document;
        }
    }
}