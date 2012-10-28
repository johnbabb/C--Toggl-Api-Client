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
        private const string TogglBaseUrl = "https://www.toggl.com/api/v6";
        
        private const string TogglAuthUrl = TogglBaseUrl + "/sessions.json";
       
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
            Initialize();

        }

        private void Initialize()
        {
            if (Session != null && !string.IsNullOrEmpty(Session.api_token))
            {
                return;
            }
            var lstKv = new List<KeyValuePair<string, string>>();
            lstKv.Add(new KeyValuePair<string, string>("api_token", ApiToken));
            GetSession(lstKv);
        }

        public Session GetSession(List<KeyValuePair<string, string>> args)
        {
            ToggleCookies = new CookieContainer();
            Session = GetResponse(TogglAuthUrl, args, ToggleCookies, "POST").GetData<Session>();
            return Session;
        }
       
        public ApiResponse GetResponse(string url)
        {
            return GetResponse(url, new List<KeyValuePair<string, string>>(), ToggleCookies);
        }
        
        public ApiResponse GetResponse(string url, List<KeyValuePair<string, string>> args)
        {
            return GetResponse(url, args, ToggleCookies);
        }

        public ApiResponse GetResponse(string url, List<KeyValuePair<string, string>> args, CookieContainer container, string method = "GET")
        {
            string value = "";

            args.ForEach(e => value += e.Key + "=" + e.Value + "&");
            value = value.Trim('&');
            if (method == "GET" && !string.IsNullOrEmpty(value))
            {
                url += "?" + value;
            }


            var authRequest = (HttpWebRequest)HttpWebRequest.Create(url);

            authRequest.Credentials = CredentialCache.DefaultCredentials;
            authRequest.Method = method;
            authRequest.ContentType = "application/x-www-form-urlencoded";
            authRequest.CookieContainer = container;

            if (method == "POST")
            {
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
    }
}
