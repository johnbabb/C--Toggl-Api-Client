using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Toggl.DataObjects;

namespace Toggl
{
    public class Project : BaseDataObject
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "billable")]
        public bool? Billable { get; set; }
        [JsonProperty(PropertyName = "workspace")]
        public Workspace Workspace { get; set; }
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }
        [JsonProperty(PropertyName = "fixed_fee")]
        public string FixedFee { get; set; }
        [JsonProperty(PropertyName = "hourly_rate")]
        public string HourlyRate { get; set; }
        [JsonProperty(PropertyName = "is_fixed_fee")]
        public string IsFixedFee { get; set; }
        [JsonProperty(PropertyName = "is_private")]
        public string IsPrivate { get; set; }
        [JsonProperty(PropertyName = "client_project_name")]
        public string ClientProjectName { get; set; }
        [JsonProperty(PropertyName = "estimated_workhours")]
        public string EstimatedWorkhours { get; set; }
        [JsonProperty(PropertyName = "automatically_calculate_estimated_workhours")]
        public bool? AutomaticallyCalculateEstimatedWorkhours { get; set; }
    }
}
