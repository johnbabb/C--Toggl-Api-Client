 using System.Collections.Generic;
using System.Threading.Tasks;

namespace Toggl.Interfaces
{
    public interface ITagServiceAsync
    {
        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/tasks.md
        /// </summary>
        /// <returns></returns>
        Task<List<Client>> List();
    }
}