using System;
using System.Linq;
using NUnit.Framework;
using Toggl.Extensions;
using Toggl.QueryObjects;
using Toggl.Services;

namespace Toggl.Tests
{
    [TestFixture]
    public class ReportTests
    {
        [Test]
        public void GetDetailedReport()
        {
            var reportService = new ReportService();
            var reportParams = new DetailedReportParams()
                               {
                                   UserAgent = "test_api",
                                   WorkspaceId = Constants.DefaultWorkspaceId                                   
                               };
            var result = reportService.Detailed(reportParams);
            Assert.AreEqual(result.Data.Count, 14);
            Assert.AreEqual(result.TotalCount, 14);
        }

        [Test]
        public void GetDetailedReportSince()
        {
            var reportService = new ReportService();
            var reportParams = new DetailedReportParams()
                               {
                                   UserAgent = "test_api",
                                   WorkspaceId = Constants.DefaultWorkspaceId,
                                   Since = DateTime.Now.AddYears(-1).ToIsoDateStr()                                   
                               };

            var result = reportService.Detailed(reportParams);
            var timeEntries = result.Data.OrderBy(te => te.Start).ToList();
            Assert.IsNotNull(result);
        }
    }
}
