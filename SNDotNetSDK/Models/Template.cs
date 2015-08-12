
namespace SNDotNetSDK.Models
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This model class used to create the template based on the given document id.
     */
    public class Template
    {
        public string document_id { get; set; }

        public string document_name { get; set; }

        public string id { get; set; }

        public string error { get; set; }

        public int code { get; set; }
    }
}
