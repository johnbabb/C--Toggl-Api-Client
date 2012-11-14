using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Toggl.DataObjects;

namespace Toggl
{
    public class Client : BaseDataObject
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "workspace")]
        public Workspace Workspace { get; set; }
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }
        [JsonProperty(PropertyName = "hourly_rate")]
        public double? HourlyRate { get; set; }
        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }
    }
}
