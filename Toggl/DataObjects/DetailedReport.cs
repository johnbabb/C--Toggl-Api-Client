using System.Collections.Generic;
using Newtonsoft.Json;

namespace Toggl.DataObjects
{
    public class DetailedReport : Report
    {
		[JsonProperty(PropertyName = "data")]
		public List<ReportTimeEntry> Data { get; set; }
		
        [JsonProperty(PropertyName = "total_count")]
        public long? TotalCount { get; set; }

        [JsonProperty(PropertyName = "per_page")]
        public long? PerPage { get; set; }
    }
}
