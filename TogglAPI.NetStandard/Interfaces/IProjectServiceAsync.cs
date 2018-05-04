using System.Collections.Generic;
using System.Threading.Tasks;

namespace Toggl.Interfaces
{
    public interface IProjectServiceAsync
    {
        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#get_projects
        /// </summary>
        /// <returns></returns>
        Task<List<Project>> List();

        Task<Project> Get(int id);

        Task<Project> Add(Project project);
    }
}