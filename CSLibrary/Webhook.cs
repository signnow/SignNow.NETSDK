using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CudaSign
{
    public class Webhook
    {
        /// <summary>
        /// Get a List of Current Webhook Subscriptions
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <returns>List of Subscriptions</returns>
        public static JObject List(string AccessToken)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            var request = new RestRequest("/event_subscription", Method.GET)
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

        /// <summary>
        /// Create Webhook that will be Triggered when the Specified Event Takes Place
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <param name="EventType">document.create, document.update, document.delete, invite.create, invite.update</param>
        /// <param name="CallbackUrl">The URL called when the even is triggered.</param>
        /// <returns>ID, Created, Updated</returns>
        public static JObject Create(string AccessToken, string EventType, string CallbackUrl)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            var request = new RestRequest("/event_subscription", Method.POST)
                .AddHeader("Accept", "application/json")
                .AddHeader("Authorization", "Bearer " + AccessToken);

            request.RequestFormat = DataFormat.Json;

            JObject jsonObj = new JObject { };
            jsonObj["event"] = EventType;
            jsonObj["callback_url"] = CallbackUrl;

            request.AddBody(jsonObj);

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
        /// Deletes a Webhook
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <param name="SubscriptionId"></param>
        /// <returns>{"status":"success"}</returns>
        public static JObject Delete(string AccessToken, string SubscriptionId)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Config.ApiHost);

            var request = new RestRequest("/event_subscription/" + SubscriptionId, Method.DELETE)
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
