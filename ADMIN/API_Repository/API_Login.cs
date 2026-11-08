using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ADMIN.API_Repository
{
    public class API_Login
    {
        public dynamic GetForgotPassword(string objAdmin)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
               // var res = (JObject)JsonConvert.DeserializeObject(client.DownloadString(Static_Data.BaseURL + "AdminLogin/" + EmailID));
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "AdminLogin", "PUT", objAdmin));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddLogin(string objAdmin)
        {

            try
            {
                WebClient client = new WebClient();
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var res = (JObject)JsonConvert.DeserializeObject(client.UploadString(Static_Data.BaseURL + "AdminLogin", "POST", objAdmin));
                
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}