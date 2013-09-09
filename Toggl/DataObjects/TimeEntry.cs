using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Toggl.DataObjects;

namespace Toggl
{

    public class TimeEntry : BaseDataObject
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "wid")]
        public int? WorkspaceId { get; set; }

        [JsonProperty(PropertyName = "pid")]
        public int? ProjectId { get; set; }

        [JsonProperty(PropertyName = "tid")]
        public int? TaskId { get; set; }

        [JsonProperty(PropertyName = "billable")]
        public bool? IsBillable { get; set; }

        [JsonProperty(PropertyName = "start")]
        //[JsonConverter(typeof(IsoDateTimeConverter))]
        //public DateTime? Start { get; set; }
        public string Start { get; set; }

        [JsonProperty(PropertyName = "stop")]
        //[JsonConverter(typeof(IsoDateTimeConverter))]
        //public DateTime? Stop { get; set; }
        public string Stop { get; set; }

        [JsonProperty(PropertyName = "duration")]
        public int? Duration { get; set; }

        [JsonProperty(PropertyName = "created_with")]
        public string CreatedWith { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public List<string> TagNames { get; set; }

        [JsonProperty(PropertyName = "duronly")]
        public bool? ShowDurationOnly { get; set; }

        [JsonProperty(PropertyName = "at")]
        public DateTime? UpdatedOn { get; set; }

    }
}
