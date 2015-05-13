using System;

namespace com.signnow.sdk.model
{
    public class Document
    {
        private string Id;
        private Oauth2Token OAuth2Token;
        private String Link;
        private String FilePath;
        private Fields[] Fields;

        public string id
        {
            get { return Id; }
            set { this.Id = value; }
        }

        public Oauth2Token oauth2Token
        {
            get { return OAuth2Token; }
            set { this.OAuth2Token = value; }
        }

        public string link
        {
            get { return Link; }
            set { this.Link = value; }
        }

        public string filePath
        {
            get { return FilePath; }
            set { this.FilePath = value; }
        }

        public Fields[] fields
        {
            get { return Fields; }
            set { this.Fields = value; }
        }
    }
}
