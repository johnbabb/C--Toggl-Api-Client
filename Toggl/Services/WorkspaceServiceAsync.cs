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
    /// <summary>
    /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/workspaces.md#workspaces
    /// </summary>
    public class WorkspaceServiceAsync : IWorkspaceServiceAsync
    {
		private IApiServiceAsync ToggleSrv { get; set; }

        public WorkspaceServiceAsync(string apiKey)
            : this(new ApiServiceAsync(apiKey))
        {

        }

        public WorkspaceServiceAsync(IApiServiceAsync srv)
        {
            ToggleSrv = srv;
        }

        public async System.Threading.Tasks.Task<List<Workspace>> List()
        {
            var url = ApiRoutes.Workspace.ListWorkspaceUrl;
            var response = await ToggleSrv.Get(url);
            var data = response.GetData<List<Workspace>>();
            return data;
        }
        
        public async System.Threading.Tasks.Task<List<User>> Users(int workspaceId)
        {
            var url = string.Format(ApiRoutes.Workspace.ListWorkspaceUsersUrl, workspaceId);
            var response = await ToggleSrv.Get(url);
            var data = response.GetData<List<User>>();
            return data;
        }

       
        public async System.Threading.Tasks.Task<List<Client>> Clients(int workspaceId)
        {
            var url = string.Format(ApiRoutes.Workspace.ListWorkspaceClientsUrl, workspaceId);
            var response = await ToggleSrv.Get(url);
            var data = response.GetData<List<Client>>();
            return data;
        }

        public async System.Threading.Tasks.Task<List<Project>> Projects(int workspaceId)
        {
            var url = string.Format(ApiRoutes.Workspace.ListWorkspaceProjectsUrl, workspaceId);
            var response = await ToggleSrv.Get(url);
            var data = response.GetData<List<Project>>();
            return data;
        }


        public async System.Threading.Tasks.Task<List<Task>> Tasks(int workspaceId)
        {
            var url = string.Format(ApiRoutes.Workspace.ListWorkspaceTasksUrl, workspaceId);
            var response = await ToggleSrv.Get(url);
            var data = response.GetData<List<Task>>();
            return data;
        }

        public async System.Threading.Tasks.Task<List<Tag>> Tags(int workspaceId)
        {
            var url = string.Format(ApiRoutes.Workspace.ListWorkspaceTagsUrl, workspaceId);
            var response = await ToggleSrv.Get(url);
            var data = response.GetData<List<Tag>>();
            return data;
        }
        


    }
}
