using System;
using System.Linq;
using NUnit.Framework;
using Toggl.Extensions;
using Toggl.QueryObjects;
using Toggl.Services;

namespace Toggl.Tests
{
    [TestFixture]
	public class TimeEntryTests : TogglApiTestWithDefaultProject
    {
		[Test]
        public void GetTimeEntries()
        {
            var entries = TimeEntryService.List();
            Assert.AreEqual(entries.Count(), 0);    
        }

		[Test]
		public void GetTimeEntriesByDateRange()
		{
			var startDate = DateTime.Now.AddMonths(-2);
			var endDate = DateTime.Now.AddMonths(-1);

			for (int i = 0; i < 3; i++)
			{
				var te = TimeEntryService.Add(new TimeEntry()
				{
					IsBillable = true,
					CreatedWith = "TogglAPI.Net",
					Duration = 900,
					Start = startDate.AddMonths(i).ToIsoDateStr(),
					WorkspaceId = DefaultWorkspaceId
				});
				Assert.IsNotNull(te);
			}
	       
            var rte = new TimeEntryParams {StartDate = startDate, EndDate = endDate};
			Assert.AreEqual(2, TimeEntryService.List(rte).Count());
			rte = new TimeEntryParams { StartDate = startDate, EndDate = DateTime.Now };
			Assert.AreEqual(3, TimeEntryService.List(rte).Count());            
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

			for (int i = 0; i < 3; i++)
			{
				var timeEntry = TimeEntryService.Add(new TimeEntry()
				{
					IsBillable = true,
					CreatedWith = "TogglAPI.Net",
					Duration = 900,
					Start = DateTime.Now.ToIsoDateStr(),
					WorkspaceId = DefaultWorkspaceId,
					TaskId = task1.Id
				});

				Assert.IsNotNull(timeEntry);
			}

			for (int i = 0; i < 3; i++)
			{
				var timeEntry = TimeEntryService.Add(new TimeEntry()
				{
					IsBillable = true,
					CreatedWith = "TogglAPI.Net",
					Duration = 900,
					Start = DateTime.Now.ToIsoDateStr(),
					WorkspaceId = DefaultWorkspaceId,
					TaskId = task2.Id
				});

				Assert.IsNotNull(timeEntry);
			}

			Assert.AreEqual(3, TimeEntryService.List().Count(te => te.TaskId == task1.Id));
			Assert.AreEqual(3, TimeEntryService.List().Count(te => te.TaskId == task2.Id));
		}

        [Test]
        public void Get()
        {
			var timeEntry = TimeEntryService.Add(new TimeEntry()
			{
				IsBillable = true,
				CreatedWith = "TogglAPI.Net",
				Duration = 900,
				Start = DateTime.Now.ToIsoDateStr(),				
				WorkspaceId = DefaultWorkspaceId
				
			});

			Assert.IsNotNull(timeEntry);

	        var loadedTimeEntry = TimeEntryService.Get(timeEntry.Id.Value);
			Assert.IsNotNull(loadedTimeEntry);
			Assert.AreEqual(timeEntry.Id, loadedTimeEntry.Id);
			Assert.AreEqual(timeEntry.IsBillable, loadedTimeEntry.IsBillable);
			Assert.AreEqual(timeEntry.CreatedWith, loadedTimeEntry.CreatedWith);
			Assert.AreEqual(timeEntry.Duration, loadedTimeEntry.Duration);
			Assert.AreEqual(timeEntry.WorkspaceId, loadedTimeEntry.WorkspaceId);            
        }

        [Test]
        public void Add()
        {
			var timeEntry = TimeEntryService.Add(new TimeEntry()
			{
				IsBillable = true,
				CreatedWith = "TogglAPI.Net",
				Duration = 900,
				Start = DateTime.Now.ToIsoDateStr(),
				Stop = DateTime.Now.AddMinutes(10).ToIsoDateStr(),
				WorkspaceId = DefaultWorkspaceId
			});

			Assert.IsNotNull(timeEntry);
			Assert.AreEqual(1, TimeEntryService.List().Count());
        }

