using System;

namespace SNDotNetSDK.Configuration
{
    public class Config : IConfig
    {
        private string ApiBase;
        private string ClientId;
        private string ClientSecret;
        /**
         * Created by Deepak on 5/14/2015
         * 
         * This class is used to read the configuration parameters like, base endpointURL, clientID, clientSecret to access SignNow Application.
         */

        /// <summary>
        /// Instantiates a Copy config with all of the standard defaults
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="apiBase"></param>

        public Config(string apiBase, string clientId, string clientSecret) {
             ApiBase = apiBase;
             ClientId = clientId;
             ClientSecret = clientSecret;
        }

        public string getApiBase() {

            if (ApiBase.Equals("signnow.eval"))
            {
                return "https://capi-eval.signnow.com/api";
            }
            else if (ApiBase.Equals("signnow.public"))
            {
                return "https://api.signnow.com";
            }
            return ApiBase;
        }

        public String getBase64EncodedClientCredentials()
        {
            string idAndSecret = ClientId + ":" + ClientSecret;
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(idAndSecret);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}