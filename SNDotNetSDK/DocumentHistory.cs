using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace com.signnow.sdk.model
{
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
