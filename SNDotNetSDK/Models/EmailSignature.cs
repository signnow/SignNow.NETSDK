using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;

namespace SNDotNetSDK.Models
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This model object is used to set the email for the document invite.
     */
    public class EmailSignature
    {
        [JsonProperty("to")]
        public List<Hashtable> To { get; set; }
        [JsonProperty("from")]
        public string From { get; set; }
        [JsonProperty("cc")]
        public string[] CC { get; set; }
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}