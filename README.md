#### About SignNow
SignNow by Barracuda is an eSigning platform that offers a cloud version, a physical appliance and also a virtual appliance. Backed by Barracuda’s industry-leading security infrastructure, SignNow is fully compliant with eSigning laws and encrypts all data in transit. Users can share, manage and access their documents with confidence. It’s never been easier to get legally binding signatures from customers, partners, and employees - in seconds using any device.

#### API Contact Information
If you have questions about the SignNow API, please visit https://techlib.barracuda.com/SignNow/RestEndpointsAPI or email [api@signnow.com](mailto:api@signnow.com).

See additional contact information at the bottom.

####Installation
To install CudaSign, run the following command in the Package Manager Console

PM> Install-Package CudaSign


####Examples
To run the examples you will need an API key. You can get one here https://signnow.com/l/api/request_information. 
For a full list of accepted parameters, refer to the SignNow REST Endpoints API guide: https://techlib.barracuda.com/SignNow/RestEndpointsAPI.
```
Refer to Test Package for code samples.
```

The SDK is accessed using "SNDotNetSDK" Namespace:
```
using SNDotNetSDK;
using SNDotNetSDK.ServiceImpl;
using SNDotNetSDK.Models;
```
Every resource returns either the result parameter or the error parameter(if occurs).

## Initialize
```
Config config = new Config("<ApiBAse>", "<Client-Id>", "<Client-Secret>");
CudaSign cudasign = new CudaSign(config);
```

## Create a User
```
public void CreateUser()
{
        String randomEmail = "lukeskywalker" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        User user = new User();
        user.Email = randomEmail;
        user.Password = "fakePassword";
        user.FirstName = "firstName";
        user.LastName = "LastName";
        User resultUser = cudasign.userService.Create(user);
		if(resultUser.Error == null)
		{
			Console.WriteLine(resultUser.Email);
		}
		else
		{
			Console.WriteLine("Error "+resultUser.Error+"occurs with status code "+resultUser.Code);
		}
}
```
## Get User
```
public void GetUser()
{
		User resultUser = cudasign.userService.Get(resultUser.OAuth2Token.AccessToken);
}
```
## Request Token
```
public void RequestToken()
{
		User user = new User();
		user.Email = resultUser.Email;
		user.Password = "fakePassword";
		Oauth2Token requestedToken = cudasign.authenticationService.RequestToken(user);       
}
```

## Verify Token
```
public void VerifyToken()
{
        Oauth2Token verifiedToken = cudasign.authenticationService.Verify(requestedToken.AccessToken);
}
```

## Create Document
```
public void CreateDocument()
{
		private string InputdirPath = "InputdirPath";
        Document doc = new Document();
        if(Directory.Exists(InputdirPath))
        {
            string[] DocFilePath = Directory.GetFiles(@InputdirPath);
            doc.filePath = DocFilePath[0];
        }
        Document document = cudasign.documentService.Create(requestedToken, doc);
		if(document.Error == null)
		{
			Console.WriteLine(document.Id);
		}
		else
		{
			Console.WriteLine("Error "+document.Error+"occurs with status code "+document.Code);
		}
}
```

## Create Document And Extract Fields
```
public void CreateDocumentFieldExtract()
{
        private string InputdirPath = "InputdirPath";
        Document doc = new Document();
        if(Directory.Exists(InputdirPath))
        {
            string[] DocFilePath = Directory.GetFiles(@InputdirPath);
            doc.filePath = DocFilePath[0];
        }
        Document document = cudasign.documentService.CreateDocumentFieldExtract(requestedToken, doc);
		if(document.Error == null)
		{
			Console.WriteLine(document.Id);
		}
		else
		{
			Console.WriteLine("Error "+document.Error+"occurs with status code "+document.Code);
		}
}
```

## Get Document
```
public void GetDocument()
{
        Document[] resultDoc = cudasign.documentService.GetDocuments(requestedToken);
		if(resultDoc[0].Error != null)
		{
			Console.WriteLine("Error "+resultDoc[0].Error+"occurs with status code "+resultDoc[0].Code);
		}
}
```

## Get Document By Id
```
public void GetDocumentbyId()
{
        Document resultDoc = cudasign.documentService.GetDocumentbyId(requestedToken, document.id);
}
```

