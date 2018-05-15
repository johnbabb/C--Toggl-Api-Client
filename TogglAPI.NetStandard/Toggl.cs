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
    public class Toggl
    {
        private IApiService ApiService { get; set; }

        /// <summary>
        /// Holds methods to access client information
        /// </summary>
        public IClientService Client { get; private set; }
        /// <summary>
        /// Holds methods to access project information
        /// </summary>
        public IProjectService Project { get; private set; }
        
        /// <summary>
        /// Holds methods to access tag information
        /// </summary>
        public ITagService Tag { get; private set; }
        
        /// <summary>
        /// Holds methods to access task information
        /// </summary>
        public ITaskService Task { get; private set; }
        
        /// <summary>
        /// Holds methods to access time entry information
        /// </summary>
        public ITimeEntryService TimeEntry  { get; private set; }
        
        /// <summary>
        /// Holds methods to access user information
        /// </summary>
        public IUserService User { get; private set; }

        /// <summary>
        /// Holds methods to access workspace information
        /// </summary>
        public IWorkspaceService Workspace { get; private set; }

        public Toggl(string key)
        {
            ApiService = new ApiService(key);
            Client = new ClientService(ApiService);
            Project = new ProjectService(ApiService);
            Tag = new TagService(ApiService);
            Task = new TaskService(ApiService);
            TimeEntry = new TimeEntryService(ApiService);
            User = new UserService(ApiService);
            Workspace = new WorkspaceService(ApiService);
        }        
    }
}
