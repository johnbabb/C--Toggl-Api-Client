using System;
using System.Collections.Generic;
using NUnit.Framework;
using Toggl.Extensions;
using Toggl.QueryObjects;
using Toggl.Services;

namespace Toggl.Tests
{
    [TestFixture]
    public class ReportTests
    {
        public static ReportService reportService;
        public static DetailedReportParams standardParams;
        [SetUp]
        public void Init()
        {
            reportService = new ReportService();
            standardParams = new DetailedReportParams()
                               {
                                   UserAgent = "test",
                                   WorkspaceId = Constants.DefaultWorkspaceId,
                                   Since = DateTime.Now.AddYears(-1).ToIsoDateStr()
                               };
            var res = reportService.Detailed(standardParams);
            var timeEntryService = new TimeEntryService();
            foreach (var item in res.Data)
            {
                timeEntryService.Delete(item.Id.Value);
            }

            var result = reportService.Detailed(standardParams);
            Assert.AreEqual(result.Data.Count, 0);
            Assert.AreEqual(result.TotalCount, 0);
            
        }

        [Test]
        public void GetDetailedReport()
        {
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

            var result = reportService.Detailed(standardParams);
            Assert.AreEqual(result.Data.Count, 6);
            Assert.AreEqual(result.TotalCount, 6);
        }

        [Test]
        public void GetDetailedReportSince()
        {
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

            var result = reportService.Detailed(standardParams);
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

            result = reportService.Detailed(standardParams);
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

        [Test]
        public void GetTimeEntriesByTaskId()
        {
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
                    WorkspaceId = Constants.DefaultWorkspaceId,
                    TaskId = i % 2
                };
                var expTimeEntry = timeEntryService.Add(timeEntry);
                Assert.IsNotNull(expTimeEntry);
            }

            standardParams.TaskIds = new List<int>() {0};
            var result = reportService.Detailed(standardParams);
            Assert.AreEqual(result.Data.Count, 6);
            Assert.AreEqual(result.TotalCount, 6);
        }
    }
}
