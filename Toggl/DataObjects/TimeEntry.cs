using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Toggl
{
    public class TimeEntry
    {
        [JsonProperty(PropertyName = "duration")]
        public string duration { get; set; }
        [JsonProperty(PropertyName = "billable")]
        public bool billable { get; set; }
        [JsonProperty(PropertyName = "workspace")]
        public Workspace workspace { get; set; }
        [JsonProperty(PropertyName = "stop")]
        public string stop { get; set; }
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }
        [JsonProperty(PropertyName = "project")]
        public Project project { get; set; }
        [JsonProperty(PropertyName = "start")]
        public string start { get; set; }
        [JsonProperty(PropertyName = "tag_names")]
        public string[] tag_names { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string description { get; set; }
        [JsonProperty(PropertyName = "ignore_start_and_stop")]
        public bool ignore_start_and_stop { get; set; }
    }
}
