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

            var obj = t.GetWorkspaces();

            Assert.GreaterOrEqual(obj.Count(), 0);
        }
    }
}
