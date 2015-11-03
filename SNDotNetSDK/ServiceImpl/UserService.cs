using Newtonsoft.Json;
using RestSharp;
using SNDotNetSDK.Configuration;
using SNDotNetSDK.Models;
using SNDotNetSDK.Service;
using System;

namespace SNDotNetSDK.ServiceImpl
{
    /**
     * Created by Deepak on 5/14/2015
     * 
     * This class is used to perform to Create / GET User details in the SignNow Application.
     */
    public class UserService : IUserService
    {
        private Config config;
        public UserService(Config config)
        {
            this.config = config;
        }
        /*
         * This method is used to create (POST) a User in the SignNow Application.
         */
        public User Create(User user) {
        User createdUser = null;
        try {
            var client = new RestClient();
            client.BaseUrl = config.GetApiBase();

            var request = new RestRequest("/user", Method.POST)
                    .AddHeader("Accept", "application/json")
                    .AddHeader("Authorization", "Basic " + config.GetBase64EncodedClientCredentials())
                    .AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(user);

            var httpResponse = client.Execute(request);
       
            string json = httpResponse.Content.ToString();
            createdUser = JsonConvert.DeserializeObject<User>(json);
        } 
        catch (Exception ex) {
            Console.WriteLine(string.Format("Exception: {0}", ex.Message));
            throw;
        }
        return createdUser;
       }

        /*
         * This method is used to retrieve (GET) a User in the SignNow Application.
         */
        public User Get(string AccessToken)
        {
        User getUser = null;
        try {
            var client = new RestClient();
            client.BaseUrl = config.GetApiBase();

            var request = new RestRequest("/user", Method.GET)
                .AddHeader("Accept", "application/json")
                .AddHeader("Authorization", "Bearer " + AccessToken);

            var httpResponse = client.Execute(request);

            string json = httpResponse.Content.ToString();
            getUser = JsonConvert.DeserializeObject<User>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(string.Format("Exception: {0}", ex.Message));
            throw;
        }
        return getUser;
      }
    }
}