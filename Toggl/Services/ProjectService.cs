using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Toggl.Interfaces;
using Toggl.Properties;

namespace Toggl.Services
{
    public class ProjectService : IProjectService
    {
        private readonly string ProjectsUrl = ApiRoutes.Project.ProjectsUrl;
        

        private IApiService ToggleSrv { get; set; }


        public ProjectService(string apiKey)
            : this(new ApiService(apiKey))
        {

        }

        public ProjectService()
            : this(new ApiService())
        {
        }

        public ProjectService(IApiService srv)
        {
            ToggleSrv = srv;
        }

        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#get_projects
        /// </summary>
        /// <returns></returns>
        public List<Project> List()
        {
            var lstProj = new List<Project>();
            var lstWrkSpc = ToggleSrv.Get(ApiRoutes.Workspace.ListWorkspaceUrl).GetData<List<Workspace>>();
            lstWrkSpc.ForEach(e =>
                {
                    var projs = ForWorkspace(e.Id.Value);
                    lstProj.AddRange(projs);
                });
            return lstProj;
        }
        
        public List<Project> ForWorkspace (int id)
        {
            var url = string.Format(ApiRoutes.Workspace.ListWorkspaceProjectsUrl, id);
            return ToggleSrv.Get(url).GetData<List<Project>>();
        }

        public Project Get(int id)
        {
            return List().Where(w => w.Id == id).FirstOrDefault();
        }

        public Project Add(Project obj)
        {

            return ToggleSrv.Post(ProjectsUrl, obj.ToJson()).GetData<Project>();
        }
       
    }
}
