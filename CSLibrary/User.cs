using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CudaSign
{
    public class User
    {
        /// <summary>
        /// Creates a New User CudaSign Account
        /// </summary>
        /// <param name="Email">New User's Email Address</param>
        /// <param name="Password">New User's Password</param>
        /// <param name="FirstName">New User's First Name</param>
        /// <param name="LastName">New User's Last Name</param>
        /// <returns>The ID of the new user account and verification status.</returns>
        public static JObject Create(string Email, string Password, string FirstName = "", string LastName = "")
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            var request = new RestRequest("/user", Method.POST)
                .AddHeader("Accept", "application/json")
                .AddHeader("Authorization", "Basic " + Config.EncodedClientCredentials);

                request.RequestFormat = DataFormat.Json;
                request.AddBody(new { email = Email, password = Password, first_name = FirstName, last_name = LastName });

            var response = client.Execute(request);
            
            if (response.StatusCode == HttpStatusCode.OK)
            {
                dynamic results = JsonConvert.DeserializeObject(response.Content);
                return results;
            }
            else
            {
                Console.WriteLine(response.Content.ToString());
                dynamic jsonObject = new JObject();
                jsonObject.error = response.Content.ToString();
                return jsonObject;
            }
        }

        /// <summary>
        /// Retrieves a User Account
        /// </summary>
        /// <param name="AccessToken">User's Access Token</param>
        /// <returns>User Account Information</returns>
        public static JObject Get(string AccessToken)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            var request = new RestRequest("/user", Method.GET)
                .AddHeader("Accept", "application/json")
                .AddHeader("Authorization", "Bearer " + AccessToken);

            var response = client.Execute(request);
            

            if (response.StatusCode == HttpStatusCode.OK)
            {
                dynamic results = JsonConvert.DeserializeObject(response.Content);
                return results;
            }
            else
            {
                Console.WriteLine(response.Content.ToString());
                dynamic jsonObject = new JObject();
                jsonObject.error = response.Content.ToString();
                return jsonObject;
            }
        }
    }
}
