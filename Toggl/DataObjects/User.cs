using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Toggl
{
    public class User
    {
        [JsonProperty(PropertyName = "jquery_timeofday_format")]
        public string jquery_timeofday_format { get; set; }

        [JsonProperty(PropertyName = "api_token")]
        public string api_token { get; set; }

        [JsonProperty(PropertyName = "time_entry_retention_days")]
        public string time_entry_retention_days { get; set; }

        [JsonProperty(PropertyName = "jquery_date_format")]
        public string jquery_date_format { get; set; }

        [JsonProperty(PropertyName = "date_format")]
        public string date_format { get; set; }

        [JsonProperty(PropertyName = "default_workspace_id")]
        public string default_workspace_id { get; set; }

        [JsonProperty(PropertyName = "new_time_entries_start_automatically")]
        public string new_time_entries_start_automatically { get; set; }

        [JsonProperty(PropertyName = "fullname")]
        public string fullname { get; set; }

        [JsonProperty(PropertyName = "language")]
        public string language { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "beginning_of_week")]
        public string beginning_of_week { get; set; }

        [JsonProperty(PropertyName = "timeofday_format")]
        public string timeofday_format { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string email { get; set; }

        [JsonProperty(PropertyName = "clients")]
        public List<Client> clients { get; set; }

        [JsonProperty(PropertyName = "projects")]
        public List<Project> projects { get; set; }

        [JsonProperty(PropertyName = "tasks")]
        public List<Task> tasks { get; set; }

        [JsonProperty(PropertyName = "workspaces")]
        public List<Workspace> workspaces { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public List<Tag> tags { get; set; }

        [JsonProperty(PropertyName = "time_entries")]
        public List<TimeEntry> time_entries { get; set; }

    }
}
