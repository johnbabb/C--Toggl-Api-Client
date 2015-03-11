using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Toggl.Interfaces
{
    /// <summary>
    /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/users.md#users
    /// </summary>
    public interface IUserServiceAsync
    {
        /// <summary>
        /// Method to get basic information about a user.
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/users.md#get-current-user-data
        /// </summary>
        /// <returns></returns>
        Task<User> GetCurrent();

        /// <summary>
        /// Method to get basix information about a user and to get all the workspaces, clients, projects, 
        /// tasks, time entries and tags which the user can see
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/users.md#get-current-user-data
        /// </summary>
        /// <returns></returns>
        Task<UserExtended> GetCurrentExtended();

        /// <summary>
        /// Method to get basix information about a user and to get all the workspaces, clients, projects, 
        /// tasks, time entries and tags which the user can see which have changed after certain time, 
        /// add since parameter to the query. 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/users.md#get-current-user-data
        /// </summary>
        /// <returns></returns>
        Task<UserExtended> GetCurrentChanged(DateTime since);

        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/users.md#update-user-data
        /// </summary>
        /// <param name="u">UserEdit</param>
        /// <returns>User</returns>
        Task<User> Edit(User u);

        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/users.md#reset-api-token
        /// </summary>
        /// <returns></returns>
        Task<string> ResetApiToken();

        /// <summary>
        ///  Get list of users for a workspace
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/workspaces.md#get-workspace-users
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<User>> GetForWorkspace(int id);

        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/users.md#get-workspace-users
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        Task<User> Add(User u);

    }
}