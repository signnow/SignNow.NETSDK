using com.signnow.sdk.model;
using log4net;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Configuration;
using System.IO;

namespace com.signnow.sdk.service.impl
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This class is used to perform the OAuth2 token specific operations to access SignNow Application.
     */
    public class OAuth2TokenService : IAuthenticationService
    {
        static OAuth2TokenService()
        {
            string log4 = ConfigurationManager.AppSettings.Get("log4net.Config");
            FileInfo finfo = new FileInfo(log4);
            log4net.Config.XmlConfigurator.Configure(finfo);
        }

        private static readonly ILog logger = LogManager.GetLogger(typeof(OAuth2TokenService));

        /**
        * This method is used to request (POST)the OAuth2 token for a specific user to access SignNow Application.
        */
        public Oauth2Token requestToken(User user)
        {
            
            Oauth2Token requestedToken = null;
            try {
            string requestBody = JsonConvert.SerializeObject(user, Formatting.Indented);
            logger.Debug("POSTING to /oauth2/token \n" + requestBody);
            var client = new RestClient();
            client.BaseUrl = Config.getApiBase();

            var request = new RestRequest("/oauth2/token", Method.POST)
                    .AddHeader("Accept", "application/json")
                    .AddHeader("Authorization", "Basic " + Config.getBase64EncodedClientCredentials())
                    .AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("username", user.email)
                    .AddParameter("password", user.password)
                    .AddParameter("grant_type", "password");

            var httpResponse = client.Execute(request);
       
            string json = httpResponse.Content.ToString();
            logger.Debug("Response Content is " + json);
            requestedToken = JsonConvert.DeserializeObject<Oauth2Token>(json);
        } 
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            logger.Error(ex.Message);
            throw new SystemException();
        }
        return requestedToken;
        }

        /**
        * This method is used to verify (GET) the OAuth2 token for a specific user to access SignNow Application.
        */
        public Oauth2Token verify(Oauth2Token token) {
        Oauth2Token verifyToken = null;
        try {
            string requestBody = JsonConvert.SerializeObject(token, Formatting.Indented);
            logger.Debug("GET to /oauth2/token \n" + requestBody);
            var client = new RestClient();
            client.BaseUrl = Config.getApiBase();

            var request = new RestRequest("/oauth2/token", Method.GET)
                    .AddHeader("Authorization", "Bearer " + token.access_token)
                    .AddHeader("Accept", "application/json");
            
            var httpResponse = client.Execute(request);
       
            string json = httpResponse.Content.ToString();
            logger.Debug("Response Content is " + json);
            verifyToken = JsonConvert.DeserializeObject<Oauth2Token>(json);
        } 
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            logger.Error(ex.Message);
            throw new SystemException();
        }
        return verifyToken;
        }
    }
}
