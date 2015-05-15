using System.Collections.Generic;
namespace com.signnow.sdk.model
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This model obejct being used to send the invitations to different users to sign the documents.
     */
    public class Invitation
    {
        public string from { get; set; }

        public string to { get; set; }

        public bool originator_pay { get; set; }
    }
}