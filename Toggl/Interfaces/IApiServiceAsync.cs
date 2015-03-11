using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Toggl.Interfaces
{
    public interface IApiServiceAsync
    {

        Task<Session> GetSession();

        Task<ApiResponse> Get(string url);

        Task<ApiResponse> Get(string url, List<KeyValuePair<string, string>> args);

        Task<TResponse> Get<TResponse>(string url, List<KeyValuePair<string, string>> args);

        Task<ApiResponse> Delete(string url);

        Task<ApiResponse> Delete(string url, List<KeyValuePair<string, string>> args);

        Task<ApiResponse> Post(string url, string data);

        Task<ApiResponse> Post(string url, List<KeyValuePair<string, string>> args, string data);

        Task<ApiResponse> Put(string url, string data);

        Task<ApiResponse> Put(string url, List<KeyValuePair<string, string>> args, string data);

        System.Threading.Tasks.Task Initialize();

    }
}
