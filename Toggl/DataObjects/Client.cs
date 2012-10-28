using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Toggl
{
    public class Client
    {
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }
        [JsonProperty(PropertyName = "workspace")]
        public Workspace workspace { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        [JsonProperty(PropertyName = "hourly_rate")]
        public string hourly_rate { get; set; }
        [JsonProperty(PropertyName = "currency")]
        public string currency { get; set; }
    }
}
