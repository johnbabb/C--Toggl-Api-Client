using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Toggl.DataObjects;

namespace Toggl
{
    
    public class TimeEntry : BaseDataObject
    {
        [JsonProperty(PropertyName = "duration")]
        public int? Duration { get; set; }

        [JsonProperty(PropertyName = "billable")]
        public bool? Billable { get; set; }

        [JsonProperty(PropertyName = "workspace")]
        public Workspace Workspace { get; set; }

        [JsonProperty(PropertyName = "stop")]
        public string Stop { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "project")]
        public Project Project { get; set; }

        [JsonProperty(PropertyName = "start")]
        public string Start { get; set; }

        [JsonProperty(PropertyName = "tag_names")]
        public List<string> TagNames { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "ignore_start_and_stop")]
        public bool? IgnoreStartAndStop { get; set; }

        [JsonProperty(PropertyName = "created_with")]
        public string CreatedWith { get; set; }
        

    }
}
