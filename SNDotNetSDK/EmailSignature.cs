using System.Collections.Generic;
using System.Collections;

namespace com.signnow.sdk.model
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This model object is used to set the email for the document invite.
     */
    public class EmailSignature
    {
        public List<Hashtable> to { get; set; }

        public string from { get; set; }

        public string[] cc { get; set; }

        public string subject { get; set; }

        public string message { get; set; }
    }
}