
namespace SNDotNetSDK.Models
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This model obejct being used to get the OAuth2 access tokens to interact with SignNow API .
     */
    public class Oauth2Token
    {
        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public string RefreshToken { get; set; }

        public string Scope { get; set; }

        public int ExpiresIn { get; set; }

        public string Error { get; set; }

        public int Code { get; set; }
    }
}