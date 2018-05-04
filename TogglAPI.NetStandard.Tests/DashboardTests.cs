using NUnit.Framework;
using System;
using System.Collections.Generic;
using Toggl.Extensions;

namespace Toggl.Tests
{
    public class DashboardTests : TogglApiTestWithDefaultProject
    {
        [Test]
        public void GetDashboard()
        {
            var expTimeEntry = TimeEntryService.Add(new TimeEntry()
            {
                IsBillable = true,
                CreatedWith = "TimeEntryTestAdd",
                Description = "Test",
                Duration = 900,
                Start = DateTime.Now.ToIsoDateStr(),
                Stop = DateTime.Now.AddMinutes(20).ToIsoDateStr(),
                ProjectId = DefaultProjectId,
                WorkspaceId = DefaultWorkspaceId,
                TagNames = new List<string> { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
            });
            Assert.IsNotNull(expTimeEntry);

            var currentUser = UserService.GetCurrent();
            Assert.IsNotNull(currentUser);
            Assert.IsNotNull(currentUser.DefaultWorkspaceId);

            var result = DashboardService.Get((int)currentUser.DefaultWorkspaceId);
            Assert.IsNotNull(result);
        }
    }
}