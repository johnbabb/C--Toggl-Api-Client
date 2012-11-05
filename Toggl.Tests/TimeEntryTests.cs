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
                              Billable = true,
                              CreatedWith = "TimeEntryTestAdd",
                              Description = "Test Desc" + DateTime.Now.Ticks,
                              Duration = 900,
                              Start = DateTime.Now.ToIsoDateStr(),
                              Stop = DateTime.Now.AddMinutes(20).ToIsoDateStr(),
                              Project = new Project{Id = Constants.DefaultProjectId},
                              TagNames = tags,
                              Workspace = new Workspace(){Id=Constants.DefaultWorkspaceId}
                
            };

            var exp = timeEntrySrv.Add(act);

            Assert.GreaterOrEqual(exp.Id, 0);
        }

        [Test]
        public void Edit()
        {

            var act = timeEntrySrv.List().FirstOrDefault();
            if (act != null && act.Id > 0)
            {
                act.Duration += 1000;
                act.Description += "more by edit";
                var tags = new List<string>();
                tags.Add(act.Duration.ToString());
                act.TagNames = tags;


            } else {
                var tags = new List<string>();
                tags.Add("one");
                tags.Add("two");

                act = new TimeEntry()
                {
                    Id = Constants.DefaultTimeEntryId,
                    Billable = true,
                    CreatedWith = "TimeEntryTestAdd",
                    //Description = "Test Desc" + DateTime.Now.Ticks,
                    Duration = 1000,
                    Start = DateTime.Now.ToIsoDateStr(),
                    //Stop =  DateTime.Now.AddMinutes(20).ToIsoDateStr(),
                    Project = new Project { Id = Constants.DefaultProjectId },
                    TagNames = tags,
                    Workspace = new Workspace() { Id = Constants.DefaultWorkspaceId }

                };
            }
           

            var exp = timeEntrySrv.Edit(act);

            Assert.GreaterOrEqual(exp.Id, 0);
        }
        


    }
}
