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
        public int Width { get; set; }

        public int Height { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int PageNumber { get; set; }

        [JsonProperty("checked")]
        public int Check { get; set; }

        public string Value { get; set; }

        public string Created { get; set; }
    }
}
