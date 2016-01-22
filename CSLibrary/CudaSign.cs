using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <param name="Public">For Public API set True for Eval API set False</param>
        public static void init(String Client, String Secret, Boolean Public)
        {
            ApiHost = (Public) ? "https://api.cudasign.com/" : "https://api-eval.cudasign.com/";
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
