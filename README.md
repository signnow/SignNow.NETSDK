 SignNowDotNetSDK
 ===============
A UnitTest Project using REST Endpoints API.

#### About SignNow
SignNow by Barracuda is an eSigning platform that offers a cloud version, a physical appliance and also a virtual appliance. Backed by Barracuda’s industry-leading security infrastructure, SignNow is fully compliant with eSigning laws and encrypts all data in transit. Users can share, manage and access their documents with confidence. It’s never been easier to get legally binding signatures from customers, partners, and employees - in seconds using any device.

#### API Contact Information
If you have questions about the SignNow API, please visit https://techlib.barracuda.com/SignNow/RestEndpointsAPI or email [api@signnow.com](mailto:api@signnow.com).

See additional contact information at the bottom.

Examples
==================

To run the examples you will need an API key. You can get one here [https://signnow.com/l/api/request_information](https://signnow.com/l/api/request_information). 

Before start using the Rest API's using this project configure the Keys in (App.config) configuration file:

-- Client Id and Client Secret are the API keys which can be obtained as explained above.

-- Input Directory and Output Directory have to be created to upload or download a document to/from Sign Now Application.

--Log file will be generated under "SNDotNetSDK\bin\Debug" with the name (log.txt) and the log configuration file (log4.config) is also under this folder and also the config file can be changed for adding/removing Appenders etc. Details on how to modify the config file can be accessed here- (https://logging.apache.org/log4net/release/manual/configuration.html)

##User

To Create and Retrieve the User to/from SigNow Application use the UserServiceTest.

##Token 

To perform Token related operations use the Oauth2TokenServiceTest.

##Document Service

To perform and test the Document specific test operations use DocumentServiceTest.

For a full list of accepted parameters, refer to the SignNow REST Endpoints API guide: [https://techlib.barracuda.com/SignNow/RestEndpointsAPI](https://techlib.barracuda.com/SignNow/RestEndpointsAPI).

##### Initialize
'''
IUserService userService = new UserService();
IAuthenticationService authenticationService = new OAuth2TokenService();
IDocumentService documentService = new DocumentService();
'''

##### Examples
Below are examples.	

##### Create a User
'''
public void createUser()
{
        String randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";
        Config config = new Config(apibase, clientID, clientSecret);User resultUser = userService.create(user);
        Console.WriteLine(resultUser.email + " " + resultUser.id);
}
'''
##### Get User
'''
public void getUser()
{
		String randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";
        Config config = new Config(apibase, clientID, clientSecret);
		User resultUser = userService.create(user);
        resultUser.password = "fakePassword";
        Oauth2Token requestedToken = authenticationService.requestToken(resultUser);
        resultUser.oauth2Token = requestedToken;
        User getUser = userService.get(resultUser);
		Console.WriteLine("Result User ID" + getUser.id);
}
'''
##### Request Token
'''
public void requestToken()
{
		string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
		User user = new User();
		user.email = randomEmail;
		user.password = "fakePassword";
		user.first_name = "firstName";
		user.last_name = "LastName";
		Config config = new Config(apibase, clientID, clientSecret);
		User resultUser = userService.create(user);
		resultUser.password = "fakePassword";
		Oauth2Token requestedToken = authenticationService.requestToken(resultUser);
		Console.WriteLine("Access Token" + requestedToken.access_token);        
}
'''
##### Verify Token
'''
public void verifyToken()
{
        string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";
        Config config = new Config(apibase, clientID, clientSecret);
		User resultUser = userService.create(user);
		resultUser.password = "fakePassword";
		Oauth2Token requestedToken = authenticationService.requestToken(resultUser);
        Oauth2Token verifiedToken = authenticationService.verify(requestedToken);
		Console.WriteLine("Verify Token", verifiedToken.access_token);
}
'''
##### Create Document
'''
public void createDocument()
{
		string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";
        Config config = new Config(apibase, clientID, clientSecret);
		User resultUser = userService.create(user);
		resultUser.password = "fakePassword";
        Oauth2Token requestedToken = authenticationService.requestToken(resultUser);
        Document doc = new Document();
        if(Directory.Exists(inputdirPath))
        {
            string[] docFilePath = Directory.GetFiles(@inputdirPath);
            doc.filePath = docFilePath[0];
        }
            
        Document document = documentService.create(requestedToken, doc);
        Console.WriteLine("DocumentId", document.id);
}
'''
##### Get Document
'''
public void getDocument()
{
        string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";
        Config config = new Config(apibase, clientID, clientSecret);
		User resultUser = userService.create(user);
		resultUser.password = "fakePassword";
		Oauth2Token requestedToken = authenticationService.requestToken(resultUser);
        Document doc = new Document();
        if (Directory.Exists(inputdirPath))
        {
            string[] docFilePath = Directory.GetFiles(@inputdirPath);
            doc.filePath = docFilePath[0];
        }
        Document document = documentService.create(requestedToken, doc);
        Document[] resultDoc =  documentService.getDocument(requestedToken);
        Console.WriteLine("resultDocid's", resultDoc.Length.ToString());
}
'''
##### Get Document By Id
'''
public void getDocumentbyId()
{
        string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";
        Config config = new Config(apibase, clientID, clientSecret);
		User resultUser = userService.create(user);
		resultUser.password = "fakePassword";
		Oauth2Token requestedToken = authenticationService.requestToken(resultUser);
        Document doc = new Document();
        if (Directory.Exists(inputdirPath))
        {
            string[] docFilePath = Directory.GetFiles(@inputdirPath);
            doc.filePath = docFilePath[0];
        }
        Document document = documentService.create(requestedToken, doc);
        Document resultDoc = documentService.getDocumentbyId(requestedToken, document.id);
        Console.WriteLine("resultDocid", resultDoc.id);
}
'''
##### Update Document
'''
public void updateDocument()
{
        string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";
        Config config = new Config(apibase, clientID, clientSecret);
		User resultUser = userService.create(user);
		resultUser.password = "fakePassword";
		Oauth2Token requestedToken = authenticationService.requestToken(resultUser);
        Document doc = new Document();
        if (Directory.Exists(inputdirPath))
        {
            string[] docFilePath = Directory.GetFiles(@inputdirPath);
            doc.filePath = docFilePath[0];
        }
        Document document = documentService.create(requestedToken, doc);
        
		System.Drawing.Image img = SNDotNetSDK.Properties.Resources.SignatureImage;
        string encodedString = ImageToBase64(img, ImageFormat.Jpeg);
		Signature signature = new Signature();
        signature.x = 66;
        signature.y = 21;
        signature.width = 117;
        signature.height = 23;
        signature.page_number = 1;
        signature.data = encodedString;
		List<Fields> signatureList = new List<Fields>();
        signatureList.Add(signature);
		
		Text text = new Text();
        text.size = 30;
        text.x = 61;
        text.y = 72;
        text.page_number = 0;
        text.font = "Arial";
        text.data = "A SAMPLE TEXT FIELD";
        text.line_height = 9.075;
		List<Fields> textsList = new List<Fields>();
        textsList.Add(text);
		
		Checkbox checks = new Checkbox();
        checks.width = 20;
        checks.height = 20;
        checks.x = 234;
        checks.y = 500;
        checks.page_number = 0;
		List<Fields> checksList = new List<Fields>();
        checksList.Add(checks);
		
		Dictionary<string, List<Fields>> fieldsMap = new Dictionary<string, List<Fields>>();
        fieldsMap.Add("signatures",signatureList);
        fieldsMap.Add("texts", textsList);
        fieldsMap.Add("checks", checksList);
		
		Document resultDoc = documentService.updateDocument(requestedToken, fieldsMap, document.id);
		Console.WriteLine("DocumentId", document.id);
}
'''
##### Invite Signers
'''
public void invite()
{
        string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";
        Config config = new Config(apibase, clientID, clientSecret);
		User resultUser = userService.create(user);
		resultUser.password = "fakePassword";
		Oauth2Token requestedToken = authenticationService.requestToken(resultUser);
        Document doc = new Document();
        if (Directory.Exists(inputdirPath))
        {
            string[] docFilePath = Directory.GetFiles(@inputdirPath);
            doc.filePath = docFilePath[0];
        }
        Document document = documentService.create(requestedToken, doc);
        string toEmail = "deepak" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        Invitation invitation = new Invitation();
        invitation.from = resultUser.email;
        invitation.to = toEmail;
		string resinvite = documentService.invite(requestedToken, invitation, document.id);
        Console.WriteLine("success");
}
'''
##### Role Based Invite
'''
public void roleBasedInvite()
{
        string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";
        Config config = new Config(apibase, clientID, clientSecret);
		User resultUser = userService.create(user);
		resultUser.password = "fakePassword";
		Oauth2Token requestedToken = authenticationService.requestToken(resultUser);
        Document doc = new Document();
        if (Directory.Exists(inputdirPath))
        {
            string[] docFilePath = Directory.GetFiles(@inputdirPath);
            doc.filePath = docFilePath[0];
        }
        Document document = documentService.create(requestedToken, doc);
        
		System.Drawing.Image img = SNDotNetSDK.Properties.Resources.SignatureImage;
        string encodedString = ImageToBase64(img, ImageFormat.Jpeg);
		Signature signature = new Signature();
        signature.x = 66;
        signature.y = 21;
        signature.width = 117;
        signature.height = 23;
        signature.page_number = 1;
        signature.data = encodedString;
		List<Fields> signatureList = new List<Fields>();
        signatureList.Add(signature);
		
		Text text = new Text();
        text.size = 30;
        text.x = 61;
        text.y = 72;
        text.page_number = 0;
        text.font = "Arial";
        text.data = "A SAMPLE TEXT FIELD";
        text.line_height = 9.075;
		List<Fields> textsList = new List<Fields>();
        textsList.Add(text);
		
		Checkbox checks = new Checkbox();
        checks.width = 20;
        checks.height = 20;
        checks.x = 234;
        checks.y = 500;
        checks.page_number = 0;
		List<Fields> checksList = new List<Fields>();
        checksList.Add(checks);
		
		Dictionary<string, List<Fields>> fieldsMap = new Dictionary<string, List<Fields>>();
        fieldsMap.Add("signatures",signatureList);
        fieldsMap.Add("texts", textsList);
        fieldsMap.Add("checks", checksList);
		
		Document resultDoc = documentService.updateDocument(requestedToken, fieldsMap, document.id);
		Document getDoc = documentService.getDocumentbyId(requestedToken, resultDoc.id);

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
		string resinvite = documentService.roleBasedInvite(requestedToken, emailSignature, document.id);
        Console.WriteLine("success");
}
'''
##### Cancel Invite
'''
public void cancelInvite()
{
        string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";
        Config config = new Config(apibase, clientID, clientSecret);
		User resultUser = userService.create(user);
		resultUser.password = "fakePassword";
		Oauth2Token requestedToken = authenticationService.requestToken(resultUser);
        Document doc = new Document();
        if (Directory.Exists(inputdirPath))
        {
            string[] docFilePath = Directory.GetFiles(@inputdirPath);
            doc.filePath = docFilePath[0];
        }
        Document document = documentService.create(requestedToken, doc);
        string toEmail = "deepak" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        Invitation invitation = new Invitation();
        invitation.from = resultUser.email;
        invitation.to = toEmail;
		string resinvite = documentService.invite(requestedToken, invitation, document.id);
		string cancelinvite = documentService.cancelInvite(requestedToken, document.id);
        Console.WriteLine("success");
}
'''
##### Download Document as PDF
'''
public void downLoadDocumentAsPDF()
{
        string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";
        Config config = new Config(apibase, clientID, clientSecret);
		User resultUser = userService.create(user);
		resultUser.password = "fakePassword";
		Oauth2Token requestedToken = authenticationService.requestToken(resultUser);
        Document doc = new Document();
        if (Directory.Exists(inputdirPath))
        {
            string[] docFilePath = Directory.GetFiles(@inputdirPath);
            doc.filePath = docFilePath[0];
        }
        Document document = documentService.create(requestedToken, doc);
        Document resdoc = documentService.downLoadDocumentAsPDF(requestedToken, document.id);
		Console.WriteLine("Document Link", resdoc.link);
}
'''
##### Get Document History
'''
public void getDocumentHistory()
{
        string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";
        Config config = new Config(apibase, clientID, clientSecret);
		User resultUser = userService.create(user);
		resultUser.password = "fakePassword";
		Oauth2Token requestedToken = authenticationService.requestToken(resultUser);
        Document doc = new Document();
        if (Directory.Exists(inputdirPath))
        {
            string[] docFilePath = Directory.GetFiles(@inputdirPath);
            doc.filePath = docFilePath[0];
        }
        Document document = documentService.create(requestedToken, doc);
        DocumentHistory[] dochistory = documentService.getDocumentHistory(requestedToken, document.id);
        Console.WriteLine("Ip Address :", dochistory[0].ip_address);
}
'''
##### Create Template
'''
public void createTemplate()
{
        string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";
        Config config = new Config(apibase, clientID, clientSecret);
		User resultUser = userService.create(user);
		resultUser.password = "fakePassword";
		Oauth2Token requestedToken = authenticationService.requestToken(resultUser);
        Document doc = new Document();
        if (Directory.Exists(inputdirPath))
        {
            string[] docFilePath = Directory.GetFiles(@inputdirPath);
            doc.filePath = docFilePath[0];
        }
        Document document = documentService.create(requestedToken, doc);
        Template template = new Template();
        template.document_id = document.id;
        template.document_name = "New Template";

        Template resultTemplate = documentService.createTemplate(requestedToken, template);
        Console.WriteLine("template create result", resultTemplate.id);
}
'''
##### Create Document from Template
'''
public void createNewDocumentFromTemplate()
{
        string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";
        Config config = new Config(apibase, clientID, clientSecret);
		User resultUser = userService.create(user);
		resultUser.password = "fakePassword";
		Oauth2Token requestedToken = authenticationService.requestToken(resultUser);
        Document doc = new Document();
        if (Directory.Exists(inputdirPath))
        {
            string[] docFilePath = Directory.GetFiles(@inputdirPath);
            doc.filePath = docFilePath[0];
        }
        Document document = documentService.create(requestedToken, doc);
        Template template = new Template();
        template.document_id = document.id;
        template.document_name = "New Template";

        Template resultTemplate = documentService.createTemplate(requestedToken, template);
        resultTemplate.document_name = "Copy Template-PostDoc28";
		Template copyTemplate = documentService.createNewDocumentFromTemplate(requestedToken, resultTemplate);
        Console.WriteLine("Document Id", copyTemplate.id);
}
'''
##### Download Collapsed Document
'''
public void downloadCollapsedDocument()
{
        string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";
        Config config = new Config(apibase, clientID, clientSecret);
		User resultUser = userService.create(user);
		resultUser.password = "fakePassword";
		Oauth2Token requestedToken = authenticationService.requestToken(resultUser);
        Document doc = new Document();
        if (Directory.Exists(inputdirPath))
        {
            string[] docFilePath = Directory.GetFiles(@inputdirPath);
            doc.filePath = docFilePath[0];
        }
        Document document = documentService.create(requestedToken, doc);
        byte[] docarr = documentService.downloadCollapsedDocument(requestedToken, document.id);
        if(Directory.Exists(outputdirPath))
        {
            string dest = outputdirPath + @"\" + document.id + ".pdf";
            File.WriteAllBytes(dest, docarr);
        }
        Console.WriteLine("Document Content", docarr.Length.ToString());
}
'''
##### Delete Document
'''
public void deleteDocument()
{
        string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";
        Config config = new Config(apibase, clientID, clientSecret);
		User resultUser = userService.create(user);
		resultUser.password = "fakePassword";
		Oauth2Token requestedToken = authenticationService.requestToken(resultUser);
        Document doc = new Document();
        if (Directory.Exists(inputdirPath))
        {
            string[] docFilePath = Directory.GetFiles(@inputdirPath);
            doc.filePath = docFilePath[0];
        }
        Document document = documentService.create(requestedToken, doc);
        string confirm = documentService.deleteDocument(requestedToken, document.id);
        Console.WriteLine("success");
}
'''
##### Merge Documents
'''
public void mergeDocuments()
{
        string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";
        Config config = new Config(apibase, clientID, clientSecret);
		User resultUser = userService.create(user);
		resultUser.password = "fakePassword";
		Oauth2Token requestedToken = authenticationService.requestToken(resultUser);
        Document doc1 = new Document();
        Document doc2 = new Document();
        if (Directory.Exists(inputdirPath) && Directory.GetFiles(@inputdirPath).Length>=2)
        {
            string[] docFilePath = Directory.GetFiles(@inputdirPath);
            doc1.filePath = docFilePath[0];
            doc2.filePath = docFilePath[1];
        }
        Document document1 = documentService.create(requestedToken, doc1);
        Document document2 = documentService.create(requestedToken, doc2);
        List<string> docIds = new List<string>();
        docIds.Add(document1.id);
        docIds.Add(document2.id);
        Hashtable myMergeMap = new Hashtable();
        myMergeMap.Add("document_ids", docIds);

        byte[] res = documentService.mergeDocuments(requestedToken, myMergeMap);
        if (Directory.Exists(outputdirPath))
        {
            string dest = outputdirPath + @"\Merge" + (document1.id.Substring(1, 4) + document2.id.Substring(1, 4)) + ".pdf";
            File.WriteAllBytes(dest, res);
        }
        Console.WriteLine("Document Content", res.Length.ToString());
}
'''
##### Create Event Subscriptions
'''
public void createEventSubscription()
{
        string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";
        Config config = new Config(apibase, clientID, clientSecret);
		User resultUser = userService.create(user);
		resultUser.password = "fakePassword";
		Oauth2Token requestedToken = authenticationService.requestToken(resultUser);
		EventSubscription evs = new EventSubscription();
        evs.Event = "document.create";
        evs.callback_url = "https://www.myapp.com/path/to/callback.php";
        EventSubscription res = documentService.createEventSubscription(requestedToken, evs);
        Console.WriteLine("Subscription Id Created", res.id);
}
'''
##### Delete Event Subscriptions
'''
public void deleteEventSubscription()
{
        string randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";
        Config config = new Config(apibase, clientID, clientSecret);
		User resultUser = userService.create(user);
		resultUser.password = "fakePassword";
		Oauth2Token requestedToken = authenticationService.requestToken(resultUser);
		EventSubscription evs = new EventSubscription();
        evs.Event = "document.create";
        evs.callback_url = "https://www.myapp.com/path/to/callback.php";
        EventSubscription res = documentService.createEventSubscription(requestedToken, evs);
		EventSubscription deleteEvent = documentService.deleteEventSubscription(requestedToken, res.id);
        Console.WriteLine("Deleted");
}
'''

# Additional Contact Information

##### SUPPORT
To contact SignNow support, please email [support@signnow.com](mailto:support@signnow.com).

##### SALES
For pricing information, please call (800) 831-2050 or email [sales@signnow.com](mailto:sales@signnow.com).
