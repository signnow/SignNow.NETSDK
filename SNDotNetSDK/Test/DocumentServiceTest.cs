using Microsoft.VisualStudio.TestTools.UnitTesting;
using SNDotNetSDK.Configuration;
using SNDotNetSDK.Models;
using SNDotNetSDK.Service;
using SNDotNetSDK.ServiceImpl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SNDotNetSDK.Test
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This test class is used to perform and test the Document specific test operations.
     */
    [TestClass]
    public class DocumentServiceTest
    {
        static CopyClient copyclient;
        private string inputdirPath = "inputdirPath";
        private string outputdirPath = "outputdirPath";

        [ClassInitialize]
        public static void before(TestContext t)
        {
            Config config = new Config("ApiBAse", "Client-Id", "Client-Secret");
            copyclient = new CopyClient(config);
        }
        
        /*
        * This test method is used to create a Document in SignNow Application
        */
       [TestMethod]
        public void createDocument()
        {
            string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            User user = new User();
            user.email = randomEmail;
            user.password = "fakePassword";
            user.first_name = "firstName";
            user.last_name = "LastName";

            User resultUser = copyclient.userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = copyclient.authenticationService.requestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.access_token);

            Document doc = new Document();
            if(Directory.Exists(inputdirPath))
            {
                string[] docFilePath = Directory.GetFiles(@inputdirPath);
                doc.filePath = docFilePath[0];
            }

            Document document = copyclient.documentService.create(requestedToken, doc);
            Assert.IsNotNull("DocumentId", document.id);
        }

        /*
         * This test method is used to get all the documents in the form of array for the specified user.
         */
        [TestMethod]
        public void getDocument()
        {
            string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            User user = new User();
            user.email = randomEmail;
            user.password = "fakePassword";
            user.first_name = "firstName";
            user.last_name = "LastName";

            User resultUser = copyclient.userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = copyclient.authenticationService.requestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.access_token);

            Document doc = new Document();
            if (Directory.Exists(inputdirPath))
            {
                string[] docFilePath = Directory.GetFiles(@inputdirPath);
                doc.filePath = docFilePath[0];
            }

            Document document = copyclient.documentService.create(requestedToken, doc);
            Assert.IsNotNull("DocumentId", document.id);

            Document[] resultDoc = copyclient.documentService.getDocument(requestedToken);
            Assert.IsNotNull("resultDocid's", resultDoc.Length.ToString());
        }

        /*
         * This test method is used to GET the Document for a given user based on the given DocumentID from SignNow Application
         */
        [TestMethod]
        public void getDocumentbyId()
        {
            string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            User user = new User();
            user.email = randomEmail;
            user.password = "fakePassword";
            user.first_name = "firstName";
            user.last_name = "LastName";

            User resultUser = copyclient.userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = copyclient.authenticationService.requestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.access_token);

            Document doc = new Document();
            if (Directory.Exists(inputdirPath))
            {
                string[] docFilePath = Directory.GetFiles(@inputdirPath);
                doc.filePath = docFilePath[0];
            }

            Document document = copyclient.documentService.create(requestedToken, doc);
            Assert.IsNotNull("DocumentId", document.id);

            Document resultDoc = copyclient.documentService.getDocumentbyId(requestedToken, document.id);
            Assert.IsNotNull("resultDocid", resultDoc.id);
        }

        /*
         * This test method is used to update an existing Document in SignNow Application
         */
        [TestMethod]
        public void updateDocument()
        {
            string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            User user = new User();
            user.email = randomEmail;
            user.password = "fakePassword";
            user.first_name = "firstName";
            user.last_name = "LastName";

            User resultUser = copyclient.userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = copyclient.authenticationService.requestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.access_token);

            Document doc = new Document();
            if (Directory.Exists(inputdirPath))
            {
                string[] docFilePath = Directory.GetFiles(@inputdirPath);
                doc.filePath = docFilePath[0];
            }

            Document document = copyclient.documentService.create(requestedToken, doc);
            Assert.IsNotNull("DocumentId", document.id);

            // Build the data for Signature Test
            System.Drawing.Image img = SNDotNetSDK.Properties.Resources.SignatureImage;
            string encodedString = ImageToBase64(img, ImageFormat.Jpeg);

            System.Drawing.Image img1 = SNDotNetSDK.Properties.Resources.SignatureImage1;
            string encodedString1 = ImageToBase64(img1, ImageFormat.Jpeg);

            Signature signature = new Signature();
            signature.x = 66;
            signature.y = 21;
            signature.width = 117;
            signature.height = 23;
            signature.page_number = 1;
            signature.data = encodedString;

            Signature signature1 = new Signature();
            signature1.x = 305;
            signature1.y = 18;
            signature1.width = 117;
            signature1.height = 23;
            signature1.page_number = 1;
            signature1.data = encodedString1;

            List<Fields> signatureList = new List<Fields>();
            signatureList.Add(signature);
            signatureList.Add(signature1);

            // Build the data for Texts Test
            Text text = new Text();
            text.size = 30;
            text.x = 61;
            text.y = 72;
            text.page_number = 0;
            text.font = "Arial";
            text.data = "A SAMPLE TEXT FIELD";
            text.line_height = 9.075;

            Text text1 = new Text();
            text1.size = 30;
            text1.x = 61;
            text1.y = 72;
            text1.page_number = 1;
            text1.font = "Arial";
            text1.data = "A SAMPLE TEXT FIELD 2";
            text1.line_height = 9.075;

            List<Fields> textsList = new List<Fields>();
            textsList.Add(text);
            textsList.Add(text1);

            // Build the data for Checks
            Checkbox checks = new Checkbox();
            checks.width = 20;
            checks.height = 20;
            checks.x = 234;
            checks.y = 500;
            checks.page_number = 0;

            Checkbox checks1 = new Checkbox();
            checks1.width = 20;
            checks1.height = 20;
            checks1.x = 200;
            checks1.y = 53;
            checks.page_number = 1;

            List<Fields> checksList = new List<Fields>();
            checksList.Add(checks);
            checksList.Add(checks1);

            // Creating the Fields

            Radio radiobutton = new Radio();
            radiobutton.page_number = 1;
            radiobutton.x = 150;
            radiobutton.y = 65;
            radiobutton.width = 40;
            radiobutton.height = 40;
            radiobutton.check = 0;
            radiobutton.value = "apple";
            radiobutton.created = "123456789";

            Radio radiobutton1 = new Radio();
            radiobutton1.page_number = 1;
            radiobutton1.x = 250;
            radiobutton1.y = 55;
            radiobutton1.width = 40;
            radiobutton1.height = 40;
            radiobutton1.check = 0;
            radiobutton1.value = "cherry";
            radiobutton1.created = "123456789";

            List<Fields> radioList = new List<Fields>();
            radioList.Add(radiobutton);
            radioList.Add(radiobutton1);

            Fields fields = new Fields();
            fields.x = 13;
            fields.y = 133;
            fields.width = 25;
            fields.height = 121;
            fields.page_number = 1;
            fields.role = "buyer";
            fields.required = true;
            fields.type = "radiobutton";
            fields.radio = radioList;

            List<Fields> fieldsList = new List<Fields>();
            fieldsList.Add(fields);

            Dictionary<string, List<Fields>> fieldsMap = new Dictionary<string, List<Fields>>();
            fieldsMap.Add("signatures",signatureList);
            fieldsMap.Add("texts", textsList);
            fieldsMap.Add("checks", checksList);
            fieldsMap.Add("fields", fieldsList);

            Document resultDoc = copyclient.documentService.updateDocument(requestedToken, fieldsMap, document.id);

            Assert.IsNotNull("DocumentId", document.id);
        }
       /**
        *
        * This utility method is used to convert the image based on its type to a base 64 encoded String.
        */
        protected static string ImageToBase64(Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        /*
         * This test method is used to invite the signers to sign the Document in SignNow Application
         */
        [TestMethod]
        public void invite()
        {
            string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            User user = new User();
            user.email = randomEmail;
            user.password = "fakePassword";
            user.first_name = "firstName";
            user.last_name = "LastName";

            User resultUser = copyclient.userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = copyclient.authenticationService.requestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.access_token);

            Document doc = new Document();
            if (Directory.Exists(inputdirPath))
            {
                string[] docFilePath = Directory.GetFiles(@inputdirPath);
                doc.filePath = docFilePath[0];
            }

            Document document = copyclient.documentService.create(requestedToken, doc);
            Assert.IsNotNull("DocumentId", document.id);

            string toEmail = "deepak" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            Invitation invitation = new Invitation();
            invitation.from = resultUser.email;
            invitation.to = toEmail;

            string resinvite = copyclient.documentService.invite(requestedToken, invitation, document.id);
            Assert.AreEqual("success", resinvite);
        }

        /*
         * This test method is used to send rolebased invites to the signers to sign on  the document
         */
        [TestMethod]
        public void roleBasedInvite()
        {
            string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            User user = new User();
            user.email = randomEmail;
            user.password = "fakePassword";
            user.first_name = "firstName";
            user.last_name = "LastName";

            User resultUser = copyclient.userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = copyclient.authenticationService.requestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.access_token);

            Document doc = new Document();
            if (Directory.Exists(inputdirPath))
            {
                string[] docFilePath = Directory.GetFiles(@inputdirPath);
                doc.filePath = docFilePath[0];
            }

            Document document = copyclient.documentService.create(requestedToken, doc);
            Assert.IsNotNull("DocumentId", document.id);

            // Build the data for Signature Test
            System.Drawing.Image img = SNDotNetSDK.Properties.Resources.SignatureImage;
            string encodedString = ImageToBase64(img, ImageFormat.Jpeg);

            System.Drawing.Image img1 = SNDotNetSDK.Properties.Resources.SignatureImage1;
            string encodedString1 = ImageToBase64(img1, ImageFormat.Jpeg);

            Signature signature = new Signature();
            signature.x = 66;
            signature.y = 21;
            signature.width = 117;
            signature.height = 23;
            signature.page_number = 1;
            signature.data = encodedString;

            Signature signature1 = new Signature();
            signature1.x = 305;
            signature1.y = 18;
            signature1.width = 117;
            signature1.height = 23;
            signature1.page_number = 1;
            signature1.data = encodedString1;

            List<Fields> signatureList = new List<Fields>();
            signatureList.Add(signature);
            signatureList.Add(signature1);

            // Build the data for Texts Test
            Text text = new Text();
            text.size = 30;
            text.x = 61;
            text.y = 72;
            text.page_number = 0;
            text.font = "Arial";
            text.data = "A SAMPLE TEXT FIELD";
            text.line_height = 9.075;

            Text text1 = new Text();
            text1.size = 30;
            text1.x = 61;
            text1.y = 72;
            text1.page_number = 1;
            text1.font = "Arial";
            text1.data = "A SAMPLE TEXT FIELD 2";
            text1.line_height = 9.075;

            List<Fields> textsList = new List<Fields>();
            textsList.Add(text);
            textsList.Add(text1);

            // Build the data for Checks
            Checkbox checks = new Checkbox();
            checks.width = 20;
            checks.height = 20;
            checks.x = 234;
            checks.y = 500;
            checks.page_number = 0;

            Checkbox checks1 = new Checkbox();
            checks1.width = 20;
            checks1.height = 20;
            checks1.x = 200;
            checks1.y = 53;
            checks.page_number = 1;

            List<Fields> checksList = new List<Fields>();
            checksList.Add(checks);
            checksList.Add(checks1);

            // Creating the Fields

            Radio radiobutton = new Radio();
            radiobutton.page_number = 1;
            radiobutton.x = 150;
            radiobutton.y = 65;
            radiobutton.width = 40;
            radiobutton.height = 40;
            radiobutton.check = 0;
            radiobutton.value = "apple";
            radiobutton.created = "123456789";

            Radio radiobutton1 = new Radio();
            radiobutton1.page_number = 1;
            radiobutton1.x = 250;
            radiobutton1.y = 55;
            radiobutton1.width = 40;
            radiobutton1.height = 40;
            radiobutton1.check = 0;
            radiobutton1.value = "cherry";
            radiobutton1.created = "123456789";

            List<Fields> radioList = new List<Fields>();
            radioList.Add(radiobutton);
            radioList.Add(radiobutton1);

            Fields fields = new Fields();
            fields.x = 13;
            fields.y = 133;
            fields.width = 25;
            fields.height = 121;
            fields.page_number = 1;
            fields.role = "signer";
            fields.required = true;
            fields.type = "radiobutton";
            fields.radio = radioList;

            Fields fields1 = new Fields();
            fields1.x = 20;
            fields1.y = 133;
            fields1.width = 122;
            fields1.height = 60;
            fields1.page_number = 0;
            fields1.role = "buyer";
            fields1.required = true;
            fields1.type = "initials";

            Fields fields2 = new Fields();
            fields2.x = 35;
            fields2.y = 133;
            fields2.width = 122;
            fields2.height = 60;
            fields2.page_number = 1;
            fields2.role = "TestingRole";
            fields2.required = true;
            fields2.type = "text";

            List<Fields> fieldsList = new List<Fields>();
            fieldsList.Add(fields);
            fieldsList.Add(fields1);
            fieldsList.Add(fields2);

            Dictionary<string, List<Fields>> fieldsMap = new Dictionary<string, List<Fields>>();
            fieldsMap.Add("signatures", signatureList);
            fieldsMap.Add("texts", textsList);
            fieldsMap.Add("checks", checksList);
            fieldsMap.Add("fields", fieldsList);

            Document resultDoc = copyclient.documentService.updateDocument(requestedToken, fieldsMap, document.id);
            Document getDoc = copyclient.documentService.getDocumentbyId(requestedToken, resultDoc.id);

            Fields[] flds = getDoc.fields;
            List<System.Collections.Hashtable> roleMapList = new List<System.Collections.Hashtable>();
            EmailSignature emailSignature = new EmailSignature();
            int counter = 0;
            //iterate over fields
            for(int i=0;i<flds.Length;i++)
            {
                string toEmail = "deepak" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
                    System.Collections.Hashtable roleMap = new System.Collections.Hashtable();
                    roleMap.Add("email", toEmail);
                    roleMap.Add("role_id", flds[i].role_id);
                    roleMap.Add("role", flds[i].role);
                    roleMap.Add("order", ++counter);
                 roleMapList.Add(roleMap);
            }
            emailSignature.to = roleMapList;
            emailSignature.from = resultUser.email;
            string[] ccuser = new string[] { "ccuser1@mailinator.com", "ccuser2@mailinator.com" };
            emailSignature.cc = ccuser;
            emailSignature.message = resultUser.email + " asked you to sign this document";
            emailSignature.subject = "SignNow Invitation";

            string resinvite = copyclient.documentService.roleBasedInvite(requestedToken, emailSignature, document.id);
            Assert.AreEqual("success", resinvite);
        }

        /*
         * This test method is used to Cancel an invite to a document.
         */
        [TestMethod]
        public void cancelInvite()
        {
            string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            User user = new User();
            user.email = randomEmail;
            user.password = "fakePassword";
            user.first_name = "firstName";
            user.last_name = "LastName";

            User resultUser = copyclient.userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = copyclient.authenticationService.requestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.access_token);

            Document doc = new Document();
            if (Directory.Exists(inputdirPath))
            {
                string[] docFilePath = Directory.GetFiles(@inputdirPath);
                doc.filePath = docFilePath[0];
            }

            Document document = copyclient.documentService.create(requestedToken, doc);
            Assert.IsNotNull("DocumentId", document.id);

            string toEmail = "deepak" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            Invitation invitation = new Invitation();
            invitation.from = resultUser.email;
            invitation.to = toEmail;

            string resinvite = copyclient.documentService.invite(requestedToken, invitation, document.id);
            Assert.AreEqual("success", resinvite);

            string cancelinvite = copyclient.documentService.cancelInvite(requestedToken, document.id);
            Assert.AreEqual("success", cancelinvite);
        }

        /*
         * This test method is used to GET a Document as PDF in SignNow Application
         */
        [TestMethod]
        public void downLoadDocumentAsPDF()
        {
            string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            User user = new User();
            user.email = randomEmail;
            user.password = "fakePassword";
            user.first_name = "firstName";
            user.last_name = "LastName";

            User resultUser = copyclient.userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = copyclient.authenticationService.requestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.access_token);

            Document doc = new Document();
            if (Directory.Exists(inputdirPath))
            {
                string[] docFilePath = Directory.GetFiles(@inputdirPath);
                doc.filePath = docFilePath[0];
            }

            Document document = copyclient.documentService.create(requestedToken, doc);
            Assert.IsNotNull("DocumentId", document.id);
            Document resdoc = copyclient.documentService.downLoadDocumentAsPDF(requestedToken, document.id);

            Assert.IsNotNull("Document Link", resdoc.link);
        }

        /*
         *This test method is used to GET the Document History for a given Document ID in SignNow Application
         */
        [TestMethod]
        public void getDocumentHistory()
        {
            string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            User user = new User();
            user.email = randomEmail;
            user.password = "fakePassword";
            user.first_name = "firstName";
            user.last_name = "LastName";

            User resultUser = copyclient.userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = copyclient.authenticationService.requestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.access_token);

            Document doc = new Document();
            if (Directory.Exists(inputdirPath))
            {
                string[] docFilePath = Directory.GetFiles(@inputdirPath);
                doc.filePath = docFilePath[0];
            }

            Document document = copyclient.documentService.create(requestedToken, doc);
            Assert.IsNotNull("DocumentId", document.id);

            DocumentHistory[] dochistory = copyclient.documentService.getDocumentHistory(requestedToken, document.id);
            Assert.IsNotNull("Ip Address :", dochistory[0].ip_address);
        }

        /*
         *This test method is used to create a Template for a Given Document in SignNow Application
         */
        [TestMethod]
        public void createTemplate()
        {
            string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            User user = new User();
            user.email = randomEmail;
            user.password = "fakePassword";
            user.first_name = "firstName";
            user.last_name = "LastName";

            User resultUser = copyclient.userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = copyclient.authenticationService.requestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.access_token);

            Document doc = new Document();
            if (Directory.Exists(inputdirPath))
            {
                string[] docFilePath = Directory.GetFiles(@inputdirPath);
                doc.filePath = docFilePath[0];
            }

            Document document = copyclient.documentService.create(requestedToken, doc);
            Assert.IsNotNull("DocumentId", document.id);

            Template template = new Template();
            template.document_id = document.id;
            template.document_name = "New Template";

            Template resultTemplate = copyclient.documentService.createTemplate(requestedToken, template);
            Assert.IsNotNull("template create result", resultTemplate.id);
        }

        /*
         This test method is used to create a Document from a Template based on the template id SignNow Application
         */
        [TestMethod]
        public void createNewDocumentFromTemplate()
        {
            string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            User user = new User();
            user.email = randomEmail;
            user.password = "fakePassword";
            user.first_name = "firstName";
            user.last_name = "LastName";

            User resultUser = copyclient.userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = copyclient.authenticationService.requestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.access_token);

            Document doc = new Document();
            if (Directory.Exists(inputdirPath))
            {
                string[] docFilePath = Directory.GetFiles(@inputdirPath);
                doc.filePath = docFilePath[0];
            }

            Document document = copyclient.documentService.create(requestedToken, doc);
            Assert.IsNotNull("DocumentId", document.id);

            Template template = new Template();
            template.document_id = document.id;
            template.document_name = "New Template-PostDoc28";

            Template resultTemplate = copyclient.documentService.createTemplate(requestedToken, template);
            Assert.IsNotNull("template create result", resultTemplate.id);
            resultTemplate.document_name = "Copy Template-PostDoc28";

            Template copyTemplate = copyclient.documentService.createNewDocumentFromTemplate(requestedToken, resultTemplate);
            Assert.IsNotNull("Document Id", copyTemplate.id);
        }

        /*
         This test method is used to get the document in the form of bytes which is later transformed to .pdf
         */
        [TestMethod]
        public void downloadCollapsedDocument()
        {
            string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            User user = new User();
            user.email = randomEmail;
            user.password = "fakePassword";
            user.first_name = "firstName";
            user.last_name = "LastName";

            User resultUser = copyclient.userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = copyclient.authenticationService.requestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.access_token);

            Document doc = new Document();
            if (Directory.Exists(inputdirPath))
            {
                string[] docFilePath = Directory.GetFiles(@inputdirPath);
                doc.filePath = docFilePath[0];
            }

            Document document = copyclient.documentService.create(requestedToken, doc);
            Assert.IsNotNull("DocumentId", document.id);

            byte[] docarr = copyclient.documentService.downloadCollapsedDocument(requestedToken, document.id);
            if(Directory.Exists(outputdirPath))
            {
                string dest = outputdirPath + @"\" + document.id + ".pdf";
                File.WriteAllBytes(dest, docarr);
            }
            Assert.IsNotNull("Document Content", docarr.Length.ToString());
        }

        /*
         This test method is used to delete a previously uploaded document
         */
        [TestMethod]
        public void deleteDocument()
        {
            string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            User user = new User();
            user.email = randomEmail;
            user.password = "fakePassword";
            user.first_name = "firstName";
            user.last_name = "LastName";

            User resultUser = copyclient.userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = copyclient.authenticationService.requestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.access_token);

            Document doc = new Document();
            if (Directory.Exists(inputdirPath))
            {
                string[] docFilePath = Directory.GetFiles(@inputdirPath);
                doc.filePath = docFilePath[0];
            }

            Document document = copyclient.documentService.create(requestedToken, doc);
            Assert.IsNotNull("DocumentId", document.id);

            string confirm = copyclient.documentService.deleteDocument(requestedToken, document.id);
            Assert.AreEqual("success", confirm);
        }

        /*
         This test method is used to merge the list of documents based on the given document id's in SignNow Application
         */
        [TestMethod]
        public void mergeDocuments()
        {
            string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            User user = new User();
            user.email = randomEmail;
            user.password = "fakePassword";
            user.first_name = "firstName";
            user.last_name = "LastName";

            User resultUser = copyclient.userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = copyclient.authenticationService.requestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.access_token);

            Document doc1 = new Document();
            Document doc2 = new Document();
            if (Directory.Exists(inputdirPath) && Directory.GetFiles(@inputdirPath).Length>=2)
            {
                string[] docFilePath = Directory.GetFiles(@inputdirPath);
                doc1.filePath = docFilePath[0];
                doc2.filePath = docFilePath[1];
            }
            Document document1 = copyclient.documentService.create(requestedToken, doc1);
            Assert.IsNotNull("DocumentId", document1.id);
            Document document2 = copyclient.documentService.create(requestedToken, doc2);
            Assert.IsNotNull("DocumentId", document2.id);

            List<string> docIds = new List<string>();
            docIds.Add(document1.id);
            docIds.Add(document2.id);
            Hashtable myMergeMap = new Hashtable();
            myMergeMap.Add("document_ids", docIds);

            byte[] res = copyclient.documentService.mergeDocuments(requestedToken, myMergeMap);
            if (Directory.Exists(outputdirPath))
            {
                string dest = outputdirPath + @"\Merge" + (document1.id.Substring(1, 4) + document2.id.Substring(1, 4)) + ".pdf";
                File.WriteAllBytes(dest, res);
            }
            Assert.IsNotNull("Document Content", res.Length.ToString());
        }

        /*
         This test method is used for creating event subscription, which will be triggered when specific event take place
         */
        [TestMethod]
        public void createEventSubscription()
        {
            string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            User user = new User();
            user.email = randomEmail;
            user.password = "fakePassword";
            user.first_name = "firstName";
            user.last_name = "LastName";

            User resultUser = copyclient.userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = copyclient.authenticationService.requestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.access_token);

            EventSubscription evs = new EventSubscription();
            evs.Event = "document.create";
            evs.callback_url = "https://www.myapp.com/path/to/callback.php";
            EventSubscription res = copyclient.documentService.createEventSubscription(requestedToken, evs);
            Assert.IsNotNull("Subscription Id Created", res.id);
        }

        /*
         This test method is used to delete an event subscription 
         */
        [TestMethod]
        public void deleteEventSubscription()
        {
            string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            User user = new User();
            user.email = randomEmail;
            user.password = "fakePassword";
            user.first_name = "firstName";
            user.last_name = "LastName";

            User resultUser = copyclient.userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = copyclient.authenticationService.requestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.access_token);

            EventSubscription evs = new EventSubscription();
            evs.Event = "document.create";
            evs.callback_url = "https://www.myapp.com/path/to/callback.php";
            EventSubscription res = copyclient.documentService.createEventSubscription(requestedToken, evs);
            Assert.IsNotNull("Subscription Id Created", res.id);

            EventSubscription deleteEvent = copyclient.documentService.deleteEventSubscription(requestedToken, res.id);
            Assert.AreEqual("deleted", deleteEvent.status);
        }

        /*
         This test method is used to upload a file that contains SignNow Document Field Tags and the tags here are only
         * Simple field tags
         */
        [TestMethod]
        public void createSimpleFieldTag()
        {
            string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
            User user = new User();
            user.email = randomEmail;
            user.password = "fakePassword";
            user.first_name = "firstName";
            user.last_name = "LastName";

            User resultUser = copyclient.userService.create(user);

            Assert.IsNotNull("No user id from creating user", resultUser.id);
            resultUser.password = "fakePassword";

            Oauth2Token requestedToken = copyclient.authenticationService.requestToken(resultUser);
            Assert.IsNotNull("Access Token", requestedToken.access_token);

            Document doc = new Document();
            if (Directory.Exists(inputdirPath))
            {
                string[] docFilePath = Directory.GetFiles(@inputdirPath);
                doc.filePath = docFilePath[0];
            }

            Document document = copyclient.documentService.create(requestedToken, doc);
            Assert.IsNotNull("DocumentId", document.id);
        }
    }
}