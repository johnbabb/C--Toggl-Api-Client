using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Toggl.DataObjects;

namespace Toggl
{
    /// <summary>
    /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/clients.md#clients
    /// </summary>
    public class Client : BaseDataObject
    {
        /// <summary>
        /// id : The client id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        /// <summary>
        /// name: The name of the client (string, required, unique in workspace)
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// wid: workspace ID, where the client will be used (integer, required)
        /// </summary>
        [JsonProperty(PropertyName = "wid")]
        public int? WorkspaceId { get; set; }

        /// <summary>
        /// notes: Notes for the client (string, not required)
        /// </summary>
        [JsonProperty(PropertyName = "notes")]
        public string Notes { get; set; }

        /// <summary>
        /// hrate: The hourly rate for this client (float, not required, available only for pro workspaces)
        /// </summary>
        [JsonProperty(PropertyName = "hrate")]
        public double? HourlyRate { get; set; }

        /// <summary>
        /// cur: The name of the client's currency (string, not required, available only for pro workspaces)
        /// </summary>
        [JsonProperty(PropertyName = "cur")]
        public string Currency { get; set; }

        /// <summary>
        /// at: timestamp that is sent in the response, indicates the time client was last updated
        /// </summary>
        [JsonProperty(PropertyName = "at")]
        public DateTime? UpdatedOn { get; set; }

		[JsonProperty(PropertyName = "server_deleted_at")]
		public DateTime? DeletedAt { get; set; }
		

	    public override string ToString()
	    {
		    return string.Format("Id: {0}, Name: {1} {2}", this.Id, this.Name, DeletedAt == null ? string.Empty : "[DELETED]");
	    }
    }
}
