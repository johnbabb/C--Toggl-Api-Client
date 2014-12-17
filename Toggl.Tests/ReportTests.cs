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
        [SetUp]
        public void Init()
        {
            var reportService = new ReportService();
            var reportParams = new DetailedReportParams()
                               {
                                   UserAgent = "test",
                                   WorkspaceId = Constants.DefaultWorkspaceId,
                                   Since = DateTime.Now.AddYears(-1).ToIsoDateStr()
                               };
            var res = reportService.Detailed(reportParams);
            var timeEntryService = new TimeEntryService();
            foreach (var item in res.Data)
            {
                timeEntryService.Delete(item.Id.Value);
            }
            
        }

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
            Assert.AreEqual(result.Data.Count, 0);
            Assert.AreEqual(result.TotalCount, 0);

            var timeEntryService = new TimeEntryService();
            for (int i = 0; i < 6; i++)
            {
                var timeEntry = new TimeEntry()
                                {
                                    IsBillable = true,
                                    CreatedWith = "TimeEntryTestAdd",
                                    Description = "Test Desc" + DateTime.Now.Ticks,
                                    Duration = 900,
                                    Start = DateTime.Now.AddDays(-i).ToIsoDateStr(),
                                    Stop = DateTime.Now.AddDays(-i).AddMinutes(20).ToIsoDateStr(),
                                    ProjectId = Constants.DefaultProjectId,
                                    WorkspaceId = Constants.DefaultWorkspaceId     
                                };
                var expTimeEntry = timeEntryService.Add(timeEntry);
                Assert.IsNotNull(expTimeEntry);
            }

            var te = new TimeEntry()
                     {
                         IsBillable = true,
                         CreatedWith = "TimeEntryTestAdd",
                         Description = "Test Desc" + DateTime.Now.Ticks,
                         Duration = 900,
                         Start = DateTime.Now.AddDays(-7).ToIsoDateStr(),
                         Stop = DateTime.Now.AddDays(-7).AddMinutes(20).ToIsoDateStr(),
                         ProjectId = Constants.DefaultProjectId,
                         WorkspaceId = Constants.DefaultWorkspaceId
                     };
            var expTe = timeEntryService.Add(te);
            Assert.IsNotNull(expTe);

            result = reportService.Detailed(reportParams);
            Assert.AreEqual(result.Data.Count, 6);
            Assert.AreEqual(result.TotalCount, 6);
        }

        [Test]
        public void GetDetailedReportSince()
        {
            var reportService = new ReportService();
            var reportParams = new DetailedReportParams()
            {
                UserAgent = "test_api",
                WorkspaceId = Constants.DefaultWorkspaceId
            };
            var result = reportService.Detailed(reportParams);
            Assert.AreEqual(result.Data.Count, 0);
            Assert.AreEqual(result.TotalCount, 0);

            var timeEntryService = new TimeEntryService();
            for (int i = 0; i < 6; i++)
            {
                var timeEntry = new TimeEntry()
                {
                    IsBillable = true,
                    CreatedWith = "TimeEntryTestAdd",
                    Description = "Test Desc" + DateTime.Now.Ticks,
                    Duration = 900,
                    Start = DateTime.Now.AddDays(-i).ToIsoDateStr(),
                    Stop = DateTime.Now.AddDays(-i).AddMinutes(20).ToIsoDateStr(),
                    ProjectId = Constants.DefaultProjectId,
                    WorkspaceId = Constants.DefaultWorkspaceId
                };
                var expTimeEntry = timeEntryService.Add(timeEntry);
                Assert.IsNotNull(expTimeEntry);
            }

            result = reportService.Detailed(reportParams);
            Assert.AreEqual(result.Data.Count, 6);
            Assert.AreEqual(result.TotalCount, 6);

            var te = new TimeEntry()
            {
                IsBillable = true,
                CreatedWith = "TimeEntryTestAdd",
                Description = "Test Desc" + DateTime.Now.Ticks,
                Duration = 900,
                Start = DateTime.Now.AddDays(-7).ToIsoDateStr(),
                Stop = DateTime.Now.AddDays(-7).AddMinutes(20).ToIsoDateStr(),
                ProjectId = Constants.DefaultProjectId,
                WorkspaceId = Constants.DefaultWorkspaceId
            };
            var expTe = timeEntryService.Add(te);
            Assert.IsNotNull(expTe);

            result = reportService.Detailed(reportParams);
            Assert.AreEqual(result.Data.Count, 6);
            Assert.AreEqual(result.TotalCount, 6);

            result = reportService.Detailed(new DetailedReportParams()
                                            {
                                                UserAgent = "test_api",
                                                WorkspaceId = Constants.DefaultWorkspaceId,
                                                Since = DateTime.Now.AddDays(-7).ToIsoDateStr()
                                            });
            Assert.AreEqual(result.Data.Count, 7);
            Assert.AreEqual(result.TotalCount, 7);
        }
    }
}