	    [Test]
	    public void Start()
	    {
		    var timeEntry = TimeEntryService.Add(new TimeEntry()
		    {
			    CreatedWith = "TogglAPI.Net",
				Description = "Start a new task",
				WorkspaceId = DefaultWorkspaceId
		    });

			Assert.IsNotNull(timeEntry);
	    }

        [Test]
        public void Edit()
        {
			var timeEntry = TimeEntryService.Add(new TimeEntry()
			{
				IsBillable = true,
				CreatedWith = "TogglAPI.Net",
				Duration = 900,
				Start = DateTime.Now.ToIsoDateStr(),
				WorkspaceId = DefaultWorkspaceId

			});

			Assert.IsNotNull(timeEntry);

			var loadedTimeEntry = TimeEntryService.Get(timeEntry.Id.Value);
			Assert.IsNotNull(loadedTimeEntry);

	        loadedTimeEntry.Duration = 1200;
	        var editedTimeEntry = TimeEntryService.Edit(loadedTimeEntry);

			Assert.AreEqual(timeEntry.Id, editedTimeEntry.Id);
			Assert.AreEqual(timeEntry.IsBillable, editedTimeEntry.IsBillable);
			Assert.AreEqual(timeEntry.CreatedWith, editedTimeEntry.CreatedWith);
			Assert.AreEqual(loadedTimeEntry.Duration, editedTimeEntry.Duration);
			Assert.AreEqual(timeEntry.WorkspaceId, editedTimeEntry.WorkspaceId);        
        }
        
        [Test]
        public void Delete()
        {
			var timeEntry = TimeEntryService.Add(new TimeEntry()
			{
				IsBillable = true,
				CreatedWith = "TogglAPI.Net",
				Duration = 900,
				Start = DateTime.Now.ToIsoDateStr(),
				WorkspaceId = DefaultWorkspaceId

			});

			Assert.IsNotNull(timeEntry);
			Assert.AreEqual(1, TimeEntryService.List().Count());
			Assert.IsTrue(TimeEntryService.Delete(timeEntry.Id.Value));
			Assert.AreEqual(0, TimeEntryService.List().Count());
        }

		[Test]
		public void EditTaskId()
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

			for (int i = 0; i < 3; i++)
			{
				var timeEntry = TimeEntryService.Add(new TimeEntry()
				{
					IsBillable = true,
					CreatedWith = "TogglAPI.Net",
					Duration = 900,
					Start = DateTime.Now.ToIsoDateStr(),
					WorkspaceId = DefaultWorkspaceId,
					TaskId = task1.Id
				});

				Assert.IsNotNull(timeEntry);
			}

			for (int i = 0; i < 3; i++)
			{
				var timeEntry = TimeEntryService.Add(new TimeEntry()
				{
					IsBillable = true,
					CreatedWith = "TogglAPI.Net",
					Duration = 900,
					Start = DateTime.Now.ToIsoDateStr(),
					WorkspaceId = DefaultWorkspaceId,
					TaskId = task2.Id
				});

				Assert.IsNotNull(timeEntry);
			}

			Assert.AreEqual(3, TimeEntryService.List().Count(te => te.TaskId == task1.Id));
			Assert.AreEqual(3, TimeEntryService.List().Count(te => te.TaskId == task2.Id));

			var task2TimeEntries = TimeEntryService.List().Where(te => te.TaskId == task2.Id).ToList();
			foreach (var timeEntry in task2TimeEntries)
			{
				timeEntry.TaskId = task1.Id;
				Assert.IsNotNull(TimeEntryService.Edit(timeEntry));				
			}

			Assert.AreEqual(6, TimeEntryService.List().Count(te => te.TaskId == task1.Id));
			Assert.AreEqual(0, TimeEntryService.List().Count(te => te.TaskId == task2.Id));
		}

    }
 }




