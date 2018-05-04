using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Toggl.Interfaces
{
    public interface IApiService
    {

        Session GetSession();

        ApiResponse Get(string url);

        ApiResponse Get(string url, List<KeyValuePair<string, string>> args);

        TResponse Get<TResponse>(string url);

        TResponse Get<TResponse>(string url, List<KeyValuePair<string, string>> args);

        ApiResponse Delete(string url);

        ApiResponse Delete(string url, List<KeyValuePair<string, string>> args);

        ApiResponse Post(string url, string data);

        ApiResponse Post(string url, List<KeyValuePair<string, string>> args, string data);

        ApiResponse Put(string url, string data);

        ApiResponse Put(string url, List<KeyValuePair<string, string>> args, string data);

        void Initialize();

    }
}
