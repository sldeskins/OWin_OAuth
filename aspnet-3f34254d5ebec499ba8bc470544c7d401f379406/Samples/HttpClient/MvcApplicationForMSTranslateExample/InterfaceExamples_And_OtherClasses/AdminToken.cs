using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Web;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Threading;

namespace MvcApplicationForMSTranslateExample.AdmAuthentification
{
    //Obtaining an Access Token
    public class AdminToken
    {
        private string _textToDetect;
        public string TextToDetect
        {
            get
            {
                return _textToDetect;
            }
            set
            {
                _textToDetect = value;
            }
        }

        private string _exceptionMessageText;
        public string ExceptionMessageText
        {
            get
            {
                return _exceptionMessageText;
            }
            set
            {
                _exceptionMessageText = value;
            }
        }

        private string _detectedLanguage;

        public string DetectedLanguage
        {
            get
            {
                return _detectedLanguage;
            }
            set
            {
                _detectedLanguage = value;
            }
        }

        public AdmAccessToken getAdminToken ()
        {
            // 3. Make an HTTP POST request to the token service
            //After you register your application with Azure DataMarket, make an HTTP POST request to the token service to obtain the access token. The parameters for the token request are URL-encoded and passed in the HTTP request body. 
            //The following table lists the mandatory input parameters and their descriptions.
            //
            // Table 1: Token Request Input Parameters
            //Parameter	Description
            //client_id	Required. The client ID that you specified when you registered your application with Azure DataMarket.
            //client_secret	Required. The client secret value that you obtained when you registered your application with Azure DataMarket.
            //scope	Required. Use the URL http://api.microsofttranslator.com as the scope value for the Microsoft Translator API.
            //grant_type	Required. Use "client_credentials" as the grant_type value for the Microsoft Translator API.
            //
            //
            //        Table 2: Token Request Output Properties
            //Property	Description
            //access_token	The access token that you can use to authenticate you access to the Microsoft Translator API.
            //token_type	The format of the access token. Currently, Azure DataMarket returns http://schemas.xmlsoap.org/ws/2009/11/swt-token-profile-1.0 , which indicates that a Simple Web Token (SWT) token will be returned.
            //expires_in	The number of seconds for which the access token is valid.
            //scope	The domain for which this token is valid. For the Microsoft Translator API, the domain is http://api.microsofttranslator.com.
            //
            //

            //        Using the Access Token
            //Bing AppID mechanism is deprecated and is no longer supported. As mentioned above, you must obtain an access token to use the Microsoft Translator API. The access token is more secure, OAuth standard compliant, and more flexible. Users who are using Bing AppID are strongly recommended to get an access token as soon as possible. 
            //The value of access token can be used for subsequent calls to the Microsoft Translator API. The access token expires after 10 minutes. It is always better to check elapsed time between time at which token issued and current time. If elapsed time exceeds 10 minute time period renew access token by following obtaining access token procedure. 
            //Remember the following points about using the access token:
            //Use the prefix "Bearer" + " " + the value of the access_token property as the Authorization header to the calls to the Microsoft Translator API.
            //Leave the appid field empty. It serves only the legacy purpose.
            //The access token is valid for 10 minutes. If the access token expires, you need to generate a new access token. The C sharp sample code below (AdmAuthentication class) can generate a new access token prior to exceeding to 10 minute time period.

            AdmAccessToken admToken = null;
            string headerValue;
            //Get Client Id and Client Secret from https://datamarket.azure.com/developer/applications/
            //Refer obtaining AccessToken (http://msdn.microsoft.com/en-us/library/hh454950.aspx) 
            string clientID = "BTMYTestApplication";//TODO set as resource or config property
            string clientSecret = "8fj99KnPWD0ptMp/Mg6g1ScwY7uoCpLNpA+Fsumfc64=";//TODO set as resource or config property

            AdmAuthentication admAuth = new AdmAuthentication(clientID, clientSecret);
            try
            {
                admToken = admAuth.GetAccessToken();
                // Create a header with the access_token property of the returned token
                headerValue = "Bearer " + admToken.access_token;

                //Console.WriteLine("Enter Text to detect language:");
                //string textToDetect = Console.ReadLine();
                DetectMethod(headerValue, _textToDetect);
            }
            catch (WebException e)
            {
                ProcessWebException(e);
                //Console.WriteLine("Press any key to continue...");
                //Console.ReadKey(true);
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.Message);
                // Console.WriteLine("Press any key to continue...");
                // Console.ReadKey(true);
                _exceptionMessageText += ex.Message;
            }
            return admToken;
        }
        private void DetectMethod ( string authToken, string textToDetect )
        {
            _exceptionMessageText = "";
            _detectedLanguage = "";

            //Keep appId parameter blank as we are sending access token in authorization header.
            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Detect?text=" + textToDetect;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Headers.Add("Authorization", authToken);
            WebResponse response = null;
            try
            {
                response = httpWebRequest.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                    string languageDetected = (string)dcs.ReadObject(stream);
                    //Console.WriteLine(string.Format("Language detected:{0}", languageDetected));
                    //Console.WriteLine("Press any key to continue...");
                    //Console.ReadKey(true);
                    _detectedLanguage = string.Format("Language detected:{0}", languageDetected);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }
        }
        private void ProcessWebException ( WebException e )
        {
            //Console.WriteLine("{0}", e.ToString());
            _exceptionMessageText += string.Format("{0}", e.ToString());


            // Obtain detailed error information
            string strResponse = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)e.Response)
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(responseStream, System.Text.Encoding.ASCII))
                    {
                        strResponse = sr.ReadToEnd();
                    }
                }
            }

            // Console.WriteLine("Http status code={0}, error message={1}", e.Status, strResponse);
            _exceptionMessageText += string.Format("Http status code={0}, error message={1}", e.Status, strResponse);

        }
    }


}