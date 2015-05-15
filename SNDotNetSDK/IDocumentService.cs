﻿using com.signnow.sdk.model;
using System.Collections;
using System.Collections.Generic;

namespace com.signnow.sdk.service
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This interface is used to perform to Document specific operations in the SignNow Application.
     */
    public interface IDocumentService
    {
        Document create(Oauth2Token token, Document documentPath);

        Document[] getDocument(Oauth2Token token);

        Document getDocumentbyId(Oauth2Token token, string id);

        Document updateDocument(Oauth2Token token, Dictionary<string, List<Fields>> fieldsMap, string id);

        string invite(Oauth2Token token, Invitation invitation, string id);

        string roleBasedInvite(Oauth2Token token, EmailSignature emailSignature, string id);

        string cancelInvite(Oauth2Token token, string id);

        Document downLoadDocumentAsPDF(Oauth2Token token, string id);

        DocumentHistory[] getDocumentHistory(Oauth2Token token, string id);

        Template createTemplate(Oauth2Token token, Template template);

        Template createNewDocumentFromTemplate(Oauth2Token token, Template template);

        byte[] downloadCollapsedDocument(Oauth2Token token, string id);

        string deleteDocument(Oauth2Token token, string id);

        byte[] mergeDocuments(Oauth2Token token, Hashtable myMergeMap);

        EventSubscription createEventSubscription(Oauth2Token token, EventSubscription events);

        EventSubscription deleteEventSubscription(Oauth2Token token, string id);

        Document createSimpleFieldTag(Oauth2Token token, Document documentPath);
    }
}