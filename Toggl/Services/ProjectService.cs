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
    public class ProjectService
    {
        private readonly string ProjectsUrl = ApiRoutes.Project.ProjectsUrl;
        

        private ITogglService ToggleSrv { get; set; }


        public ProjectService(string apiKey)
            : this(new TogglService(apiKey))
        {

        }

        public ProjectService()
            : this(new TogglService())
        {
        }

        public ProjectService(ITogglService srv)
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
            return ToggleSrv.Get(ProjectsUrl).GetData<List<Project>>();
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
