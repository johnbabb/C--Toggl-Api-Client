using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Toggl.Properties;

namespace Toggl.Services
{
    public class WorkspaceService
    {
        private const string TogglBaseUrl = "https://www.toggl.com/api/v6";

        private const string TogglWorkspaceUrl = TogglBaseUrl + "/workspaces.json";

        private ITogglService ToggleSrv { get; set; }

        public WorkspaceService()
            : this(new TogglService())
        {
        }

        public WorkspaceService(ITogglService srv)
        {
            ToggleSrv = srv;
        }

        public List<Workspace> GetWorkspaces()
        {
            return ToggleSrv.GetResponse(TogglWorkspaceUrl).GetData<List<Workspace>>();
        }
    }
}
