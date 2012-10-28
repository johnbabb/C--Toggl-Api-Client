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
    public class TaskService
    {
        private const string TogglBaseUrl = "https://www.toggl.com/api/v6";
        private const string TogglTasksUrl = TogglBaseUrl + "/tasks.json";

        private ITogglService ToggleSrv { get; set; }

        public TaskService()
            : this(new TogglService())
        {
        }

        public TaskService(ITogglService srv)
        {
            ToggleSrv = srv;
        }

        public List<Task> GetTasks()
        {
            return ToggleSrv.GetResponse(TogglTasksUrl).GetData<List<Task>>();
        }
       
        
    }
}
