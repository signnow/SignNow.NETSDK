
namespace SNDotNetSDK.Models
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This model class used to create the template based on the given document id.
     */
    public class Template
    {
        public string DocumentId { get; set; }

        public string DocumentName { get; set; }

        public string Id { get; set; }

        public string Error { get; set; }

        public int Code { get; set; }
    }
}
