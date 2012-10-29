using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Toggl.DataObjects;

namespace Toggl
{
    public class Session : BaseDataObject
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "date_format")]
        public string DateFormat { get; set; }

        [JsonProperty(PropertyName = "new_time_entries_start_automatically")]
        public string NewTimeEntriesStartAutomatically { get; set; }

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }
        [JsonProperty(PropertyName = "api_token")]
        public string ApiToken { get; set; }

        [JsonProperty(PropertyName = "fullname")]
        public string FullName { get; set; }

        [JsonProperty(PropertyName = "time_entry_retention_days")]        
        public string TimeEntryRetentionDays { get; set; }

        [JsonProperty(PropertyName = "jquery_date_format")]
        public string JqueryDateFormat { get; set; }

        [JsonProperty(PropertyName = "default_workspace_id")]
        public int? DefaultWorkspaceId { get; set; }

        [JsonProperty(PropertyName = "jquery_timeofday_format")]
        public string JQueryTimeofdayFormat { get; set; }

        [JsonProperty(PropertyName = "beginning_of_week")]
        public string BeginningOfWeek { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "timeofday_format")]
        public string TimeOfDayFormat { get; set; }
    }
}
