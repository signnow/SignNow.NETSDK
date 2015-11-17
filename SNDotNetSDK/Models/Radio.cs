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
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("x")]
        public int X { get; set; }
        [JsonProperty("y")]
        public int Y { get; set; }
        [JsonProperty("page_number")]
        public int PageNumber { get; set; }

        [JsonProperty("checked")]
        public int Check { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }
    }
}
