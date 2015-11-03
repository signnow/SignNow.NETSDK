using System.Collections.Generic;
using System.Collections;

namespace SNDotNetSDK.Models
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This model object is used to set the email for the document invite.
     */
    public class EmailSignature
    {
        public List<Hashtable> To { get; set; }

        public string From { get; set; }

        public string[] CC { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}