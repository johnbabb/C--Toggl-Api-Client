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
	public class ClientRestSharp
    {
		public int? id { get; set; }
        /// <summary>
        /// name: The name of the client (string, required, unique in workspace)
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// wid: workspace ID, where the client will be used (integer, required)
        /// </summary>
        public int wid { get; set; }

        /// <summary>
        /// notes: Notes for the client (string, not required)
        /// </summary>
        public string notes { get; set; }

        /// <summary>
        /// hrate: The hourly rate for this client (float, not required, available only for pro workspaces)
        /// </summary>
        public double? hrate { get; set; }

        /// <summary>
        /// cur: The name of the client's currency (string, not required, available only for pro workspaces)
        /// </summary>
        public string cur { get; set; }

        /// <summary>
        /// at: timestamp that is sent in the response, indicates the time client was last updated
        /// </summary>
        public DateTime at { get; set; }

		//TODO: add some description
		public DateTime? server_deleted_at { get; set; }
		

	    public override string ToString()
	    {
		    return string.Format("Name: {0} {1}", this.name, server_deleted_at == null ? string.Empty : "[DELETED]");
	    }
    }
}
