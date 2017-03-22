using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignNow;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Examples
{
    class Examples
    {
        #region Console Helpers - Ignore
        static void ConsoleH1(string txt)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=======================");
            Console.WriteLine(" {0}", txt);
            Console.WriteLine("=======================");
            Console.ResetColor();
        }

        static void ConsoleH2(string txt)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{0} \r\n", txt);
            Console.ResetColor();
        }
        #endregion

        static void Main(string[] args)
        {
            //Test Information - Needed to Run Test Below
            string accountUser = "";
            string accountPass = "";
            string testEmailAddress = "";

            //Client ID, Client Secret, API URL
            SignNow.Config.init("[YOUR CLIENT ID]", "[YOUR CLIENT SECRET]", "https://api-eval.signnow.com/");
            //=======================
            // OAuth2
            //=======================
            ConsoleH1("OAuth2");

            //Request OAuth Token
            ConsoleH2("Requesting OAuth Token");
            JObject OAuthRes = SignNow.OAuth2.RequestToken(accountUser, accountPass);
            String AccessToken = OAuthRes["access_token"].ToString();
            Console.WriteLine("Access Token: {0} \r\n\r\n\r\n", AccessToken);

            //Verify OAuth Token
            ConsoleH2("Verifying OAuth2 Token");
            JObject OAuthVRes = SignNow.OAuth2.Verify(AccessToken);
            Console.WriteLine("Verified Access Token: {0} \r\n\r\n\r\n", OAuthVRes["access_token"]);

            //=======================
            // User
            //=======================
            ConsoleH1("User");

            //Create New SignNow User Account
            ConsoleH2("Creating New User Account");
            JObject newAccountRes = SignNow.User.Create("demo@demo.com", "123456789", "John", "Carter");
            Console.WriteLine(newAccountRes["id"] + "\r\n\r\n\r\n");

            //Retreive User Account Information
            ConsoleH2("Retrieving User Account Information");
            JObject accountRes = SignNow.User.Get(AccessToken);
            Console.WriteLine("First Name: {0}", accountRes["first_name"]);
            Console.WriteLine("Last Name: {0}", accountRes["last_name"]);
            Console.WriteLine("Primary Email: {0}", accountRes["primary_email"]);
            Console.WriteLine("Active: {0} \r\n\r\n\r\n", accountRes["active"]);

            //=======================
            // Document
            //=======================
            ConsoleH1("Document");

            //Create a New Document
            ConsoleH2("Creating New Document");
            JObject newDocRes = SignNow.Document.Create(AccessToken, "pdf-sample.pdf");
            String DocumentId = newDocRes["id"].ToString();
            Console.WriteLine("New Document ID: {0} \r\n\r\n\r\n", DocumentId);

            //Create a New Document and Extract the Fields
            ConsoleH2("Creating New Document & Extracting Fields");
            JObject newDocExtRes = SignNow.Document.Create(AccessToken, "Example Fields.docx", true);
            Console.WriteLine("New Document ID using Extract Fields: {0} \r\n\r\n\r\n", newDocExtRes["id"]);

            //Update Document Add Field
            ConsoleH2("Updating Document Adding Fields");
            dynamic dataObj = new 
            {
                fields = new[] 
                { 
                    new
                    {
                        x = 10,
                        y = 10,
                        width = 122,
                        height = 34,
                        page_number = 0,
                        role = "Buyer",
                        required = true,
                        type = "signature"
                    }
                }
            };
            JObject updateDocRes = SignNow.Document.Update(AccessToken, DocumentId, dataObj);
            Console.WriteLine("Updated Document ID: {0} \r\n\r\n\r\n", updateDocRes["id"]);

            //Delete Document
            ConsoleH2("Deleting Document");
            JObject deleteDocRes = SignNow.Document.Delete(AccessToken, newDocExtRes["id"].ToString());
            Console.WriteLine("Deleted Document {0}: {1} \r\n\r\n\r\n", newDocExtRes["id"], deleteDocRes["status"]);

            //Download Document
            ConsoleH2("Downloading Document");
            JObject downloadDocRes = SignNow.Document.Download(AccessToken, DocumentId, "/", "sample");
            Console.WriteLine("Download Document: {0} \r\n\r\n\r\n", downloadDocRes["file"].ToString());
            
            //Send Free Form Invite
            ConsoleH2("Sending Role-based Invite");
            dynamic inviteDataObj = new
            {
                from = accountUser,
                to = testEmailAddress
            };
            JObject tempDocRes = SignNow.Document.Create(AccessToken, "pdf-sample.pdf");
            JObject sendFreeFormInviteRes = SignNow.Document.Invite(AccessToken, tempDocRes["id"].ToString(), inviteDataObj);
            Console.WriteLine("Invite Status: {0} \r\n\r\n\r\n", sendFreeFormInviteRes["result"].ToString());

            //Cancel Invite
            ConsoleH2("Canceling Invite");
            JObject cancelInviteRes = SignNow.Document.CancelInvite(AccessToken, tempDocRes["id"].ToString());
            Console.WriteLine("Cancel Invite Status: {0} \r\n\r\n\r\n", cancelInviteRes["status"].ToString());

            //Create Download Link
            ConsoleH2("Creating Download Link (Share)");
            JObject downloadLinkRes = SignNow.Document.Share(AccessToken, DocumentId);
            Console.WriteLine("Download Link: {0} \r\n\r\n\r\n", downloadLinkRes["link"].ToString());

            //Merge Existing Documents
            ConsoleH2("Merging Existing Documents");
            dynamic mergeDocsObj = new
            {
                name = "My New Merged Doc",
                document_ids = new[] { DocumentId, tempDocRes["id"].ToString() }
            };
            JObject mergeDocsRes = SignNow.Document.Merge(AccessToken, mergeDocsObj, "/", "sample-merge");
            Console.WriteLine("Merged Docs Result: {0} \r\n\r\n\r\n", mergeDocsRes);

            //Document History
            ConsoleH2("Retrieving Document History");
            JArray docHistoryRes = SignNow.Document.History(AccessToken, DocumentId);
            Console.WriteLine("Document History: {0} \r\n\r\n\r\n", docHistoryRes[0].ToString());

            //=======================
            // Template
            //=======================
            ConsoleH1("Template");

            //Create Template
            ConsoleH2("Create Template");
            JObject newTemplateRes = SignNow.Template.Create(AccessToken, DocumentId, "My New Template");
            Console.WriteLine("New Template ID: {0} \r\n\r\n\r\n", newTemplateRes["id"].ToString());

            //Copy Template
            ConsoleH2("Copy Template");
            JObject copyTemplateRes = SignNow.Template.Copy(AccessToken, DocumentId, "My Copy Template Doc");
            Console.WriteLine("Results: {0} \r\n\r\n\r\n", copyTemplateRes);

            //=======================
            // Folder
            //=======================
            ConsoleH1("Folder");

            //List Folders
            ConsoleH2("List Folders");
            JObject listFoldersRes = SignNow.Folder.List(AccessToken);
            Console.WriteLine("Results: {0} \r\n\r\n\r\n", listFoldersRes.ToString());

            //Get Folder
            ConsoleH2("Get Folder");
            JObject getFolderRes = SignNow.Folder.Get(AccessToken, listFoldersRes["folders"][0]["id"].ToString());
            Console.WriteLine("Results: {0} \r\n\r\n\r\n", getFolderRes.ToString());

            //=======================
            // Webhook
            //=======================
            ConsoleH1("Webhook");

            //Create Webook
            ConsoleH2("Create Webhook");
            JObject createWebhookRes = SignNow.Webhook.Create(AccessToken, "document.create", "http://requestb.in/");
            Console.WriteLine("Results: {0} \r\n\r\n\r\n", createWebhookRes.ToString());

            //List Webhooks
            ConsoleH2("List Webhooks");
            JObject listWebhooksRes = SignNow.Webhook.List(AccessToken);
            Console.WriteLine("Results: {0} \r\n\r\n\r\n", listWebhooksRes.ToString());

            //Delete Webhook
            ConsoleH2("Delete Webhooks");
            JObject deleteWebhookRes = SignNow.Webhook.Delete(AccessToken, createWebhookRes["id"].ToString());
            Console.WriteLine("Results: {0} \r\n\r\n\r\n", deleteWebhookRes.ToString());

            //=======================
            // Link
            //=======================
            ConsoleH1("Link");

            ConsoleH2("Create Signing Link");
            JObject createLinkRes = SignNow.Link.Create(AccessToken, DocumentId);
            Console.WriteLine("Results: {0} \r\n\r\n\r\n", createLinkRes.ToString());

            //=======================
            // PAUSE FOR READING
            //=======================
            Console.Read();
        }
    }
}
