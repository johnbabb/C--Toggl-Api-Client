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

        private readonly string ListWorkspaceUrl = ApiRoutes.Workspace.ListWorkspaceUrl;


        private ITogglService ToggleSrv { get; set; }

        public WorkspaceService(string apiKey)
            : this(new TogglService(apiKey))
        {

        }

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
            return ToggleSrv.GetResponse(ListWorkspaceUrl).GetData<List<Workspace>>();
        }
    }
}
