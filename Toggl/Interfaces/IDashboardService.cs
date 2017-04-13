using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toggl.DataObjects;

namespace Toggl.Interfaces
{
    public interface IDashboardService
    {
        IApiService ToggleSrv { get; set; }

        Dashboard Get(int workspaceID);
    }
}
