using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using NUnit.Framework;
using Toggl.DataObjects;
using Toggl.Extensions;
using Toggl.QueryObjects;
using Toggl.Services;

namespace Toggl.Tests
{
    [TestFixture]
    public class TimeEntryTests : BaseTogglApiTest
    {
        [Test]
        public void GetTimeEntries()
        {
            var entries = TimeEntryService.List();
            Assert.GreaterOrEqual(entries.Count(), 0);    
        }
        [Test]
        [TestCase("1/1/2012", "1/1/2013")]
        public void GetTimeEntriesByDateRange(DateTime from, DateTime to)
        {

            var rte = new TimeEntryParams {StartDate = @from, EndDate = to};

            var entries = TimeEntryService.List(rte);

            Assert.GreaterOrEqual(entries.Count(), 0);
        }
        
        [Test]
        [TestCase(51575828)]
        public void GetTimeEntryByID(int id)
        {
            var entry = TimeEntryService.Get(id);
            Assert.IsTrue(entry.Id == id);
        }
        [Test]
        public void Add()
        {           
            var tags = new List<string> {"one"};

            var act = new TimeEntry()
            {
                IsBillable = true,
                CreatedWith = "TimeEntryTestAdd",
                Description = "Test Desc" + DateTime.Now.Ticks,
                Duration = 900,
                Start = DateTime.Now.ToIsoDateStr(),
                Stop = DateTime.Now.AddMinutes(20).ToIsoDateStr(),
                ProjectId = Constants.DefaultProjectId,
                TagNames = tags,
                WorkspaceId = Constants.DefaultWorkspaceId                
            };

            var exp = TimeEntryService.Add(act);

            Assert.GreaterOrEqual(exp.Id, 0);
            Assert.AreEqual(exp.Description, act.Description);
            Assert.AreEqual(exp.Duration, act.Duration);
            Assert.AreEqual(exp.Start, act.Start);
            Assert.AreEqual(exp.Stop, act.Stop);
            Assert.AreEqual(exp.ProjectId, act.ProjectId);
        }

        [Test]
        public void Edit()
        {
            var tags = new List<string> {"one", "two"};

            var exp = new TimeEntry()
            {
                Id = Constants.DefaultTimeEntryId,
                IsBillable = true,
                CreatedWith = "TimeEntryTestAdd",
                Description = "Test Desc" + DateTime.Now.Ticks,
                Duration = 1000,
                Start = DateTime.Now.ToIsoDateStr(),
                //Stop =  DateTime.Now.AddMinutes(20).ToIsoDateStr(),
                ProjectId = Constants.DefaultProjectId,
                TagNames = tags,
                WorkspaceId = Constants.DefaultWorkspaceId

            };
            exp = TimeEntryService.Add(exp);

            Assert.NotNull(exp);
            Assert.Greater(exp.Id, 0);

            exp = TimeEntryService.Get(exp.Id.Value);

            exp.Duration += 1000;
            exp.Description += "more by edit";
            tags.Add(exp.Duration.ToString());
            exp.TagNames = tags;
            
            var act = TimeEntryService.Edit(exp);

            Assert.NotNull(act);
            Assert.Greater(act.Id, 0);
            Assert.AreEqual(exp.Id, act.Id);
            Assert.AreEqual(exp.Duration, act.Duration);
            Assert.AreEqual(exp.Description, act.Description);
            Assert.AreEqual(exp.TagNames.Count, act.TagNames.Count);
        }
        
        [Test]
        public void Delete()
        {
            var actLst = TimeEntryService.List();
            var act = actLst.LastOrDefault();
            var expCnt = actLst.Count()-1;
            

            var exp = TimeEntryService.Delete((int)act.Id);

            var actCnt = TimeEntryService.List().Count();

            Assert.AreEqual(actCnt, expCnt);
        }
        
        [Test]
        public void HasProject()
        {

            var act = new TimeEntry()
            {
                IsBillable = true,
                CreatedWith = "TimeEntryTestAdd",
                Description = "Test Desc" + DateTime.Now.Ticks,
                Duration = 1000,
                Start = DateTime.Now.ToIsoDateStr(),
                Stop = DateTime.Now.AddMinutes(20).ToIsoDateStr(),
                ProjectId = Constants.DefaultProjectId

            };

            var tmp = TimeEntryService.Add(act);
            Assert.IsNotNull(tmp);
            var exp = TimeEntryService.Get(tmp.Id.Value);
            Assert.IsNotNull(exp);
            Assert.IsNotNull(exp.ProjectId.Value);
        }

        [Test]
        public void HasWorkspace()
        {

            var act = new TimeEntry()
            {
                IsBillable = true,
                CreatedWith = "TimeEntryTestAdd",
                Description = "Test Desc" + DateTime.Now.Ticks,
                Duration = 1000,
                Start = DateTime.Now.ToIsoDateStr(),
                Stop = DateTime.Now.AddMinutes(20).ToIsoDateStr(),
                
                WorkspaceId = Constants.DefaultWorkspaceId

            };

            var tmp = TimeEntryService.Add(act);
            Assert.IsNotNull(tmp);
            var exp = TimeEntryService.Get(tmp.Id.Value);
            Assert.IsNotNull(exp);
            Assert.IsNotNull(exp.WorkspaceId.Value);
        }

    }

    }




