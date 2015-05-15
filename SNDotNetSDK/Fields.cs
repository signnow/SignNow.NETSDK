using System.Collections.Generic;
namespace com.signnow.sdk.model
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This model is useful to build the Fields Object to have different types of files like, Signature,CheckBox,Texts and Initials.
     */
    public class Fields
    {
        public int x { get; set; }

        public int y { get; set; }

        public int width { get; set; }

        public int height { get; set; }

        public int page_number { get; set; }

        public string role { get; set; }

        public bool required { get; set; }

        public string type { get; set; }

        public string role_id { get; set; }

        public List<Fields> radio { get; set; }

        public string email { get; set; }
    }
}