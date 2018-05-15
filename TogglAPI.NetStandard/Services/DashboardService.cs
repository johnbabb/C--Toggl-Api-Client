using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toggl.DataObjects;
using Toggl.Interfaces;

namespace Toggl.Services
{
    public class DashboardService : IDashboardService
    {
        public IApiService ToggleSrv { get; set; }

        public Dashboard Get(int workspaceID)
        {
            var url = string.Format(ApiRoutes.Dashboard.DashboardUrl, workspaceID);
            var dashboard = ToggleSrv.Get<Dashboard>(url);

            return dashboard;
        }

        public DashboardService(string apiKey)
            : this(new ApiService(apiKey))
        {

        }

        public DashboardService(IApiService srv)
        {
            ToggleSrv = srv;
        }
    }
}
