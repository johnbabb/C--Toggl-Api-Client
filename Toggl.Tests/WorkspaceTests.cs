using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Toggl.Services;

namespace Toggl.Tests
{
    [TestFixture]
    public class WorkspaceTests
    {
        [Test]
        public void GetWorkSpaces()
        {
            var t = new WorkspaceService();
            t.List();
            var obj = t.List();

            Assert.GreaterOrEqual(obj.Count(), 0);
        }

        [Test]
        public void GetWorkSpaceProjects()
        {
            var t = new WorkspaceService();
            var obj = t.List().FirstOrDefault();
            var lst = t.Projects(obj.Id.Value);
            Assert.Greater(lst.Count(),0);
        }

        [Test]
        public void GetWorkSpaceUsers()
        {
            var t = new WorkspaceService();
            var obj = t.List().FirstOrDefault();
            var lst = t.Users(obj.Id.Value);
            Assert.Greater(lst.Count(), 0);
        }

        [Test]
        public void GetWorkSpaceTags()
        {
            var t = new WorkspaceService();
            var obj = t.List().FirstOrDefault();
            var lst = t.Tags(obj.Id.Value);
            Assert.Greater(lst.Count(), 0);
        }

        [Test]
        public void GetWorkSpaceTasks()
        {
            var t = new WorkspaceService();
            var obj = t.List().FirstOrDefault();
            var lst = t.Tasks(obj.Id.Value);
            Assert.Greater(lst.Count(), 0);
        }

        [Test]
        public void GetWorkSpaceClients()
        {
            var t = new WorkspaceService();
            var obj = t.List().FirstOrDefault();
            var lst = t.Clients(obj.Id.Value);
            Assert.Greater(lst.Count(), 0);
        }
    }
}
