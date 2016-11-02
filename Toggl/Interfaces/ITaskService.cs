using System.Collections.Generic;

namespace Toggl.Interfaces
{
    public interface ITaskService
    {
        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/tasks.md#get-task-details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		Task Get(int id);

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/tasks.md#create-a-task
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task Add(Task t);

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/tasks.md#update-a-task
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task Edit(Task t);

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/tasks.md#delete-a-task
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool Delete(int id);
    }
}