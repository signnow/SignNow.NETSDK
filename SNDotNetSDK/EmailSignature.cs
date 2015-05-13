using System.Collections.Generic;
using System.Collections;

namespace com.signnow.sdk.model
{
    public class EmailSignature
    {
        public List<Hashtable> to { get; set; }

        public string from { get; set; }

        public string[] cc { get; set; }

        public string subject { get; set; }

        public string message { get; set; }
    }
}
