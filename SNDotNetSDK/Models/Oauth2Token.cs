using Newtonsoft.Json;

namespace SNDotNetSDK.Models
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This model obejct being used to get the OAuth2 access tokens to interact with SignNow API .
     */
    public class Oauth2Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
    }
}