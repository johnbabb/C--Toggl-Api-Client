using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Toggl
{
    public class Project
    {
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }
        [JsonProperty(PropertyName = "billable")]
        public bool billable { get; set; }
        [JsonProperty(PropertyName = "workspace")]
        public Workspace workspace { get; set; }
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }
        [JsonProperty(PropertyName = "fixed_fee")]
        public string fixed_fee { get; set; }
        [JsonProperty(PropertyName = "hourly_rate")]
        public string hourly_rate { get; set; }
        [JsonProperty(PropertyName = "is_fixed_fee")]
        public string is_fixed_fee { get; set; }
        [JsonProperty(PropertyName = "is_private")]
        public string is_private { get; set; }
        [JsonProperty(PropertyName = "client_project_name")]
        public string client_project_name { get; set; }
        [JsonProperty(PropertyName = "estimated_workhours")]
        public string estimated_workhours { get; set; }
        [JsonProperty(PropertyName = "automatically_calculate_estimated_workhours")]
        public string automatically_calculate_estimated_workhours { get; set; }
    }
}
