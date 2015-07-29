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

        public string callback_url { get; set; }

        public string id { get; set; }

        public string created { get; set; }

        public string updated { get; set; }

        public string status { get; set; }
    }
}
