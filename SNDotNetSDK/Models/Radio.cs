using Newtonsoft.Json;

namespace SNDotNetSDK.Models
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This model object is used to place the radio button on the document.
     */
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
