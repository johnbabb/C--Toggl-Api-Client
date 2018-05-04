using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toggl.Interfaces;
using Toggl.Services;

namespace Toggl
{
    /// <summary>
    /// 
    /// </summary>
    public class TogglAsync
    {
        private IApiServiceAsync ApiService { get; set; }

        /// <summary>
        /// Holds methods to access client information
        /// </summary>
        public IClientServiceAsync Client { get; private set; }

        /// <summary>
        /// Holds methods to access project information
        /// </summary>
        public IProjectServiceAsync Project { get; private set; }
        
        /// <summary>
        /// Holds methods to access tag information
        /// </summary>
        public ITagServiceAsync Tag { get; private set; }
        
        /// <summary>
        /// Holds methods to access task information
        /// </summary>
        public ITaskServiceAsync Task { get; private set; }
        
        /// <summary>
        /// Holds methods to access time entry information
        /// </summary>
        public ITimeEntryServiceAsync TimeEntry  { get; private set; }
        
        /// <summary>
        /// Holds methods to access user information
        /// </summary>
        public IUserServiceAsync User { get; private set; }

        /// <summary>
        /// Holds methods to access workspace information
        /// </summary>
        public IWorkspaceServiceAsync Workspace { get; private set; }

        public TogglAsync(string key)
        {
            ApiService = new ApiServiceAsync(key);
            Client = new ClientServiceAsync(ApiService);
            Project = new ProjectServiceAsync(ApiService);
            Tag = new TagServiceAsync(ApiService);
            Task = new TaskServiceAsync(ApiService);
            TimeEntry = new TimeEntryServiceAsync(ApiService);
            User = new UserServiceAsync(ApiService);
            Workspace = new WorkspaceServiceAsync(ApiService);
        }        
    }
}
