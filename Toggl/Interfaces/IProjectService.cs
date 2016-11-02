using System.Collections.Generic;

namespace Toggl.Interfaces
{
    public interface IProjectService
    {
        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#get_projects
        /// </summary>
        /// <returns></returns>
        List<Project> List();

        Project Get(int id);
        Project Add(Project project);
    }
}