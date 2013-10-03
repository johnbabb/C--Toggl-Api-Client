using System.Collections.Generic;

namespace Toggl.Interfaces
{
    public interface ITaskService
    {
        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#get_tasks
        /// </summary>
        /// <returns></returns>
        List<Task> List();

        Task Get(int id);

        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#post_tasks
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task Add(Task t);

        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#put_tasks
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task Edit(Task t);

        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#del_tasks
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool Delete(int id);
    }
}