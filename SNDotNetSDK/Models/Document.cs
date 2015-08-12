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
        private string Id;
        private Oauth2Token OAuth2Token;
        private String Link;
        private String FilePath;
        private Fields[] Fields;
        private string Error;

        private int Code;

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

        public string error
        {
            get { return Error; }
            set { this.Error = value; }
        }

        public int code
        {
            get { return Code; }
            set { this.Code = value; }
        }
    }
}