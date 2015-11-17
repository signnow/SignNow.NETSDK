using Newtonsoft.Json;

namespace SNDotNetSDK.Models
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This class is used to create the check box on the document
     */
    public class Checkbox : Fields
    {
        [JsonProperty("x")]
        public int X { get; set; }
        [JsonProperty("y")]
        public int Y { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("page_number")]
        public int PageNumber { get; set; }
    }
}