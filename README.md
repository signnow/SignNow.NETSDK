SignNow
===========
SignNow .NET SDK

#### About SignNow
SignNow by Barracuda is an eSigning platform that offers a cloud version, a physical appliance and also a virtual appliance. Backed by Barracuda’s industry-leading security infrastructure, SignNow is fully compliant with eSigning laws and encrypts all data in transit. Users can share, manage and access their documents with confidence. It’s never been easier to get legally binding signatures from customers, partners, and employees - in seconds using any device.

#### API Contact Information
If you have questions about the SignNow API, please visit https://techlib.barracuda.com/SignNow/RestEndpointsAPI or email [api@signnow.com](mailto:api@signnow.com).

See additional contact information at the bottom.

Installation
==============

```
PM> Install-Package SignNow
```

Note. You will need Newtonsoft.Json to work with the results returned by each method as you will see in the Examples.

```
PM> Install-Package Newtonsoft.Json
```

Setup
==============
```csharp
using SignNow;
using Newtonsoft.Json.Linq;
using System.IO;

SignNow.Config.init("YOUR CLIENT ID", "YOUR CLIENT SECRET", "https://api-eval.signnow.com");
```

Examples
==========

To run the examples you will need an API key. You can get one here [https://signnow.com/l/api/request_information](https://signnow.com/l/api/request_information). For a full list of accepted parameters, refer to the SignNow REST Endpoints API guide: [https://techlib.barracuda.com/SignNow/RestEndpointsAPI](https://techlib.barracuda.com/SignNow/RestEndpointsAPI).

#OAuth2

## Request OAuth Token
```csharp
JObject OAuthRes = SignNow.OAuth2.RequestToken("YOUR USERNAME", "YOUR PASSWORD");
```

## Verify OAuth Token
```csharp
JObject OAuthVRes = SignNow.OAuth2.Verify(AccessToken);
```

# User

## Create New User
```csharp
JObject newAccountRes = SignNow.User.Create("name@domain.com", "(123) 123-1234", "Firstname", "Lastname");
```

## Retreive User Account Information
```csharp
JObject accountRes = SignNow.User.Get(AccessToken);
```

# Document

## Get Document
```csharp
//without annotations
JObject docRes = SignNow.Document.Get(AccessToken, "YOUR DOCUMENT ID");

//with annotations
JObject docRes = SignNow.Document.Get(AccessToken, "YOUR DOCUMENT ID", true);
```

## Create New Document
```csharp
JObject newDocRes = SignNow.Document.Create(AccessToken, "pdf-sample.pdf");
```

## Create New Document and Extract the Fields
```csharp
JObject newDocExtRes = SignNow.Document.Create(AccessToken, "Example Fields.docx", true);
```

## Update Document
```csharp
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

JObject updateDocRes = SignNow.Document.Update(AccessToken, "YOUR DOCUMENT ID", dataObj);
```

## Delete Document
```csharp
JObject deleteDocRes = SignNow.Document.Delete(AccessToken, "YOUR DOCUMENT ID");
```

## Download Document
```csharp
JObject downloadDocRes = SignNow.Document.Download(AccessToken, "YOUR DOCUMENT ID", "/", "sample");
```

## Send Free Form Invite
```csharp
dynamic inviteDataObj = new
{
  from = "account_email@domain.com",
  to = "name@domain.com"
};

JObject sendFreeFormInviteRes = SignNow.Document.Invite(AccessToken, "YOUR DOCUMENT ID", inviteDataObj);
```

## Send Role-based Invite
```csharp
dynamic inviteDataObj = new {
  to = new [] {
    new {
      email = "name@domain.com",
      role_id = "",
      role = "Role 1",
      order = 1,
      authentication_type = "password",
      password = "SOME PASSWORD",
      expiration_days = 15,
      reminder = 5
    },
    new {
      email = "name@domain.com",
      role_id = "",
      role = "Role 2",
      order = 2,
      authentication_type = "password",
      password = "SOME PASSWORD",
      expiration_days = 30,
      reminder = 10
    }
  },
  from = "your_account_email@domain.com",
  cc = new [] {
    "name@domain.com"
  },
  subject = "YOUR SUBJECT",
  message = "YOUR MESSAGE"
};

JObject sendRoleBasedInviteRes = SignNow.Document.Invite(AccessToken, DocumentId, inviteDataObj);
```

## Cancel Invite
```csharp
JObject cancelInviteRes = SignNow.Document.CancelInvite(AccessToken, "YOUR DOCUMENT ID");
```

## Merge Existing Documents
```csharp
dynamic mergeDocsObj = new
{
  name = "My New Merged Doc",
  document_ids = new[] { "YOUR DOCUMENT ID", "YOUR DOCUMENT ID" }
};

JObject mergeDocsRes = SignNow.Document.Merge(AccessToken, mergeDocsObj, "/", "sample-merge");
```

## Document History
```csharp
JArray docHistoryRes = SignNow.Document.History(AccessToken, "YOUR DOCUMENT ID");
```

# Template

## Create Template
```csharp
JObject newTemplateRes = SignNow.Template.Create(AccessToken, "YOUR DOCUMENT ID", "My New Template");
```

## Copy Template
```csharp
JObject copyTemplateRes = SignNow.Template.Copy(AccessToken, "YOUR TEMPLATE ID", "My Copy Template Doc");
```

# Folder

Filters  | Values
------------- | -------------
```signing-status```  | ```waiting-for-me```, ```waiting-for-others```, ```signed```, ```pending```
```document-updated```  | ```new Date()```
```document-created```  | ```new Date()```

Sort  | Values
------------- | -------------
```document-name```  | ```asc```/```desc```
```updated```  | ```asc```/```desc```
```created```  | ```asc```/```desc```

## List Folders
```csharp
JObject listFoldersRes = SignNow.Folder.List(AccessToken);
```

## Get Folder
```csharp
JObject getFolderRes = SignNow.Folder.Get(AccessToken, "YOUR FOLDER ID");
```

# Webhook

## Create Webhook

Events  | Description
------------- | -------------
```document.create```  | Webhook is triggered when a document is uploaded to users account in SignNow
```document.update```  | Webhook is triggered when a document is updated (fields added, text added, signature added, etc.)
```document.delete```  | Webhook is triggered when a document is deleted from
```invite.create```  | Webhook is triggered when an invitation to a SignNow document is created.
```invite.update```  | Webhook is triggered when an invite to Signnow document is updated. Ex. A signer has signed the document.

```csharp
JObject createWebhookRes = SignNow.Webhook.Create(AccessToken, "document.create", "YOUR URL");
```

## List Webhooks
```csharp
JObject listWebhooksRes = SignNow.Webhook.List(AccessToken);
```

## Delete Webhook
```csharp
JObject deleteWebhookRes = SignNow.Webhook.Delete(AccessToken, "YOUR WEBHOOK ID");
```

# Link

## Create Link
```csharp
JObject createLinkRes = SignNow.Link.Create(AccessToken, "YOUR DOCUMENT ID");
```

# Updates
- 1/21/2016 - Every method now contains an additional ResultFormat argument that allows you to specify JSON (default) or XML.
- 2/10/2016 - SignNow.Document.Invite now has an optional send email argument, updated documentation with new Role-based invite example.

# Additional Contact Information

##### SUPPORT
 [https://university.signnow.com/](https://university.signnow.com/).

##### SALES
For pricing information, please call (800) 831-2050 or email [sales@signnow.com](mailto:sales@signnow.com).
