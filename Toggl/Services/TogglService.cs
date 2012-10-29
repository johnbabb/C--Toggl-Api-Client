using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Toggl.Properties;

namespace Toggl.Services
{
    public class TogglService : ITogglService
    {
        private readonly static string TogglBaseUrl = Settings.Default.TogglBaseUrl;

        private readonly static string TogglAuthUrl = TogglBaseUrl + "/sessions.json";

        private string ApiToken { get; set; }

        private CookieContainer ToggleCookies { get; set; }

        public Session Session { get; set; }

        public TogglService()
            : this(Settings.Default.ApiToken)
        {
        }

        public TogglService(string apiToken)
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

            args.Add(new KeyValuePair<string, string>("api_token", ApiToken));

            Session = PostResponse(TogglAuthUrl, args).GetData<Session>();

            ApiToken = Session.ApiToken;

            return Session;
        }

        public ApiResponse GetResponse(string url)
        {
            return GetResponse(new ApiRequest
            {
                Url = url,
                Container = ToggleCookies
            });
        }

        public ApiResponse GetResponse(string url, List<KeyValuePair<string, string>> args)
        {
            return GetResponse( new ApiRequest
                    {
                        Url = url, 
                        Args = args
                    });
        }

        public ApiResponse PostResponse(string url, string data)
        {
            return GetResponse(
                new ApiRequest
                    {
                        Url = url, 
                        Method = "POST",
                        ContentType = "application/json",
                        Data = data
                    });
        }

        public ApiResponse PostResponse(string url, List<KeyValuePair<string, string>> args, string data="")
        {
            return GetResponse(
                new ApiRequest { 
                    Url = url, 
                    Args = args, 
                    Method = "POST", 
                    ContentType = "application/json",
                    Data = data 
                });
        }

        public ApiResponse GetResponse(ApiRequest apiRequest)
        {
            string value = "";

            if (apiRequest.Args != null)
            {
                apiRequest.Args.ForEach(e => value += e.Key + "=" + e.Value + "&");
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
            
            if (apiRequest.Method == "POST")
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
            return JsonConvert.DeserializeObject<ApiResponse>(content);
        }



        public string GetAuthHeader()
        {
            var encodedApiKey = Convert.ToBase64String(Encoding.ASCII.GetBytes(ApiToken + ":api_token"));
            return "Authorization: Basic " + encodedApiKey;

        }

      
    }

    

}
