using System;
using System.Collections.Generic;

namespace Toggl.Interfaces
{
    /// <summary>
    /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/users.md#users
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Method to get basic information about a user.
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/users.md#get-current-user-data
        /// </summary>
        /// <returns></returns>
        User GetCurrent();

        /// <summary>
        /// Method to get basix information about a user and to get all the workspaces, clients, projects, 
        /// tasks, time entries and tags which the user can see
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/users.md#get-current-user-data
        /// </summary>
        /// <returns></returns>
        UserExtended GetCurrentExtended();

        /// <summary>
        /// Method to get basix information about a user and to get all the workspaces, clients, projects, 
        /// tasks, time entries and tags which the user can see which have changed after certain time, 
        /// add since parameter to the query. 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/users.md#get-current-user-data
        /// </summary>
        /// <returns></returns>
        UserExtended GetCurrentChanged(DateTime since);

        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/users.md#update-user-data
        /// </summary>
        /// <param name="u">UserEdit</param>
        /// <returns>User</returns>
        User Edit(User u);

        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/users.md#reset-api-token
        /// </summary>
        /// <returns></returns>
        string ResetApiToken();

        /// <summary>
        ///  Get list of users for a workspace
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/workspaces.md#get-workspace-users
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<User> GetForWorkspace(int id);

        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/users.md#get-workspace-users
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        User Add(User u);

    }
}