
namespace SNDotNetSDK.Models
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This model class used to create the User in SignNow Application
     */
    public class User {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Verified { get; set; }

        public Oauth2Token OAuth2Token { get; set; }

        public string Error { get; set; }

        public int Code { get; set; }
    }
}