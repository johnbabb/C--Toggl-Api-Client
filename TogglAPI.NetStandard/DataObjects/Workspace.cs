using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Toggl.DataObjects;

namespace Toggl
{
    /// <summary>
    /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/workspaces.md#workspaces
    /// </summary>
    public class Workspace : BaseDataObject
    {
        /// <summary>
        /// id: The Workspace id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        /// <summary>
        /// name: (string, required)
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// premium: If it's a pro workspace or not. Shows if someone is paying for the workspace or not (boolean, not required)
        /// </summary>
        [JsonProperty(PropertyName = "premium")]
        public bool? Ispremium { get; set; }

        /// <summary>
        /// at: timestamp that is sent in the response, indicates the time item was last updated
        /// </summary>
        [JsonProperty(PropertyName = "at")]
        public DateTime? UpdatedOn { get; set; }

		public override string ToString()
		{
			return string.Format("Id: {0}, Name: {1}", this.Id, this.Name);
		}
    }
}
