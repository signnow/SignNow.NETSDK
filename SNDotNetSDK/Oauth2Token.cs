
namespace com.signnow.sdk.model
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This model obejct being used to get the OAuth2 access tokens to interact with SignNow API .
     */
    public class Oauth2Token
    {
    
        private string accessToken;

        private string tokenType;

        private string refreshToken;

        private string scope;

        private int expiresIn;

        public string access_token
	    {
            get { return accessToken; }
		    set { this.accessToken = value; }
	    }

        public string token_type
	    {
		    get {   return tokenType; }
		    set {   this.tokenType = value; }
	    }

        public string refresh_token
	    {
		    get {   return refreshToken; }
		    set {   this.refreshToken = value; }
	    }
	
	    public string Scope
	    {
		    get {   return scope; }
		    set {   this.scope = value; }
	    }

        public int expires_in
	    {
		    get {   return expiresIn; }
		    set {   this.expiresIn = value; }
	    }
    }
}