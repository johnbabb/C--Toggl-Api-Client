using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Toggl.DataObjects;
using Toggl.Extensions;
using Toggl.QueryObjects;
using Toggl.Services;

namespace Toggl.Tests
{
    [TestFixture]
    public class TimeEntryTests
    {
        TimeEntryService timeEntrySrv = new TimeEntryService();

        [SetUp]
        public void Init()
        { /* ... */ }

        [TearDown]
        public void Dispose()
        { /* ... */ }
        [Test]
        public void GetTimeEntries()
        {
            var entries = timeEntrySrv.List();
            Assert.GreaterOrEqual(entries.Count(), 0);    
        }
        [Test]
        [TestCase("1/1/2012", "1/1/2013")]
        public void GetTimeEntriesByDateRange(DateTime from, DateTime to)
        {

            var rte = new TimeEntryParams();
            rte.StartDate = from;
            rte.EndDate = to;

            var entries = timeEntrySrv.List(rte);

            Assert.GreaterOrEqual(entries.Count(), 0);

        }
        
        [Test]
        [TestCase(51575828)]
        public void GetTimeEntryByID(int id)
        {

            var entry = timeEntrySrv.Get(id);
            Assert.IsTrue(entry.Id == id);
        }
        [Test]
        public void Add()
        {
      
      
            var tags = new List<string>();
            tags.Add("one");

            var act = new TimeEntry()
                          {
                              IsBillable = true,
                              CreatedWith = "TimeEntryTestAdd",
                              Description = "Test Desc" + DateTime.Now.Ticks,
                              Duration = 900,
                              Start = DateTime.Now.ToIsoDateStr(),
                              Stop = DateTime.Now.AddMinutes(20).ToIsoDateStr(),
                              ProjectId =Constants.DefaultProjectId,
                              TagNames = tags,
                              WorkspaceId = Constants.DefaultWorkspaceId
                
            };

            var exp = timeEntrySrv.Add(act);

            Assert.GreaterOrEqual(exp.Id, 0);
        }

        [Test]
        public void Edit()
        {

            
            var tags = new List<string>();
            tags.Add("one");
            tags.Add("two");

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
            exp = timeEntrySrv.Add(exp);

            Assert.NotNull(exp);
            Assert.Greater(exp.Id, 0);

            exp = timeEntrySrv.Get(exp.Id.Value);

            exp.Duration += 1000;
            exp.Description += "more by edit";
            tags.Add(exp.Duration.ToString());
            exp.TagNames = tags;
            
           

            var act = timeEntrySrv.Edit(exp);

            Assert.NotNull(act);
            Assert.Greater(act.Id, 0);
            Assert.AreEqual(act.Id, exp.Id);
        }
        
        [Test]
        public void Delete()
        {
            var actLst = timeEntrySrv.List();
            var act = actLst.LastOrDefault();
            var expCnt = actLst.Count()-1;
            

            var exp = timeEntrySrv.Delete((int)act.Id);

            var actCnt = timeEntrySrv.List().Count();

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

            var tmp = timeEntrySrv.Add(act);
            Assert.IsNotNull(tmp);
            var exp = timeEntrySrv.Get(tmp.Id.Value);
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

            var tmp = timeEntrySrv.Add(act);
            Assert.IsNotNull(tmp);
            var exp = timeEntrySrv.Get(tmp.Id.Value);
            Assert.IsNotNull(exp);
            Assert.IsNotNull(exp.WorkspaceId.Value);
        }

    }

    }




