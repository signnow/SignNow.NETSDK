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
    public class OAuth2
    {
        /// <summary>
        /// Request an Access Token using the User's Credentials
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        /// <param name="Scope">A space delimited list of API URIs e.g. "user%20documents%20user%2Fdocumentsv2"</param>
        /// <returns>New Access Token, Token Type, Expires In, Refresh Token, ID, Scope</returns>
        public static JObject RequestToken(string Email, string Password, string Scope = "*")
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            var request = new RestRequest("/oauth2/token", Method.POST)
                .AddHeader("Accept", "application/json")
                .AddHeader("Authorization", "Basic " + Config.EncodedClientCredentials)
                .AddHeader("Content-Type", "application/x-www-form-urlencoded")
                .AddParameter("username", Email)
                .AddParameter("password", Password)
                .AddParameter("grant_type", "password")
                .AddParameter("scope", Scope);

                request.RequestFormat = DataFormat.Json;

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
        /// Verify a User's Access Token
        /// </summary>
        /// <param name="AccessToken">User's Access Token</param>
        /// <returns>Access Token, Token Type, Expires In, Refresh Token, Scope</returns>
        public static JObject Verify(string AccessToken)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            var request = new RestRequest("/oauth2/token", Method.GET)
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
