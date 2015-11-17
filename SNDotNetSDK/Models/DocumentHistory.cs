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
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }
        [JsonProperty("client_app_name")]
        public string ClientAppName { get; set; }
        [JsonProperty("user_agent")]
        public string UserAgent { get; set; }
        [JsonProperty("origin")]
        public string Origin { get; set; }
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("client_timestamp")]
        public string ClientTimeStamp { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
    }
}