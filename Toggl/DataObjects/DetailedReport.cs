using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Toggl.DataObjects
{
    public class DetailedReport : Report
    {
        [JsonProperty(PropertyName = "data")]
        public List<TimeEntry> Data { get; set; }
        
        [JsonProperty(PropertyName = "total_count")]
        public int? TotalCount { get; set; }

        [JsonProperty(PropertyName = "per_page")]
        public int? PerPage { get; set; }
    }
}
