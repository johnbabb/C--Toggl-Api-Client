using System;
using System.Collections.Generic;
using NUnit.Framework;
using Toggl.Extensions;
using Toggl.QueryObjects;
using Toggl.Services;

namespace Toggl.Tests
{
    [TestFixture]
	public class ReportTests : TogglApiTestWithDefaultProject
    {
        [Test]
        public void GetDetailedReport()
        {
            for (int i = 0; i < 6; i++)
            {
				var expTimeEntry = TimeEntryService.Add(new TimeEntry()
                {
                    IsBillable = true,
                    CreatedWith = "TimeEntryTestAdd",
                    Duration = 900,
                    Start = DateTime.Now.AddDays(-i).ToIsoDateStr(),
                    Stop = DateTime.Now.AddDays(-i).AddMinutes(20).ToIsoDateStr(),
                    ProjectId = DefaultProjectId,
                    WorkspaceId = DefaultWorkspaceId     
                });
                Assert.IsNotNull(expTimeEntry);
            }

			var standardParams = new DetailedReportParams()
			{
				UserAgent = "TogglAPI.Net",
				WorkspaceId = DefaultWorkspaceId,
				Since = DateTime.Now.AddYears(-1).ToIsoDateStr()
			};

            var result = ReportService.Detailed(standardParams);
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
                    ProjectId = DefaultProjectId,
                    WorkspaceId = DefaultWorkspaceId
                };
                var expTimeEntry = timeEntryService.Add(timeEntry);
                Assert.IsNotNull(expTimeEntry);
            }

			var standardParams = new DetailedReportParams()
			{
				UserAgent = "TogglAPI.Net",
				WorkspaceId = DefaultWorkspaceId				
			};

            var result = ReportService.Detailed(standardParams);
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
                ProjectId = DefaultProjectId,
                WorkspaceId = DefaultWorkspaceId
            };
            var expTe = timeEntryService.Add(te);
            Assert.IsNotNull(expTe);

            result = ReportService.Detailed(standardParams);
            Assert.AreEqual(result.Data.Count, 6);
            Assert.AreEqual(result.TotalCount, 6);

            result = ReportService.Detailed(new DetailedReportParams()
                                            {
                                                UserAgent = "test_api",
                                                WorkspaceId = DefaultWorkspaceId,
                                                Since = DateTime.Now.AddDays(-7).ToIsoDateStr()
                                            });
            Assert.AreEqual(result.Data.Count, 7);
            Assert.AreEqual(result.TotalCount, 7);
        }

        [Test]
        public void GetTimeEntriesByTaskId()
        {
			var task1 = TaskService.Add(new Task
			{
				IsActive = true,
				Name = "task1",
				EstimatedSeconds = 3600,
				WorkspaceId = DefaultWorkspaceId,
				ProjectId = DefaultProjectId
			});
			Assert.IsNotNull(task1);

			var task2 = TaskService.Add(new Task
			{
				IsActive = true,
				Name = "task2",
				EstimatedSeconds = 3600,
				WorkspaceId = DefaultWorkspaceId,
				ProjectId = DefaultProjectId
			});
			Assert.IsNotNull(task2);

            var timeEntryService = new TimeEntryService();
            for (int i = 0; i < 3; i++)
            {
                var timeEntry = new TimeEntry()
                {
                    IsBillable = true,
                    CreatedWith = "TimeEntryTestAdd",
                    Description = "Test Desc" + DateTime.Now.Ticks,
                    Duration = 900,
                    Start = DateTime.Now.AddDays(-i).ToIsoDateStr(),
                    Stop = DateTime.Now.AddDays(-i).AddMinutes(20).ToIsoDateStr(),
                    WorkspaceId = DefaultWorkspaceId,
                    TaskId = task1.Id
                };
                var expTimeEntry = timeEntryService.Add(timeEntry);
                Assert.IsNotNull(expTimeEntry);
            }

			for (int i = 0; i < 3; i++)
			{
				var timeEntry = new TimeEntry()
				{
					IsBillable = true,
					CreatedWith = "TimeEntryTestAdd",
					Description = "Test Desc" + DateTime.Now.Ticks,
					Duration = 900,
					Start = DateTime.Now.AddDays(-i).ToIsoDateStr(),
					Stop = DateTime.Now.AddDays(-i).AddMinutes(20).ToIsoDateStr(),
					WorkspaceId = DefaultWorkspaceId,
					TaskId = task2.Id
				};
				var expTimeEntry = timeEntryService.Add(timeEntry);
				Assert.IsNotNull(expTimeEntry);
			}

			var standardParams = new DetailedReportParams()
			{
				UserAgent = "TogglAPI.Net",
				WorkspaceId = DefaultWorkspaceId,
				Since = DateTime.Now.AddYears(-1).ToIsoDateStr(),
				TaskIds = string.Format("{0},{1}", task1.Id.Value, task2.Id.Value)
			};

            var result = ReportService.Detailed(standardParams);
            Assert.AreEqual(result.Data.Count, 6);
            Assert.AreEqual(result.TotalCount, 6);

	        standardParams.TaskIds = task1.Id.Value.ToString();

			result = ReportService.Detailed(standardParams);
			Assert.AreEqual(result.Data.Count, 3);
			Assert.AreEqual(result.TotalCount, 3);

        }
    }
}
