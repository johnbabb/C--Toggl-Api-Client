using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Toggl
{
    public class Task
    {
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }
        [JsonProperty(PropertyName = "workspace")]
        public Workspace workspace { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        [JsonProperty(PropertyName = "project")]
        public Project project { get; set; }
        [JsonProperty(PropertyName = "user")]
        public User user { get; set; }
        [JsonProperty(PropertyName = "estimated_workhours")]
        public string estimated_workhours{ get; set; }
        [JsonProperty(PropertyName = "estimated_seconds")]
        public string estimated_seconds { get; set; }
        [JsonProperty(PropertyName = "is_active")]
        public string is_active { get; set; }
    }
}