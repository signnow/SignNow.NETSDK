using System.Collections.Generic;
using Newtonsoft.Json;

namespace SNDotNetSDK.Models
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This model obejct being used to send the invitations to different users to sign the documents.
     */
    public class Invitation
    {
        [JsonProperty("from")]
        public string From { get; set; }
        [JsonProperty("to")]
        public string To { get; set; }
        
        public bool OriginatorPay { get; set; }
    }
}