using System.Collections.Generic;
using System.Threading.Tasks;

namespace Toggl.Interfaces
{
    public interface ITaskServiceAsync
    {
        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/tasks.md#get-task-details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		Task<Task> Get(int id);

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/tasks.md#create-a-task
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<Task> Add(Task t);

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/tasks.md#update-a-task
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<Task> Edit(Task t);

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/tasks.md#delete-a-task
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<bool> Delete(int id);
    }
}