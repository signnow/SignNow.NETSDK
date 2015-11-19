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

        public static void init(String Client, String Secret, Boolean Public)
        {
            ApiHost = (Public) ? "https://api.cudasign.com/" : "https://api-eval.cudasign.com/";

            string idAndSecret = Client + ":" + Secret;
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(idAndSecret);
            EncodedClientCredentials = System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
