using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toggl.DataObjects;
using Toggl.QueryObjects;

namespace Toggl.Interfaces
{
    public interface IReportService
    {
        IApiService ToggleSrv { get; set; }

        DetailedReport Detailed(DetailedReportParams requestParameters);
    }
}
