using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
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
            var entries = timeEntrySrv.GetTimeEntries();
            Assert.GreaterOrEqual(entries.Count(), 0);    
        }
        [Test]
        [TestCase("1/1/2012", "1/1/2013")]
        public void GetTimeEntriesByDateRange(string from, string to)
        {
            
            var rte = new QueryObjects.TimeEntryParams();
            rte.start_date = Convert.ToDateTime(from);
            rte.end_date = Convert.ToDateTime(to);

            var entries = timeEntrySrv.GetTimeEntries(rte);

            Assert.GreaterOrEqual(entries.Count(), 0);

        }
        
        [Test]
        [TestCase(51194253)]
        [TestCase(51194255)]
        public void GetTimeEntryByID(int id)
        {

            var entry = timeEntrySrv.GetTimeEntry(id);
            Assert.IsTrue(entry.Id == id);
        }
    }
}
