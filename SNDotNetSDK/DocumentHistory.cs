using Newtonsoft.Json;

namespace com.signnow.sdk.model
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This model object is used to get the document history of a specified document.
     */
    public class DocumentHistory
    {
        public string email { get; set; }
        public string ip_address { get; set; }
        public string created { get; set; }
        [JsonProperty("event")]
        public string Event { get; set; }
        public string client_app_name { get; set; }
        public string user_agent { get; set; }
        public string origin { get; set; }
        public string version { get; set; }
        public string client_timestamp { get; set; }
    }
}