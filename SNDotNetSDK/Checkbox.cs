
namespace com.signnow.sdk.model
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This class is used to create the check box on the document
     */
    public class Checkbox : Fields
    {
        public int x { get; set; }

        public int y { get; set; }

        public int width { get; set; }

        public int height { get; set; }

        public int page_number { get; set; }
    }
}