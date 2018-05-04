using System.Collections.Generic;
using System.Threading.Tasks;

namespace Toggl.Interfaces
{
    /// <summary>
    /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/workspaces.md#workspaces
    /// </summary>
    public interface IWorkspaceServiceAsync
    {
       
        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/workspaces.md#get-workspaces
        /// </summary>
        /// <returns></returns>
        Task<List<Workspace>> List();
        
        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/workspaces.md#get-workspace-users
        /// </summary>
        /// <param name="workspaceId"></param>
        /// <returns></returns>
        Task<List<User>> Users(int workspaceId);
        
        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/workspaces.md#get-workspace-clients
        /// </summary>
        /// <param name="workspaceId"></param>
        /// <returns></returns>
        Task<List<Client>> Clients(int workspaceId);

        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/workspaces.md#get-workspace-projects
        /// </summary>
        /// <param name="workspaceId"></param>
        /// <returns></returns>
        Task<List<Project>> Projects(int workspaceId);
        
        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/workspaces.md#get-workspace-tasks
        /// </summary>
        /// <param name="workspaceId"></param>
        /// <returns></returns>
        Task<List<Task>> Tasks(int workspaceId);

        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/workspaces.md#get-workspace-tags
        /// </summary>
        /// <param name="workspaceId"></param>
        /// <returns></returns>
        Task<List<Tag>> Tags(int workspaceId);
    }
}