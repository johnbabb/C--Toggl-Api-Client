using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Toggl.DataObjects;

namespace Toggl
{
    public class Client : BaseDataObject
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "wid")]
        public int? WorkspaceId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "notes")]
        public string Notes { get; set; }

        [JsonProperty(PropertyName = "hrate")]
        public double? HourlyRate { get; set; }

        [JsonProperty(PropertyName = "cur")]
        public string Currency { get; set; }
        
    }
}
