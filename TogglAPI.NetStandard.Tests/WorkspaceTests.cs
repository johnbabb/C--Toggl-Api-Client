using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Toggl.Services;

namespace Toggl.Tests
{
    [TestFixture]
    public class WorkspaceTests : BaseTogglApiTest
    {
		[Test]
		public void GetWorkSpacesRestSharp()
		{
			var client = new TogglApiViaRestSharp("6eae86fe55a39666057f045af4e3ca83", "api_token");

			var workspaces = client.GetWorkspaces();
			Assert.AreEqual(workspaces.Count(), 1); //by default user also have one workspace
		}

		[Test]
        public void GetWorkSpaces()
	    {
		    var workspaces = WorkspaceService.List();
            Assert.AreEqual(workspaces.Count(), 1);
        }

        [Test]
        public void GetWorkSpaceProjects()
        {
			var workspace = WorkspaceService.List().SingleOrDefault();
			Assert.IsFalse(WorkspaceService.Projects(workspace.Id.Value).Any());

	        ProjectService.Add(new Project()
								{
									
								});

			var t = new WorkspaceService(Constants.ApiToken);
            var obj = t.List().FirstOrDefault();
            var lst = t.Projects(obj.Id.Value);
            Assert.Greater(lst.Count(),0);
        }

        [Test]
        public void GetWorkSpaceUsers()
        {
			var t = new WorkspaceService(Constants.ApiToken);
            var obj = t.List().FirstOrDefault();
            var lst = t.Users(obj.Id.Value);
            Assert.Greater(lst.Count(), 0);
        }

        [Test]
        public void GetWorkSpaceTags()
        {
			var t = new WorkspaceService(Constants.ApiToken);
            var obj = t.List().FirstOrDefault();
            var lst = t.Tags(obj.Id.Value);
            Assert.Greater(lst.Count(), 0);
        }

        [Test]
        public void GetWorkSpaceTasks()
        {
			var t = new WorkspaceService(Constants.ApiToken);
            var obj = t.List().FirstOrDefault();
            var lst = t.Tasks(obj.Id.Value);
            Assert.Greater(lst.Count(), 0);
        }

        [Test]
        public void GetWorkSpaceClients()
        {
			var t = new WorkspaceService(Constants.ApiToken);
            var obj = t.List().FirstOrDefault();
            var lst = t.Clients(obj.Id.Value);
            Assert.Greater(lst.Count(), 0);
        }
    }
}
