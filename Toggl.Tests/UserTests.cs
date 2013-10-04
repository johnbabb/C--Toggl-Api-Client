using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Toggl.Extensions;
using Toggl.Services;

namespace Toggl.Tests
{
    [TestFixture]
    public class UserTests
    {
       

        [Test]
        public void GetCurrentTest()
        {
            var t = new UserService();
            var obj = t.GetCurrent();
            
            Assert.AreEqual(Constants.DefaultUserId, obj.Id);
        }
        
        [Test]
        public void GetCurrentExtendedTest()
        {
            var t = new UserService();
            var obj = t.GetCurrentExtended();

            Assert.AreEqual(Constants.DefaultUserId, obj.Id);

            Assert.Greater(obj.Clients.Count(), 0);
            Assert.Greater(obj.Projects.Count(), 0);
            Assert.Greater(obj.Tags.Count(), 0);
            Assert.Greater(obj.TimeEntries.Count(), 0);
            Assert.Greater(obj.Workspaces.Count(), 0);
        }

        [Test]
        public void GetCurrentChangedTest()
        {
            var t = new UserService();
            

            var teSrv = new TimeEntryService();
            teSrv.Add(new TimeEntry{   
                            IsBillable = true,
                            CreatedWith = "TimeEntryTestAdd",
                            Description = "Test Desc" + DateTime.Now.Ticks,
                            Duration = 900,
                            Start = DateTime.Now.ToIsoDateStr(),
                            Stop = DateTime.Now.AddMinutes(20).ToIsoDateStr(),
                            ProjectId =Constants.DefaultProjectId,
                            WorkspaceId = Constants.DefaultWorkspaceId
                    });
            
            var obj = t.GetCurrentChanged(DateTime.Now.AddSeconds(-5));

            Assert.AreEqual(Constants.DefaultUserId, obj.Id);
            Assert.Greater(obj.TimeEntries.Count(), 0);


            
        }
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public void EditTest(int dow)
        {
            var srv = new UserService();
            var exp = srv.GetCurrent();
            exp.BeginningOfWeek = dow;

            var act = srv.Edit(exp);

            Assert.AreEqual(act.BeginningOfWeek, exp.BeginningOfWeek);
        }

        [Test]
        public void AddTest()
        {
            return;
            var srv = new UserService();
            var exp = new User();
            exp.Email = "john.babb" + DateTime.Now.Ticks + "@ikoios.com";
            

            var act = srv.Edit(exp);

            Assert.AreEqual(act.BeginningOfWeek, exp.BeginningOfWeek);
        }
    }
}
