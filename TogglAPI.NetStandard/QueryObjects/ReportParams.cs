using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Toggl.DataObjects;

namespace Toggl.QueryObjects
{
    public class ReportParams : BaseDataObject
    {
        [JsonProperty(PropertyName = "user_agent")]
        public string UserAgent { get; set; }

        [JsonProperty(PropertyName = "workspace_id")]
        public int WorkspaceId { get; set; }

        [JsonProperty(PropertyName = "since")]
        public string Since { get; set; }

        [JsonProperty(PropertyName = "until")]
        public string Until { get; set; }

        [JsonProperty(PropertyName = "billable")]
        public string Billable { get; set; }

        [JsonProperty(PropertyName = "client_ids")]
        public List<int> ClientIds { get; set; }

        [JsonProperty(PropertyName = "project_ids")]
        public List<int> ProjectIds { get; set; }

        [JsonProperty(PropertyName = "user_ids")]
        public List<int> UserIds { get; set; }

        [JsonProperty(PropertyName = "tag_ids")]
        public List<int> TagIds { get; set; }

        [JsonProperty(PropertyName = "task_ids")]
        public string TaskIds { get; set; }

        [JsonProperty(PropertyName = "time_entry_ids")]
        public List<int> TimeEntryIds { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "without_description")]
        public bool? WithoutDescription { get; set; }
       
    }
}
