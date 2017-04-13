using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Toggl.DataObjects;
using Toggl.Interfaces;

namespace Toggl.Services
{
    public class ApiService : IApiService
    {
        private string ApiToken { get; set; }

        public Session Session { get; set; }

        public ApiService(string apiToken)
        {
            ApiToken = apiToken;
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

        public TResponse Get<TResponse>(string url)
        {
            return Get<TResponse>(new ApiRequest()
            {
                Url = url
            });
        }

        public TResponse Get<TResponse>(string url, List<KeyValuePair<string, string>> args)
        {
            return Get<TResponse>(new ApiRequest()
                                  {
                                      Url = url, Args = args
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

        private TResponse Get<TResponse>(ApiRequest apiRequest) 
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

            var authResponse = (HttpWebResponse)authRequest.GetResponse();
            string content = "";
            using (var reader = new StreamReader(authResponse.GetResponseStream(), Encoding.UTF8))
            {
                content = reader.ReadToEnd();
            }

            var rsp = JsonConvert.DeserializeObject<TResponse>(content);              
            return rsp;
        }

        private HttpWebRequest ManufactureRequest(ApiRequest apiRequest)
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
                var utd8WithoutBom = new UTF8Encoding(false);

                value += apiRequest.Data;
                authRequest.ContentLength = utd8WithoutBom.GetByteCount(value);
                using (var writer = new StreamWriter(authRequest.GetRequestStream(), utd8WithoutBom))
                {
                    writer.Write(value);
                }
            }
            return authRequest;
        }

        private ApiResponse Get(ApiRequest apiRequest)
        {
            HttpWebResponse authResponse = null;

            while (true)
            {
                try
                {
                    HttpWebRequest authRequest = ManufactureRequest(apiRequest);

                    authResponse = (HttpWebResponse)authRequest.GetResponse();
                    Console.WriteLine(((int)authResponse.StatusCode).ToString());
                    break;
                }
                catch (System.Net.WebException ex)
                {
                    // Pay attention to HTTP 429 responses to work with the Leaky bucket
                    // mentioned at https://github.com/toggl/toggl_api_docs  
                    // retry as necessary

                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        var response = ex.Response as HttpWebResponse;
                        if (response != null)
                        {
                            int statusCode = (int)response.StatusCode;
                            Console.WriteLine(statusCode.ToString());
                            if (statusCode == 429)
                            {
                                Thread.Sleep(1500); // 1500ms based on cursory testing
                            }
                            else
                            {
                                throw;
                            }
                        }
                        else {
                            throw;
                        }
                    }
                    else {
                        throw;
                    }
                }
            }
            string content = "";
            using (var reader = new StreamReader(authResponse.GetResponseStream(), Encoding.UTF8))
            {
                content = reader.ReadToEnd();
            }

            if ((string.IsNullOrEmpty(content)
                || content.ToLower() == "null")
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
	            ApiResponse rsp = content.ToLower() == "null" 
					? new ApiResponse { Data = null } 
					: JsonConvert.DeserializeObject<ApiResponse>(content);
	            
                rsp.StatusCode = authResponse.StatusCode;
                rsp.Method = authResponse.Method;
                return rsp;
            }
            catch (Exception)
            {
                var token = JToken.Parse(content);
                var rsp = new ApiResponse()
                    {
                        Data = token,
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
