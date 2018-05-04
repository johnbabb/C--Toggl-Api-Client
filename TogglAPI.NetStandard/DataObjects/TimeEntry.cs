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
        public long? Id { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "wid")]
		public long? WorkspaceId { get; set; }

        [JsonProperty(PropertyName = "pid")]
		public long? ProjectId { get; set; }

		[JsonProperty(PropertyName = "tid")]
		public long? TaskId { get; set; }

		[JsonProperty(PropertyName = "task")]
		public string TaskName { get; set; }

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
        public long? Duration { get; set; }

        [JsonProperty(PropertyName = "created_with")]
        public string CreatedWith { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public List<string> TagNames { get; set; }

        [JsonProperty(PropertyName = "duronly")]
        public bool? ShowDurationOnly { get; set; }

        [JsonProperty(PropertyName = "at")]
        public DateTime? UpdatedOn { get; set; }

		public override string ToString()
		{
			return string.Format("Id: {0}, Start: {1}, Stop: {2}, TaskId: {3}", this.Id, this.Start, this.Stop, this.TaskId);
		}
    }
}
