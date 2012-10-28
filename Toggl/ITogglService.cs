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

        Session GetSession(List<KeyValuePair<string, string>> args);

        ApiResponse GetResponse(string url);

        ApiResponse GetResponse(string url, List<KeyValuePair<string, string>> args);

        ApiResponse GetResponse(string url, List<KeyValuePair<string, string>> args, CookieContainer container,
                                string method = "GET");
    }
}
