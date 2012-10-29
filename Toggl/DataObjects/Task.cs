using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Toggl.DataObjects;

namespace Toggl
{
    public class Task : BaseDataObject
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "workspace")]
        public Workspace Workspace { get; set; }
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }
        [JsonProperty(PropertyName = "project")]
        public Project Project { get; set; }
        [JsonProperty(PropertyName = "user")]
        public User User { get; set; }
        [JsonProperty(PropertyName = "estimated_workhours")]
        public int? EstimatedWorkhours{ get; set; }
        [JsonProperty(PropertyName = "estimated_seconds")]
        public int? EstimatedSeconds { get; set; }
        [JsonProperty(PropertyName = "is_active")]
        public bool? IsActive { get; set; }
    }
}