using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Toggl.Interfaces;
using Toggl.Properties;

namespace Toggl.Services
{
    public class ApiService : IApiService
    {
        private string ApiToken { get; set; }

        private CookieContainer ToggleCookies { get; set; }

        public Session Session { get; set; }

        public ApiService()
            : this(Settings.Default.ApiToken)
        {
        }

        public ApiService(string apiToken)
        {
            ApiToken = apiToken;
            //Initialize();
        }

        public void Initialize()
        {
            if (Session != null && !string.IsNullOrEmpty(Session.ApiToken))
            {
                return;
            }

            GetSession();
        }

        public Session GetSession()
        {

            var args = new List<KeyValuePair<string, string>>();

            Session = Get(ApiRoutes.Session.Me, args).GetData<Session>();

            ApiToken = Session.ApiToken;

            return Session;
        }

        public ApiResponse Get(string url)
        {
            return Get(new ApiRequest
            {
                Url = url
            });
        }

        public ApiResponse Get(string url, List<KeyValuePair<string, string>> args)
        {
            return Get(new ApiRequest
            {
                Url = url,
                Args = args
            });
        }
        public ApiResponse Delete(string url)
        {
            return Get(new ApiRequest
            {
                Url = url,
                Method = "DELETE"
            });
        }

        public ApiResponse Delete(string url, List<KeyValuePair<string, string>> args)
        {
            return Get(new ApiRequest
            {
                Url = url,
                Method = "DELETE",
                Args = args
            });
        }
        public ApiResponse Post(string url, string data)
        {
            return Get(
                new ApiRequest
                {
                    Url = url,
                    Method = "POST",
                    ContentType = "application/json",
                    Data = data
                });
        }

        public ApiResponse Post(string url, List<KeyValuePair<string, string>> args, string data = "")
        {
            return Get(
                new ApiRequest
                {
                    Url = url,
                    Args = args,
                    Method = "POST",
                    ContentType = "application/json",
                    Data = data
                });
        }

        public ApiResponse Put(string url, string data)
        {
            return Get(
                new ApiRequest
                {
                    Url = url,
                    Method = "PUT",
                    ContentType = "application/json",
                    Data = data
                });
        }

        public ApiResponse Put(string url, List<KeyValuePair<string, string>> args, string data = "")
        {
            return Get(
                new ApiRequest
                {
                    Url = url,
                    Args = args,
                    Method = "PUT",
                    ContentType = "application/json",
                    Data = data
                });
        }

        private ApiResponse Get(ApiRequest apiRequest)
        {
            string value = "";

            if (apiRequest.Args != null && apiRequest.Args.Count > 0)
            {
                apiRequest.Args.ForEach(e => value += e.Key + "=" + System.Uri.EscapeDataString(e.Value) + "&");
                value = value.Trim('&');
            }

            if (apiRequest.Method == "GET" && !string.IsNullOrEmpty(value))
            {
                apiRequest.Url += "?" + value;
            }

            var authRequest = (HttpWebRequest)HttpWebRequest.Create(apiRequest.Url);

            authRequest.Method = apiRequest.Method;

            authRequest.ContentType = apiRequest.ContentType;

            authRequest.Credentials = CredentialCache.DefaultNetworkCredentials;

            authRequest.Headers.Add(GetAuthHeader());

            if (apiRequest.Method == "POST" || apiRequest.Method == "PUT")
            {
                value += apiRequest.Data;
                authRequest.ContentLength = value.Length;
                using (StreamWriter writer = new StreamWriter(authRequest.GetRequestStream(), Encoding.ASCII))
                {
                    writer.Write(value);
                }
            }

            var authResponse = (HttpWebResponse)authRequest.GetResponse();
            string content = "";
            using (var reader = new StreamReader(authResponse.GetResponseStream(), Encoding.UTF8))
            {
                content = reader.ReadToEnd();
            }

            if ((string.IsNullOrEmpty(content)
                || content.ToLower() =="null")
                && authResponse.StatusCode == HttpStatusCode.OK
                && authResponse.Method == "DELETE")
            {
                var rsp = new ApiResponse();
                rsp.Data = new JObject();
                rsp.related_data_updated_at = DateTime.Now;
                rsp.StatusCode = authResponse.StatusCode;
                rsp.Method = authResponse.Method;

                return rsp;
            }

            try
            {

                var rsp =  JsonConvert.DeserializeObject<ApiResponse>(content);
                rsp.StatusCode = authResponse.StatusCode;
                rsp.Method = authResponse.Method;

                return rsp;
            }
            catch (Exception)
            {
                var jry = JArray.Parse(content);
                var rsp = new ApiResponse()
                    {
                        Data = jry,
                        related_data_updated_at = DateTime.Now,
                        StatusCode = authResponse.StatusCode,
                        Method = authResponse.Method
                    };

                return rsp;
            }

        }



        private string GetAuthHeader()
        {
            var encodedApiKey = Convert.ToBase64String(Encoding.ASCII.GetBytes(ApiToken + ":api_token"));
            return "Authorization: Basic " + encodedApiKey;

        }


    }

}
