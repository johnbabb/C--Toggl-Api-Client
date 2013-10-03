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
    public class TaskService : ITaskService
    {
        private readonly string TogglTasksUrl = ApiRoutes.Task.TogglTasksUrl;
        

        private IApiService ToggleSrv { get; set; }


        public TaskService(string apiKey)
            : this(new ApiService(apiKey))
        {

        }

        public TaskService()
            : this(new ApiService())
        {
        }

        public TaskService(IApiService srv)
        {
            ToggleSrv = srv;
        }

        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#get_tasks
        /// </summary>
        /// <returns></returns>
        public List<Task> List()
        {
            return ToggleSrv.Get(TogglTasksUrl).GetData<List<Task>>();
        }

        public Task Get(int id)
        {
            return List().Where(w => w.Id == id).FirstOrDefault();
        }
       
        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#post_tasks
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Task Add(Task t)
        {
            return ToggleSrv.Post(TogglTasksUrl, t.ToJson()).GetData<Task>();
        }

        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#put_tasks
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Task Edit(Task t)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#del_tasks
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
