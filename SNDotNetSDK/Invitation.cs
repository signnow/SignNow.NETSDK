using System.Collections.Generic;
namespace com.signnow.sdk.model
{
    public class Invitation
    {
        public string from { get; set; }

        public string to { get; set; }

        public bool originator_pay { get; set; }
    }
}
