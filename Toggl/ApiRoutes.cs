using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toggl.Properties;

namespace Toggl
{
    public class ApiRoutes
    {
        readonly static string TogglBaseUrl = Settings.Default.TogglBaseUrl;
        
        public class Client
        {
            public readonly static string ListClientsUrl = TogglBaseUrl + "/clients.json";
        }

        public class Workspace
        {
            public readonly static string ListWorkspaceUrl = TogglBaseUrl + "/workspaces.json";

            public readonly static string AddWorkspaceUserUrl = TogglBaseUrl + "/workspaces/{0}/project_users.json";

            public readonly static string ListWorkspaceUsersUrl = TogglBaseUrl + "/workspaces/{0}/users.json";
            
        }

        public class Task
        {
            public readonly static string TogglTasksUrl = TogglBaseUrl + "/tasks.json";
        }

        public class TimeEntry
        {
            public readonly static string TimeEntriesUrl = TogglBaseUrl + "/time_entries.json";

            public readonly static string TimeEntryUrl = TogglBaseUrl + "/time_entries/{0}.json";
        }
        public class Project
        {
            public readonly static string ProjectsUrl=TogglBaseUrl + "/projects.json";
        }

    }
}
