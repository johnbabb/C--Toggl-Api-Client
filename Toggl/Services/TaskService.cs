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

        public Task Get(int id)
        {
	        var url = string.Format(ApiRoutes.Task.TogglTasksGet, id);
			return ToggleSrv.Get(url).GetData<Task>();
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
			var url = string.Format(ApiRoutes.Task.TogglTasksGet, t.Id);
			return ToggleSrv.Put(url, t.ToJson()).GetData<Task>();
        }

        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#del_tasks
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
			var url = string.Format(ApiRoutes.Task.TogglTasksGet, id);

			var rsp = ToggleSrv.Delete(url);

			return rsp.StatusCode == HttpStatusCode.OK;            
        }
		
		public bool DeleteIfAny(int[] ids)
		{
			if (!ids.Any() || ids == null)
				return true;
			return Delete(ids);
		}

	    public bool Delete(int[] ids)
	    {
			if (!ids.Any() || ids == null)
				throw new ArgumentNullException("ids");

		    var url = string.Format(
			    ApiRoutes.Task.TogglTasksGet,
			    string.Join(",", ids.Select(id => id.ToString()).ToArray()));

		    var rsp = ToggleSrv.Delete(url);

		    return rsp.StatusCode == HttpStatusCode.OK;
	    }

        public List<Task> ForProject(int id)
        {
            var url = string.Format(ApiRoutes.Project.ProjectTasksUrl, id);
            return ToggleSrv.Get(url).GetData<List<Task>>();
        }
    }
}
