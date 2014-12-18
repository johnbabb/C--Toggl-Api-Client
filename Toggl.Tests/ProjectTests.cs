using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Toggl.Services;

namespace Toggl.Tests
{
    [TestFixture]
    public class ProjectTests : BaseTogglApiTest
    {
		[Test]
        public void Add()
        {
            var project = new Project
            {
                IsBillable = true,
                WorkspaceId = DefaultWorkspaceId,
                Name = "New Project" + DateTime.UtcNow,
                IsAutoEstimates = false
            };

            var act = ProjectService.Add(project);
			
			Assert.NotNull(act, "response back from api does not have at project object");
            Assert.GreaterOrEqual(act.Id, 0, "response back from object does not have a project id greater than zero");

			Assert.AreEqual(1, ProjectService.ForWorkspace(DefaultWorkspaceId).Count);
        }

        [Test]
        public void Get()
        {
			var project = new Project
			{
				IsBillable = true,
				WorkspaceId = DefaultWorkspaceId,
				Name = "New Project" + DateTime.UtcNow,
				IsAutoEstimates = false
			};

			var act = ProjectService.Add(project);
			var loadedProject = ProjectService.Get(act.Id.Value);

			Assert.AreEqual(act.Id, loadedProject.Id);
			Assert.AreEqual(project.IsBillable, loadedProject.IsBillable);
			Assert.AreEqual(project.WorkspaceId, loadedProject.WorkspaceId);
			Assert.AreEqual(project.Name, loadedProject.Name);
			Assert.AreEqual(project.IsAutoEstimates, loadedProject.IsAutoEstimates);		            
        }

		[Test]
	    public void Delete()
	    {
			var project = new Project
			{
				IsBillable = true,
				WorkspaceId = DefaultWorkspaceId,
				Name = "New Project" + DateTime.UtcNow,
				IsAutoEstimates = false
			};

			var act = ProjectService.Add(project);
			Assert.IsTrue(ProjectService.Delete(act.Id.Value));
			Assert.AreEqual(0, ProjectService.ForWorkspace(DefaultWorkspaceId).Count);
	    }

		[Test]
		public void BulkDelete()
		{
			var ids = new List<int>();

			for (int i = 0; i < 3; i++)
			{
				var project = new Project
				{
					IsBillable = true,
					WorkspaceId = DefaultWorkspaceId,
					Name = "New Project" + DateTime.UtcNow,
					IsAutoEstimates = false
				};
				var act = ProjectService.Add(project);				
				ids.Add(act.Id.Value);
			}
			
			Assert.AreEqual(3, ProjectService.ForWorkspace(DefaultWorkspaceId).Count);
			Assert.IsTrue(ProjectService.Delete(ids.ToArray()));
			Assert.AreEqual(0, ProjectService.ForWorkspace(DefaultWorkspaceId).Count);
		}

		// todo test for client
		// todo test for workspaces
    }
}
