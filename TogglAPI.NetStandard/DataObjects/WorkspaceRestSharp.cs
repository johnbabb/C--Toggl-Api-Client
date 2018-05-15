using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Toggl.DataObjects;

namespace Toggl
{
    /// <summary>
	/// https://github.com/toggl/toggl_api_docs/blob/master/chapters/workspaces.md
    /// </summary>
	public class WorkspaceRestSharp
    {
        /// <summary>
        /// id: The Workspace id
        /// </summary>
        public int? id { get; set; }

        /// <summary>
		/// name: the name of the workspace (string)
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// premium: If it's a pro workspace or not. Shows if someone is paying for the workspace or not (boolean, not required)
        /// </summary>
        public bool? premium { get; set; }

		/// <summary>
		/// admin: shows whether currently requesting user has admin access to the workspace (boolean)
		/// </summary>
		public bool admin { get; set; }

		/// <summary>
		/// default_hourly_rate: default hourly rate for workspace, won't be shown to non-admins 
		/// if the only_admins_see_billable_rates flag is set to true (float)
		/// </summary>
		public double? default_hourly_rate { get; set; }

		/// <summary>
		/// default_currency: default currency for workspace (string)
		/// </summary>
		public string default_currency { get; set; }

		/// <summary>
		/// only_admins_may_create_projects: whether only the admins can create projects or everybody (boolean)
		/// </summary>
		public bool only_admins_may_create_projects { get; set; }

		/// <summary>
		/// only_admins_see_billable_rates: whether only the admins can see billable rates or everybody (boolean)
		/// </summary>
		public bool only_admins_see_billable_rates { get; set; }

		/// <summary>
		/// rounding: type of rounding (integer)
		/// </summary>
		public int rounding { get; set; }

		/// <summary>
		/// rounding_minutes: round up to nearest minute (integer)
		/// </summary>
		public int rounding_minutes { get; set; }

		/// <summary>
		/// at: timestamp that indicates the time workspace was last updated
		/// </summary>
		public DateTime at { get; set; }

		/// <summary>
		/// logo_url: URL pointing to the logo (if set, otherwise omited) (string)
		/// </summary>
		public string logo_url { get; set; }

		public override string ToString()
		{
			return string.Format("Name: {0}", this.name);
		}
    }
}
