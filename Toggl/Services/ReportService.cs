using Toggl.DataObjects;
using Toggl.Interfaces;
using Toggl.QueryObjects;

namespace Toggl.Services
{
    public class ReportService : IReportService
    {
        public IApiService ToggleSrv { get; set; }
        public DetailedReport Detailed(DetailedReportParams requestParameters)
        {
            var report = ToggleSrv.Get<DetailedReport>(ApiRoutes.Reports.Detailed, requestParameters.ToKeyValuePair());
                
            return report;
        }

         public ReportService(string apiKey)
            : this(new ApiService(apiKey))
        {

        }
        
        public ReportService():this(new ApiService())
        {

        }

        public ReportService(IApiService srv)
        {
            ToggleSrv = srv;
        }        
    }
}