## Update Document
```
public void UpdateDocument()
{      
		Text text = new Text();
        text.Size = 30;
        text.X = 61;
        text.Y = 72;
        text.PageNumber = 0;
        text.Font = "Arial";
        text.Data = "A SAMPLE TEXT FIELD";
        text.LineHeight = 9.075;
		List<Fields> textsList = new List<Fields>();
        textsList.Add(text);
		
		Checkbox checks = new Checkbox();
        checks.Width = 20;
        checks.Height = 20;
        checks.X = 234;
        checks.Y = 500;
        checks.PageNumber = 0;
		List<Fields> checksList = new List<Fields>();
        checksList.Add(checks);
		
		Dictionary<string, List<Fields>> fieldsMap = new Dictionary<string, List<Fields>>();
        fieldsMap.Add("texts", textsList);
        fieldsMap.Add("checks", checksList);
		
		Document resultDoc = cudasign.documentService.UpdateDocument(requestedToken, fieldsMap, document.Id);
}
```

## Invite Signers
```
public void Invite()
{
        string toEmail = "deepak" + DateTime.Now.ToBinary().ToString() + "@mailinator.com";
        Invitation invitation = new Invitation();
        invitation.From = resultUser.Email;
        invitation.To = toEmail;
		string resinvite = cudasign.documentService.Invite(requestedToken, invitation, document.Id);
		if(!resinvite.Equals("Success"))
		{
			Console.WriteLine("Error occurs "+resinvite);
		}
}
```

## Role Based Invite
```
public void RoleBasedInvite()
{   
		Document getDoc = cudasign.documentService.GetDocumentbyId(requestedToken, document.Id);
	    Fields[] flds = getDoc.Fields;
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
        emailSignature.To = roleMapList;
        emailSignature.From = resultUser.Email;
        string[] ccuser = new string[] { "ccuser1@mailinator.com", "ccuser2@mailinator.com" };
        emailSignature.CC = ccuser;
        emailSignature.Message = resultUser.Email + " asked you to sign this document";
        emailSignature.Subject = "SignNow Invitation";
		string resinvite = cudasign.documentService.RoleBasedInvite(requestedToken, emailSignature, document.Id);
}
```

## Cancel Invite
```
public void CancelInvite()
{
        string cancelinvite = cudasign.documentService.CancelInvite(requestedToken, document.Id);
}
```

## Get Document History
```
public void GetDocumentHistory()
{
        DocumentHistory[] dochistory = cudasign.documentService.GetDocumentHistory(requestedToken, document.Id);
}
```

## Create Template
```
public void CreateTemplate()
{
        Template template = new Template();
        template.DocumentId = document.Id;
        template.DocumentName = "New Template";

        Template resultTemplate = cudasign.documentService.CreateTemplate(requestedToken, template);
}
```

## Create Document from Template
```
public void CreateNewDocumentFromTemplate()
{
        Template copyTemplate = cudasign.documentService.CreateNewDocumentFromTemplate(requestedToken, resultTemplate);
}
```

## Download Collapsed Document
```
public void DownloadCollapsedDocument()
{
		private string OutputdirPath = "outputdirPath";
        byte[] docarr = cudasign.documentService.DownloadCollapsedDocument(requestedToken, document.Id);
        if(Directory.Exists(OutputdirPath))
        {
            string dest = OutputdirPath + @"\" + document.Id + ".pdf";
            File.WriteAllBytes(dest, docarr);
        }
}
```

## Delete Document
```
public void DeleteDocument()
{
        string confirm = cudasign.documentService.DeleteDocument(requestedToken, document.Id);
}
```

## Merge Documents
```
public void MergeDocuments()
{
		private string OutputdirPath = "outputdirPath";
		
		List<string> docIds = new List<string>();
        docIds.Add(document1.Id);
        docIds.Add(document2.Id);
		
        Hashtable myMergeMap = new Hashtable();
        myMergeMap.Add("document_ids", docIds);
        byte[] res = cudasign.documentService.MergeDocuments(requestedToken, myMergeMap);
        if (Directory.Exists(OutputdirPath))
        {
            string dest = OutputdirPath + @"\Merge" + (document1.Id.Substring(1, 4) + document2.Id.Substring(1, 4)) + ".pdf";
            File.WriteAllBytes(dest, res);
        }
}
```

## Create Event Subscriptions
```
public void CreateEventSubscription()
{
        EventSubscription evs = new EventSubscription();
        evs.Event = "document.create";
        evs.CallbackUrl = "https://www.myapp.com/path/to/callback.php";
        EventSubscription res = cudasign.documentService.CreateEventSubscription(requestedToken, evs);
}
```

## Delete Event Subscriptions
```
public void DeleteEventSubscription()
{
        EventSubscription deleteEvent = cudasign.documentService.DeleteEventSubscription(requestedToken, res.Id);
}
```

# Additional Contact Information

##### SUPPORT
To contact SignNow support, please email [support@signnow.com](mailto:support@signnow.com).

##### SALES
For pricing information, please call (800) 831-2050 or email [sales@signnow.com](mailto:sales@signnow.com).
