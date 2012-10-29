using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Toggl.Properties;

namespace Toggl
{
    public interface ITogglService
    {

        Session GetSession();

        ApiResponse GetResponse(string url);

        ApiResponse GetResponse(string url, List<KeyValuePair<string, string>> args);

        ApiResponse PostResponse(string url, string data);

        ApiResponse PostResponse(string url, List<KeyValuePair<string, string>> args, string data);

        ApiResponse GetResponse(ApiRequest apiRequest);

        void Initialize();

    }
}
