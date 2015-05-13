using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace com.signnow.sdk.model
{
    public class Radio : Fields
    {
        public int width { get; set; }
        public int height { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int page_number { get; set; }
        [JsonProperty("checked")]
        public int check { get; set; }
        public string value { get; set; }
        public string created { get; set; }
    }
}
