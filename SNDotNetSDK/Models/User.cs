using Newtonsoft.Json;

namespace SNDotNetSDK.Models
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This model class used to create the User in SignNow Application
     */
    public class User {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("verified")]
        public string Verified { get; set; }

        public Oauth2Token OAuth2Token { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
    }
}