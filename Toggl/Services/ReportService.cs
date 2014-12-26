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

	    public DetailedReport FullDetailedReport(DetailedReportParams requestParameters)
	    {
		    var report = this.Detailed(requestParameters);

		    if (report.TotalCount < report.PerPage)
			    return report;

			var pageCount = (report.TotalCount + report.PerPage - 1) / report.PerPage;

		    DetailedReport resultReport = null;
			for (var page = 1; page <= pageCount; page++)
			{
				requestParameters.Page = page;
				var pagedReport = Detailed(requestParameters);

				if (resultReport == null)
				{
					resultReport = pagedReport;
				}
				else
				{
					resultReport.Data.AddRange(pagedReport.Data);
				}
		    }

		    return resultReport;

	    }

	    public ReportService(string apiKey)
            : this(new ApiService(apiKey))
        {

        }
        
        public ReportService(IApiService srv)
        {
            ToggleSrv = srv;
        }        
    }
}
