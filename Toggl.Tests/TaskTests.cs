using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Toggl.Services;

namespace Toggl.Tests
{
	using global::Toggl.Extensions;
	using global::Toggl.QueryObjects;

	[TestFixture]
	public class TaskTests : TogglApiTestWithDefaultProject
    {
	    [Test]
        public void Add()
        {
			var task = TaskService.Add(new Task
            {
                IsActive = true,
                Name = "test123",
                EstimatedSeconds = 3600,
                WorkspaceId = DefaultWorkspaceId,
				ProjectId = DefaultProjectId
            });
			Assert.IsNotNull(task);        
    		Assert.AreEqual(1, WorkspaceService.Tasks(DefaultWorkspaceId).Count);
        }

		[Test]
	    public void Get()
	    {
			var task = TaskService.Add(new Task
			{
				IsActive = true,
				Name = "test123",
				EstimatedSeconds = 3600,
				WorkspaceId = DefaultWorkspaceId,
				ProjectId = DefaultProjectId
			});
			Assert.IsNotNull(task);

			var loadedTask = TaskService.Get(task.Id.Value);
			Assert.IsNotNull(loadedTask);
			Assert.AreEqual(task.Id, loadedTask.Id);
			Assert.AreEqual(task.IsActive, loadedTask.IsActive);
			Assert.AreEqual(task.Name, loadedTask.Name);
			Assert.AreEqual(task.EstimatedSeconds, loadedTask.EstimatedSeconds);
			Assert.AreEqual(task.WorkspaceId, loadedTask.WorkspaceId);
			Assert.AreEqual(task.ProjectId, loadedTask.ProjectId);
			Assert.AreEqual(1, WorkspaceService.Tasks(DefaultWorkspaceId).Count);
	    }

		[Test]
		public void Edit()
		{
			var task = TaskService.Add(new Task
			{
				IsActive = true,
				Name = "test123",
				EstimatedSeconds = 3600,
				WorkspaceId = DefaultWorkspaceId,
				ProjectId = DefaultProjectId
			});
			Assert.IsNotNull(task);

			var loadedTask = TaskService.Get(task.Id.Value);
			loadedTask.Name = "test2";
			var editedTask = TaskService.Edit(loadedTask);

			Assert.IsNotNull(editedTask);
			Assert.AreEqual(task.Id, editedTask.Id);
			Assert.AreEqual(task.IsActive, editedTask.IsActive);
			Assert.AreEqual(loadedTask.Name, editedTask.Name);
			Assert.AreEqual(task.EstimatedSeconds, editedTask.EstimatedSeconds);
			Assert.AreEqual(task.WorkspaceId, editedTask.WorkspaceId);
			Assert.AreEqual(task.ProjectId, editedTask.ProjectId);
			Assert.AreEqual(1, WorkspaceService.Tasks(DefaultWorkspaceId).Count);
		}

		[Test]
		public void Delete()
		{
			var task = TaskService.Add(new Task
			{
				IsActive = true,
				Name = "test123",
				EstimatedSeconds = 3600,
				WorkspaceId = DefaultWorkspaceId,
				ProjectId = DefaultProjectId
			});
			Assert.IsNotNull(task);

			Assert.IsTrue(TaskService.Delete(task.Id.Value));
			Assert.AreEqual(0, WorkspaceService.Tasks(DefaultWorkspaceId).Count);
		}

		[Test]
		public void BulkDelete()
		{
			var ids = new List<int>();
			for (int i = 0; i < 3; i++)
			{
				var task = TaskService.Add(new Task
				{
					IsActive = true,
					Name = "test" + i,
					EstimatedSeconds = 3600,
					WorkspaceId = DefaultWorkspaceId,
					ProjectId = DefaultProjectId
				});
				Assert.IsNotNull(task);
				ids.Add(task.Id.Value);
			}
			Assert.AreEqual(3, WorkspaceService.Tasks(DefaultWorkspaceId).Count);

			Assert.IsTrue(TaskService.Delete(ids.ToArray()));
			Assert.AreEqual(0, WorkspaceService.Tasks(DefaultWorkspaceId).Count);
		}

	    [Test]
	    public void MergeTwoTasks()
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

			var timeEntryService = new TimeEntryService(Constants.ApiToken);
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

			Assert.AreEqual(3, TimeEntryService.List().Count(te => te.TaskId == task1.Id.Value));
			Assert.AreEqual(3, TimeEntryService.List().Count(te => te.TaskId == task2.Id.Value));

		    TaskService.Merge(task1.Id.Value, task2.Id.Value, DefaultWorkspaceId);

			Assert.AreEqual(6, TimeEntryService.List().Count(te => te.TaskId == task1.Id.Value));
			Assert.AreEqual(0, TimeEntryService.List().Count(te => te.TaskId == task2.Id.Value));
			Assert.IsFalse(WorkspaceService.Tasks(DefaultWorkspaceId).Select(t => t.Id).Contains(task2.Id));
	    }

		[Test]
		public void MergeMoreThanTwoTasks()
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

			var task3 = TaskService.Add(new Task
			{
				IsActive = true,
				Name = "task3",
				EstimatedSeconds = 3600,
				WorkspaceId = DefaultWorkspaceId,
				ProjectId = DefaultProjectId
			});
			Assert.IsNotNull(task3);

			var timeEntryService = new TimeEntryService(Constants.ApiToken);
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
					TaskId = task3.Id
				};
				var expTimeEntry = timeEntryService.Add(timeEntry);
				Assert.IsNotNull(expTimeEntry);
			}

			Assert.AreEqual(3, TimeEntryService.List().Count(te => te.TaskId == task1.Id.Value));
			Assert.AreEqual(3, TimeEntryService.List().Count(te => te.TaskId == task2.Id.Value));
			Assert.AreEqual(3, TimeEntryService.List().Count(te => te.TaskId == task3.Id.Value));

			TaskService.Merge(task1.Id.Value, new [] { task2.Id.Value, task3.Id.Value}, DefaultWorkspaceId);

			Assert.AreEqual(9, TimeEntryService.List().Count(te => te.TaskId == task1.Id.Value));
			Assert.AreEqual(0, TimeEntryService.List().Count(te => te.TaskId == task2.Id.Value));
			Assert.AreEqual(0, TimeEntryService.List().Count(te => te.TaskId == task3.Id.Value));
			Assert.IsFalse(WorkspaceService.Tasks(DefaultWorkspaceId).Select(t => t.Id).Contains(task2.Id));
			Assert.IsFalse(WorkspaceService.Tasks(DefaultWorkspaceId).Select(t => t.Id).Contains(task3.Id));
		}		
    }
}
