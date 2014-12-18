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
            Assert.AreEqual(entries.Count(), 0);    
        }

		[Test]
		public void GetTimeEntriesByDateRange()
		{
			var startDate = DateTime.Now.AddMonths(-2);
			var endDate = DateTime.Now.AddMonths(-1);

			for (int i = 0; i < 3; i++)
			{
				var te = TimeEntryService.Add(new TimeEntry()
				{
					IsBillable = true,
					CreatedWith = "TogglAPI.Net",
					Duration = 900,
					Start = startDate.AddMonths(i).ToIsoDateStr(),
					WorkspaceId = DefaultWorkspaceId
				});
				Assert.IsNotNull(te);
			}
	       
            var rte = new TimeEntryParams {StartDate = startDate, EndDate = endDate};
			Assert.AreEqual(2, TimeEntryService.List(rte).Count());
			rte = new TimeEntryParams { StartDate = startDate, EndDate = DateTime.Now };
			Assert.AreEqual(3, TimeEntryService.List(rte).Count());            
        }
        
        [Test]
        public void Get()
        {
			var timeEntry = TimeEntryService.Add(new TimeEntry()
			{
				IsBillable = true,
				CreatedWith = "TogglAPI.Net",
				Duration = 900,
				Start = DateTime.Now.ToIsoDateStr(),				
				WorkspaceId = DefaultWorkspaceId
				
			});

			Assert.IsNotNull(timeEntry);

	        var loadedTimeEntry = TimeEntryService.Get(timeEntry.Id.Value);
			Assert.IsNotNull(loadedTimeEntry);
			Assert.AreEqual(timeEntry.Id, loadedTimeEntry.Id);
			Assert.AreEqual(timeEntry.IsBillable, loadedTimeEntry.IsBillable);
			Assert.AreEqual(timeEntry.CreatedWith, loadedTimeEntry.CreatedWith);
			Assert.AreEqual(timeEntry.Duration, loadedTimeEntry.Duration);
			Assert.AreEqual(timeEntry.WorkspaceId, loadedTimeEntry.WorkspaceId);            
        }

        [Test]
        public void Add()
        {
			var timeEntry = TimeEntryService.Add(new TimeEntry()
			{
				IsBillable = true,
				CreatedWith = "TogglAPI.Net",
				Duration = 900,
				Start = DateTime.Now.ToIsoDateStr(),
				Stop = DateTime.Now.AddMinutes(10).ToIsoDateStr(),
				WorkspaceId = DefaultWorkspaceId
			});

			Assert.IsNotNull(timeEntry);
			Assert.AreEqual(1, TimeEntryService.List().Count());
        }

        [Test]
        public void Edit()
        {
			var timeEntry = TimeEntryService.Add(new TimeEntry()
			{
				IsBillable = true,
				CreatedWith = "TogglAPI.Net",
				Duration = 900,
				Start = DateTime.Now.ToIsoDateStr(),
				WorkspaceId = DefaultWorkspaceId

			});

			Assert.IsNotNull(timeEntry);

			var loadedTimeEntry = TimeEntryService.Get(timeEntry.Id.Value);
			Assert.IsNotNull(loadedTimeEntry);

	        loadedTimeEntry.Duration = 1200;
	        var editedTimeEntry = TimeEntryService.Edit(loadedTimeEntry);

			Assert.AreEqual(timeEntry.Id, editedTimeEntry.Id);
			Assert.AreEqual(timeEntry.IsBillable, editedTimeEntry.IsBillable);
			Assert.AreEqual(timeEntry.CreatedWith, editedTimeEntry.CreatedWith);
			Assert.AreEqual(loadedTimeEntry.Duration, editedTimeEntry.Duration);
			Assert.AreEqual(timeEntry.WorkspaceId, editedTimeEntry.WorkspaceId);        
        }
        
        [Test]
        public void Delete()
        {
			var timeEntry = TimeEntryService.Add(new TimeEntry()
			{
				IsBillable = true,
				CreatedWith = "TogglAPI.Net",
				Duration = 900,
				Start = DateTime.Now.ToIsoDateStr(),
				WorkspaceId = DefaultWorkspaceId

			});

			Assert.IsNotNull(timeEntry);
			Assert.AreEqual(1, TimeEntryService.List().Count());
			Assert.IsTrue(TimeEntryService.Delete(timeEntry.Id.Value));
			Assert.AreEqual(0, TimeEntryService.List().Count());
        }
        
    
    }
 }




