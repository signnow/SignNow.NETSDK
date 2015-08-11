 SignNowDotNetSDK
 ===============
A UnitTest Project using REST Endpoints API.

#### About SignNow
SignNow by Barracuda is an eSigning platform that offers a cloud version, a physical appliance and also a virtual appliance. Backed by Barracuda’s industry-leading security infrastructure, SignNow is fully compliant with eSigning laws and encrypts all data in transit. Users can share, manage and access their documents with confidence. It’s never been easier to get legally binding signatures from customers, partners, and employees - in seconds using any device.

#### API Contact Information
If you have questions about the SignNow API, please visit https://techlib.barracuda.com/SignNow/RestEndpointsAPI or email [api@signnow.com](mailto:api@signnow.com).

See additional contact information at the bottom.


##User

To Create and Retrieve the User to/from SigNow Application use the UserServiceTest.

##Token 

To perform Token related operations use the Oauth2TokenServiceTest.

##Document Service

To perform and test the Document specific test operations use DocumentServiceTest.

For a full list of accepted parameters, refer to the SignNow REST Endpoints API guide: [https://techlib.barracuda.com/SignNow/RestEndpointsAPI](https://techlib.barracuda.com/SignNow/RestEndpointsAPI).

## Initialize
```
Config config = new Config("ApiBAse", "Client-Id", "Client-Secret");
CopyClient copyclient = new CopyClient(config);
```

## Create a User
```
public void createUser()
{
        String randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.email = randomEmail;
        user.password = "fakePassword";
        user.first_name = "firstName";
        user.last_name = "LastName";
        User resultUser = copyclient.userService.create(user);
}
```
## Get User
```
public void getUser()
{
		User resultUser = copyclient.userService.get(resultUser.oauth2Token.access_token);
}
```
## Request Token
```
public void requestToken()
{
		User user = new User();
		user.email = resultUser.email;
		user.password = "fakePassword";
		Oauth2Token requestedToken = copyclient.authenticationService.requestToken(user);       
}
```

## Verify Token
```
public void verifyToken()
{
        Oauth2Token verifiedToken = copyclient.authenticationService.verify(requestedToken.access_token);
}
```

## Create Document
```
public void createDocument()
{
		private string inputdirPath = "inputdirPath";
        Document doc = new Document();
        if(Directory.Exists(inputdirPath))
        {
            string[] docFilePath = Directory.GetFiles(@inputdirPath);
            doc.filePath = docFilePath[0];
        }
        Document document = copyclient.documentService.create(requestedToken, doc);
}
```

## Get Document
```
public void getDocument()
{
        Document[] resultDoc = copyclient.documentService.getDocuments(requestedToken);
}
```

## Get Document By Id
```
public void getDocumentbyId()
{
        Document resultDoc = copyclient.documentService.getDocumentbyId(requestedToken, document.id);
}
```

## Update Document
```
public void updateDocument()
{       
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
		
		Document resultDoc = copyclient.documentService.updateDocument(requestedToken, fieldsMap, document.id);
}
```

## Invite Signers
```
public void invite()
{
        string toEmail = "deepak" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        Invitation invitation = new Invitation();
        invitation.from = resultUser.email;
        invitation.to = toEmail;
		string resinvite = copyclient.documentService.invite(requestedToken, invitation, document.id);
}
```

## Role Based Invite
```
public void roleBasedInvite()
{   
		Document getDoc = copyclient.documentService.getDocumentbyId(requestedToken, document.id);
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
}
```

## Cancel Invite
```
public void cancelInvite()
{
        string cancelinvite = copyclient.documentService.cancelInvite(requestedToken, document.id);
}
```

## Get Document History
```
public void getDocumentHistory()
{
        DocumentHistory[] dochistory = copyclient.documentService.getDocumentHistory(requestedToken, document.id);
}
```

## Create Template
```
public void createTemplate()
{
        Template template = new Template();
        template.document_id = document.id;
        template.document_name = "New Template";

        Template resultTemplate = copyclient.documentService.createTemplate(requestedToken, template);
}
```

## Create Document from Template
```
public void createNewDocumentFromTemplate()
{
        Template copyTemplate = copyclient.documentService.createNewDocumentFromTemplate(requestedToken, resultTemplate);
}
```

## Download Collapsed Document
```
public void downloadCollapsedDocument()
{
		private string outputdirPath = "outputdirPath";
        byte[] docarr = copyclient.documentService.downloadCollapsedDocument(requestedToken, document.id);
        if(Directory.Exists(outputdirPath))
        {
            string dest = outputdirPath + @"\" + document.id + ".pdf";
            File.WriteAllBytes(dest, docarr);
        }
}
```

## Delete Document
```
public void deleteDocument()
{
        string confirm = copyclient.documentService.deleteDocument(requestedToken, document.id);
}
```

## Merge Documents
```
public void mergeDocuments()
{
		private string outputdirPath = "outputdirPath";
		
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
}
```

## Create Event Subscriptions
```
public void createEventSubscription()
{
        EventSubscription evs = new EventSubscription();
        evs.Event = "document.create";
        evs.callback_url = "https://www.myapp.com/path/to/callback.php";
        EventSubscription res = copyclient.documentService.createEventSubscription(requestedToken, evs);
}
```

## Delete Event Subscriptions
```
public void deleteEventSubscription()
{
        EventSubscription deleteEvent = copyclient.documentService.deleteEventSubscription(requestedToken, res.id);
}
```

# Additional Contact Information

##### SUPPORT
To contact SignNow support, please email [support@signnow.com](mailto:support@signnow.com).

##### SALES
For pricing information, please call (800) 831-2050 or email [sales@signnow.com](mailto:sales@signnow.com).
