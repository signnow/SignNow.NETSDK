using Newtonsoft.Json;

namespace SNDotNetSDK.Models
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This model object is used to get the document history of a specified document.
     */
    public class DocumentHistory
    {
        public string Email { get; set; }

        public string IpAddress { get; set; }

        public string Created { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }

        public string ClientAppName { get; set; }

        public string UserAgent { get; set; }

        public string Origin { get; set; }

        public string Version { get; set; }

        public string ClientTimeStamp { get; set; }

        public string Error { get; set; }

        public int Code { get; set; }
    }
}