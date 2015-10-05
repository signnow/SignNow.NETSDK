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
        public User create(User user) {
        User createdUser = null;
        try {
            var client = new RestClient();
            client.BaseUrl = config.getApiBase();

            var request = new RestRequest("/user", Method.POST)
                    .AddHeader("Accept", "application/json")
                    .AddHeader("Authorization", "Basic " + config.getBase64EncodedClientCredentials())
                    .AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(user);

            var httpResponse = client.Execute(request);
       
            string json = httpResponse.Content.ToString();
            createdUser = JsonConvert.DeserializeObject<User>(json);
        } 
        catch (Exception ex) {
            throw new SystemException();
        }
        return createdUser;
       }

        /*
         * This method is used to retrieve (GET) a User in the SignNow Application.
         */
        public User get(string access_token)
        {
        User getUser = null;
        try {
            var client = new RestClient();
            client.BaseUrl = config.getApiBase();

            var request = new RestRequest("/user", Method.GET)
                .AddHeader("Accept", "application/json")
                .AddHeader("Authorization", "Bearer " + access_token);

            var httpResponse = client.Execute(request);

            string json = httpResponse.Content.ToString();
            getUser = JsonConvert.DeserializeObject<User>(json);
        }
        catch (Exception ex)
        {
            throw new SystemException();
        }
        return getUser;
      }
    }
}