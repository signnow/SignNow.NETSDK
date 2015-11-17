using Newtonsoft.Json;

namespace SNDotNetSDK.Models
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This model object is used to set event subscription values for the account.
     */
    public class EventSubscription
    {
        [JsonProperty("event")]
        public string Event { get; set; }
        [JsonProperty("callback_url")]
        public string CallbackUrl { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }
        [JsonProperty("updated")]
        public string Updated { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
    }
}
