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

        public string CallbackUrl { get; set; }

        public string Id { get; set; }

        public string Created { get; set; }

        public string Updated { get; set; }

        public string Status { get; set; }

        public string Error { get; set; }

        public int Code { get; set; }
    }
}
