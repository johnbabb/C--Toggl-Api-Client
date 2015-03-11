using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Toggl.Interfaces;

namespace Toggl.Services
{
    public class ProjectServiceAsync : IProjectServiceAsync
    {
        private readonly string ProjectsUrl = ApiRoutes.Project.ProjectsUrl;
        

        private IApiServiceAsync ToggleSrv { get; set; }


        public ProjectServiceAsync(string apiKey)
            : this(new ApiServiceAsync(apiKey))
        {

        }

		public ProjectServiceAsync(IApiServiceAsync srv)
        {
            ToggleSrv = srv;
        }

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/projects.md
        /// </summary>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<List<Project>> List()
        {
            var lstProj = new List<Project>();
            var response = await ToggleSrv.Get(ApiRoutes.Workspace.ListWorkspaceUrl);
            var lstWrkSpc = response.GetData<List<Workspace>>();
            lstWrkSpc.ForEach(async e =>
                {
                    var projs = await ForWorkspace(e.Id.Value);
                    lstProj.AddRange(projs);
                });
            return lstProj;
        }
        
        public async System.Threading.Tasks.Task<List<Project>> ForWorkspace (int id)
        {
            var url = string.Format(ApiRoutes.Workspace.ListWorkspaceProjectsUrl, id);
            var response = await ToggleSrv.Get(url);
            var data = response.GetData<List<Project>>();
            return data;
        }

	    public async System.Threading.Tasks.Task<List<Project>> ForClient(Client client)
	    {
		    if (!client.Id.HasValue)
				throw new InvalidOperationException("Client Id not set");
			return await ForClient(client.Id.Value);
	    }

        public async System.Threading.Tasks.Task<List<Project>> ForClient(int id)
        {
            var url = string.Format(ApiRoutes.Client.ClientProjectsUrl, id);
            var response = await ToggleSrv.Get(url);
            var data = response.GetData<List<Project>>();
            return data;
        }

        public async System.Threading.Tasks.Task<Project> Get(int id)
        {
            var result = await List();
            return result.FirstOrDefault(w => w.Id == id);
        }

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/projects.md#create-project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<Project> Add(Project project)
        {
            var response = await ToggleSrv.Post(ProjectsUrl, project.ToJson());
            var data = response.GetData<Project>();
            return data;
        }

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/projects.md#delete-a-project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
	    public async System.Threading.Tasks.Task<bool> Delete(int id)
	    {
		    var url = string.Format(ApiRoutes.Project.DetailUrl, id);
		    var rsp = await ToggleSrv.Delete(url);
		    return rsp.StatusCode == HttpStatusCode.OK;
	    }

	    public async System.Threading.Tasks.Task<bool> DeleteIfAny(int[] ids)
	    {
		    if (!ids.Any() || ids == null)
				return true;
		    return await Delete(ids);
	    }

        public async System.Threading.Tasks.Task<bool> Delete(int[] ids)
		{
			if (!ids.Any() || ids == null)
				throw new ArgumentNullException("ids");

			var url = string.Format(
				ApiRoutes.Project.ProjectsBulkDeleteUrl,
				string.Join(",", ids.Select(id => id.ToString()).ToArray()));

			var rsp = await ToggleSrv.Delete(url);
			return rsp.StatusCode == HttpStatusCode.OK;
		}    
       
    }
}
