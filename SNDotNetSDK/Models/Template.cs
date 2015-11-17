using Newtonsoft.Json;

namespace SNDotNetSDK.Models
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This model class used to create the template based on the given document id.
     */
    public class Template
    {
        [JsonProperty("document_id")]
        public string DocumentId { get; set; }
        [JsonProperty("document_name")]
        public string DocumentName { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
    }
}
