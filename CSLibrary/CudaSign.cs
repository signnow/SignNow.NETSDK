using System;

namespace CudaSign
{
    public class Config
    {
        internal static string EncodedClientCredentials = "";
        internal static string ApiHost = "";

        /// <summary>
        /// CudaSign Initialization
        /// </summary>
        /// <param name="Client">API Credentials - Client</param>
        /// <param name="Secret">API Credentials - Secret</param>
        /// <param name="ApiServer">API Server Path. Defaults to CudaSign EVALUATION if left blank.</param>
        public static void init(String Client, String Secret, String ApiServer = "")
        {
            ApiHost = (ApiServer != "") ? ApiServer : "https://api-eval.cudasign.com/";
            EncodedClientCredentials = encodeClientCredentials(Client, Secret);
        }

        public static string encodeClientCredentials(string Client, string Secret)
        {
            string idAndSecret = Client + ":" + Secret;
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(idAndSecret);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
