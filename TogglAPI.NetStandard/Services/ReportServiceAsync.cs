using Toggl.DataObjects;
using Toggl.Interfaces;
using Toggl.QueryObjects;

namespace Toggl.Services
{
    public class ReportServiceAsync : IReportServiceAsync
    {
        public IApiServiceAsync ToggleSrv { get; set; }
        

	    public ReportServiceAsync(string apiKey)
            : this(new ApiServiceAsync(apiKey))
        {

        }
        
        public ReportServiceAsync(IApiServiceAsync srv)
        {
            ToggleSrv = srv;
        }


        public async System.Threading.Tasks.Task<DetailedReport> Detailed(DetailedReportParams requestParameters)
        {
            var report = await ToggleSrv.Get<DetailedReport>(ApiRoutes.Reports.Detailed, requestParameters.ToKeyValuePair());
            return report;
        }

        public async System.Threading.Tasks.Task<DetailedReport> FullDetailedReport(DetailedReportParams requestParameters)
	    {
		    var report = await Detailed(requestParameters);

		    if (report.TotalCount < report.PerPage)
			    return report;

			var pageCount = (report.TotalCount + report.PerPage - 1) / report.PerPage;

		    DetailedReport resultReport = null;
			for (var page = 1; page <= pageCount; page++)
			{
				requestParameters.Page = page;
				var pagedReport = await Detailed(requestParameters);

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
    }
}
