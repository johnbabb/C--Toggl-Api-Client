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
    public class UserTests : BaseTogglApiTest
    {

		[Test]
		public void GetViaRestSharp()
		{
			var restSharpClient = new TogglApiViaRestSharp("6eae86fe55a39666057f045af4e3ca83", "api_token");
			var user = restSharpClient.GetUserInfo();

			Assert.IsNotNull(user);


		}

        [Test]
        public void GetCurrentTest()
        {
            var currentUser = UserService.GetCurrent();
            Assert.IsNotNull(currentUser);
        }

        [Test]
        public void GetCurrentExtendedTest()
        {
            var currentUser = UserService.GetCurrentExtended();

            Assert.AreEqual(0, currentUser.Clients.Count(client => client.DeletedAt == null));
            Assert.IsNull(currentUser.Projects);
            Assert.IsNull(currentUser.Tags);
            Assert.IsNull(currentUser.TimeEntries);
            Assert.AreEqual(1, currentUser.Workspaces.Count());
        }

		/*
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
		 * */

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
			var srv = new UserService(Constants.ApiToken);
            var exp = srv.GetCurrent();
            exp.BeginningOfWeek = dow;

            var act = srv.Edit(exp);

            Assert.AreEqual(act.BeginningOfWeek, exp.BeginningOfWeek);
        }

        [Test]
        public void AddTest()
        {
            return;
			var srv = new UserService(Constants.ApiToken);
            var exp = new User();
            exp.Email = "john.babb" + DateTime.Now.Ticks + "@ikoios.com";
            

            var act = srv.Edit(exp);

            Assert.AreEqual(act.BeginningOfWeek, exp.BeginningOfWeek);
        }

        [Test]
        public void ResetApiToken()
        {
            var oldToken = Constants.ApiToken;
            var srv = new UserService(oldToken);

            var act = srv.ResetApiToken();
            Constants.ApiToken = act;           // Update the ApiToken, so that other tests can use the new active apiToken
            Assert.AreNotEqual(oldToken, act);
        }

    }
}
