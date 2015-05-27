using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using MvcApplicationForMSTranslateExample.AdminToken;

namespace WebApplication
{
    public class HTTPInterface_example
    {
        private void ExampleCode1 ()
        {
            AdmAccessToken admToken = AdminToken.getAdminToken();
            string text = "Use pixels to express measurements for padding and margins.";
            string from = "en";
            string to = "de";

            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" + System.Web.HttpUtility.UrlEncode(text) + "&from=" + from + "&to=" + to;
            string authToken = "Bearer" + " " + admToken.access_token;

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Headers.Add("Authorization", authToken);
        
            //part2 two
            WebResponse response = null;
            try
            {
                response = httpWebRequest.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                    string translation = (string)dcs.ReadObject(stream);
                    Console.WriteLine("Translation for source text '{0}' from {1} to {2} is", text, "en", "de");
                    Console.WriteLine(translation);
                }
            }
            catch
            {
            }
        }

    }
}