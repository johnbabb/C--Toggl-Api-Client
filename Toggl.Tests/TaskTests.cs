using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Toggl.Services;

namespace Toggl.Tests
{
    [TestFixture]
    public class TaskTests : BaseTogglApiTest
    {
	    public static int DefaultProjectId;

		[SetUp]
	    public override void Init()
	    {
		    base.Init();
			
			var project = ProjectService.Add(new Project
			{
				IsBillable = true,
				WorkspaceId = DefaultWorkspaceId,
				Name = "New Project" + DateTime.UtcNow,
				IsAutoEstimates = false
			});

		    DefaultProjectId = project.Id.Value;
	    }

	    /// <summary>
        /// 
        /// 
        /// </summary>
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
    
		
	}
}
