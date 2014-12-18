using System;
using Newtonsoft.Json;
using Toggl.DataObjects;

namespace Toggl
{
    /// <summary>
    /// 
    ///https://github.com/toggl/toggl_api_docs/blob/master/chapters/projects.md#projects
    /// </summary>
    public class Project : BaseDataObject
    {
        /// <summary>
        /// id: The id of the project
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        /// <summary>
        /// name: The name of the project (string, required, unique for client and workspace)
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        
        /// <summary>
        /// wid: workspace ID, where the project will be saved (integer, required)
        /// </summary>
        [JsonProperty(PropertyName = "wid")]
        public int? WorkspaceId { get; set; }
        
        /// <summary>
        ///  cid: client ID(integer, not required)
        /// </summary>
        [JsonProperty(PropertyName = "cid")]
        public int? ClientId { get; set; }
        
        /// <summary>
        /// active: whether the project is archived or not (boolean, by default true)
        /// </summary>
        [JsonProperty(PropertyName = "active")]
        public bool? IsActive { get; set; }

        /// <summary>
        /// is_private: whether project is accessible for only project users or for all workspace users (boolean, default true)
        /// </summary>
        [JsonProperty(PropertyName = "is_private")]
        public bool? IsPrivate { get; set; }

        /// <summary>
        /// template: whether the project can be used as a template (boolean, not required)
        /// </summary>
        [JsonProperty(PropertyName = "template")]
        public bool? IsTemplateable { get; set; }

        /// <summary>
        /// template_id: id of the template project used on current project's creation
        /// </summary>
        [JsonProperty(PropertyName = "template_id")]
        public int? TemplateId { get; set; }

        /// <summary>
        /// billable: whether the project is billable or not (boolean, default true, available only for pro workspaces)
        /// </summary>
        [JsonProperty(PropertyName = "billable")]
        public bool? IsBillable { get; set; }

        /// <summary>
        /// auto_estimates: whether the esitamated hours is calculated based on task esimations or is fixed manually(boolean, default false, not required, premium functionality)
        /// </summary>
        [JsonProperty(PropertyName = "auto_estimates")]
        public bool? IsAutoEstimates { get; set; }

        /// <summary>
        /// estimated_hours: if auto_estimates is true then the sum of task estimations is returned, otherwise user inserted hours (integer, not required, premium functionality)
        /// </summary>
        [JsonProperty(PropertyName = "estimated_hours")]
        public int? EstimatedHours { get; set; }
      
        /// <summary>
        /// at: timestamp that is sent in the response for PUT, indicates the time task was last updated
        /// </summary>
        [JsonProperty(PropertyName = "at")]
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// rate: hourly rate of the project (float, not required, premium functionality)
        /// </summary>
        [JsonProperty(PropertyName = "rate")]
        public double? HourlyRate { get; set; }

		public override string ToString()
		{
			return string.Format("Id: {0}, Name: {1}", this.Id, this.Name);
		}
    }
}
