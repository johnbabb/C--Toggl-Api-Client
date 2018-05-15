using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Toggl.DataObjects;

namespace Toggl
{
    /// <summary>
    /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/tasks.md#tasks
    /// </summary>
    public class Task : BaseDataObject
    {

        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "pid")]
        public int? ProjectId { get; set; }

        [JsonProperty(PropertyName = "wid")]
        public int? WorkspaceId { get; set; }
        
        [JsonProperty(PropertyName = "uid")]
        public int? UserId { get; set; }

        [JsonProperty(PropertyName = "estimated_seconds")]
        public int? EstimatedSeconds { get; set; }

        [JsonProperty(PropertyName = "is_active")]
        public bool? IsActive { get; set; }

        [JsonProperty(PropertyName = "at")]
        public DateTime? UpdatedOn { get; set; }
		
		public override string ToString()
		{
			return string.Format("Id: {0}, Name: {1}", this.Id, this.Name);
		}
    }
}