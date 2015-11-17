using System;
using Newtonsoft.Json;

namespace SNDotNetSDK.Models
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This class is used to create the document model object
     */
    public class Document
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public Oauth2Token OAuth2Token { get; set; }
        [JsonProperty("link")]
        public String Link { get; set; }
        [JsonProperty("file_path")]
        public String FilePath { get; set; }
        [JsonProperty("fields")]
        public Fields[] Fields { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
    }
}