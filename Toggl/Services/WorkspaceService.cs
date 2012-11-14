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

        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#get_workspaces
        /// </summary>
        /// <returns></returns>
        public List<Workspace> List()
        {
            return ToggleSrv.Get(ListWorkspaceUrl).GetData<List<Workspace>>();
        }

        public List<User> Users()
        {
            throw new NotImplementedException();
            //return ToggleSrv.Get(ApiRoutes.Workspace.ListWorkspaceUsersUrl).GetData<List<User>>();

        }
    }
}
