using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Toggl.ApiResponses;
using Toggl.Properties;

namespace Toggl.Services
{
    public class TogglService : ITogglService
    {
        private readonly static string TogglBaseUrl = Settings.Default.TogglBaseUrl;

        private static readonly string TogglAuthUrl = ApiRoutes.Session.Me;

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
            var rsp = Get(TogglAuthUrl, args);
            Session = rsp.GetData<Session>();

            ApiToken = Session.ApiToken;

            return Session;
        }

        public ObjectResponse Get(string url)
        {

            var rsp = Get(new ApiRequest
                {
                    Url = url
                });
            return JsonConvert.DeserializeObject<ObjectResponse>(rsp);
        }

        public ObjectResponse Get(string url, List<KeyValuePair<string, string>> args)
        {
            var rsp = Get(new ApiRequest
                    {
                        Url = url, 
                        Args = args
                    });
            return JsonConvert.DeserializeObject<ObjectResponse>(rsp);
        }
        public CollectionResponse List(string url)
        {
            var rsp = Get(new ApiRequest
            {
                Url = url
            });
            return JsonConvert.DeserializeObject<CollectionResponse>(rsp);
        }

        public CollectionResponse List(string url, List<KeyValuePair<string, string>> args)
        {
            var rsp = Get(new ApiRequest
            {
                Url = url,
                Args = args
            });
            return JsonConvert.DeserializeObject<CollectionResponse>(rsp);
        }
        public ObjectResponse Delete(string url)
        {
            var rsp = Get(new ApiRequest
            {
                Url = url,
                Method = "DELETE"
            });
            return JsonConvert.DeserializeObject<ObjectResponse>(rsp);
        }

        public ObjectResponse Delete(string url, List<KeyValuePair<string, string>> args)
        {
            var rsp = Get(new ApiRequest
            {
                Url = url,
                Method = "DELETE",
                Args = args
            });
            return JsonConvert.DeserializeObject<ObjectResponse>(rsp);
        }
        public ObjectResponse Post(string url, string data)
        {
            var rsp = Get(
                new ApiRequest
                    {
                        Url = url, 
                        Method = "POST",
                        ContentType = "application/json",
                        Data = data
                    });
            return JsonConvert.DeserializeObject<ObjectResponse>(rsp);
        }

        public ObjectResponse Post(string url, List<KeyValuePair<string, string>> args, string data = "")
        {
            var rsp = Get(
                new ApiRequest { 
                    Url = url, 
                    Args = args, 
                    Method = "POST", 
                    ContentType = "application/json",
                    Data = data 
                });
            return JsonConvert.DeserializeObject<ObjectResponse>(rsp);
        }

        public ObjectResponse Put(string url, string data)
        {
            var rsp = Get(
                new ApiRequest
                {
                    Url = url,
                    Method = "PUT",
                    ContentType = "application/json",
                    Data = data
                });
            return JsonConvert.DeserializeObject<ObjectResponse>(rsp);
        }

        public ObjectResponse Put(string url, List<KeyValuePair<string, string>> args, string data = "")
        {
            var rsp = Get(
                new ApiRequest
                {
                    Url = url,
                    Args = args,
                    Method = "PUT",
                    ContentType = "application/json",
                    Data = data
                });
            return JsonConvert.DeserializeObject<ObjectResponse>(rsp);
        }

        private string Get(ApiRequest apiRequest)
        {
            string value = "";

            if (apiRequest.Args != null && apiRequest.Args.Count > 0)
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
            
            if (apiRequest.Method == "POST" || apiRequest.Method == "PUT" )
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

            return content;

        }



        public string GetAuthHeader()
        {
            var encodedApiKey = Convert.ToBase64String(Encoding.ASCII.GetBytes(ApiToken + ":api_token"));
            return "Authorization: Basic " + encodedApiKey;

        }

      
    }

    

}
