using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace csharpRestClient
{
    public enum httpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public enum authenticationType
    {
        Basic,
        NTLM
    }

    public enum authenticationTechnique
    {
        RollYourOwn,
        NetworkCredential
    }

    class RestClient
    {
        public string endPoint { get; set; }
        public httpVerb httpMethod { get; set; }
        public authenticationType authType { get; set; }
        public authenticationTechnique authTech { get; set; }
        public string userName { get; set; }
        public string Password{ get; set; }



        public RestClient()
        {
            endPoint = string.Empty;
            httpMethod = httpVerb.GET;
        }

        public string makeRequest()
        {

            string strResponseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);

            String authHeader = System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(userName + ":" + Password));
            request.Headers.Add("Authorization", authType.ToString() + " " + authHeader);

            try
            {
                request.Method = httpMethod.ToString();
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ApplicationException("error code " + response.StatusCode.ToString());
                    }
                    //Process the response stream ... (could be JSON ,XML or HTML etc...)

                    using (Stream responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (StreamReader reader = new StreamReader(responseStream))
                            {
                                strResponseValue = reader.ReadToEnd();
                            }//End of StreamReader
                        }
                    }//End of using ResponseStream

                }//End of using response
            }

            catch(Exception e)
            {
                System.Diagnostics.Debug.Write(e.Message, ToString() + Environment.NewLine);
                return e.Message.ToString();
            }
                   
                return strResponseValue;
        }
    }
}
