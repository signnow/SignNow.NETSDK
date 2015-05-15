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
     * This class is used to perform to Create / GET User details in the SignNow Application.
     */
    public class UserService : IUserService
    {
        static UserService()
        {
            string log4 = ConfigurationManager.AppSettings.Get("log4net.Config");
            FileInfo finfo = new FileInfo(log4);
            log4net.Config.XmlConfigurator.Configure(finfo);
        }

        private static readonly ILog logger = LogManager.GetLogger(typeof(UserService));

        /*
         * This method is used to create (POST) a User in the SignNow Application.
         */
        public User create(User user) {
        User createdUser = null;
        try {
            string requestBody = JsonConvert.SerializeObject(user, Formatting.Indented);
            logger.Debug("POSTING to /user \n" + requestBody);
            var client = new RestClient();
            client.BaseUrl = Config.getApiBase();

            var request = new RestRequest("/user", Method.POST)
                    .AddHeader("Accept", "application/json")
                    .AddHeader("Authorization", "Basic " + Config.getBase64EncodedClientCredentials())
                    .AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(user);

            var httpResponse = client.Execute(request);
       
            string json = httpResponse.Content.ToString();
            logger.Debug("Response Content is " + json);
            createdUser = JsonConvert.DeserializeObject<User>(json);
        } 
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            logger.Error(ex.Message);
            throw new SystemException();
        }
        return createdUser;
       }

        /*
         * This method is used to retrieve (GET) a User in the SignNow Application.
         */
        public User get(User user) {
        User getUser = null;
        try {
            String requestBody = JsonConvert.SerializeObject(user, Formatting.Indented);
            logger.Debug("GET user to /user \n" + requestBody);
            var client = new RestClient();
            client.BaseUrl = Config.getApiBase();

            var request = new RestRequest("/user", Method.GET)
                .AddHeader("Accept", "application/json")
                .AddHeader("Authorization", "Bearer " + user.oauth2Token.access_token);

            var httpResponse = client.Execute(request);

            string json = httpResponse.Content.ToString();
            logger.Debug("Response Content is " + json);
            getUser = JsonConvert.DeserializeObject<User>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            logger.Error(ex.Message);
            throw new SystemException();
        }
        return getUser;
    }
    }
}