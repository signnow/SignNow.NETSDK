using System;

namespace SNDotNetSDK.Models
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This class is used to create the document model object
     */
    public class Document
    {
        public string Id { get; set; }

        public Oauth2Token OAuth2Token { get; set; }

        public String Link { get; set; }

        public String FilePath { get; set; }

        public Fields[] Fields { get; set; }

        public string Error { get; set; }

        public int Code { get; set; }
    }
}