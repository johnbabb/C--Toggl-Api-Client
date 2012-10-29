using System;
using System.Collections.Generic;
using System.Linq;


namespace Toggl.Services
{
    public class TimeEntryService
    {

        private ITogglService ToggleSrv { get; set; }

        public TimeEntryService(string apiKey)
            : this(new TogglService(apiKey))
        {

        }
        
        public TimeEntryService():this(new TogglService())
        {

        }
        
        public TimeEntryService(ITogglService srv)
        {
            ToggleSrv = srv;
        }

        public List<TimeEntry> GetTimeEntries()
        {
            return GetTimeEntries(new QueryObjects.TimeEntryParams());
        }

        public List<TimeEntry> GetTimeEntries(QueryObjects.TimeEntryParams obj)
        {
            var entries = ToggleSrv.GetResponse(ApiRoutes.TimeEntry.TimeEntriesUrl, obj.GetParameters())
                        .GetData<List<TimeEntry>>()
                        .AsQueryable();

            if (obj.ProjectId.HasValue)
                entries = entries.Where(w => w.Project.Id == obj.ProjectId.Value);

            return entries.Select(s => s).ToList();
        }

        public TimeEntry GetTimeEntry(int id)
        {
            var url = string.Format(ApiRoutes.TimeEntry.TimeEntryUrl, id);
            
            var timeEntry = ToggleSrv.GetResponse(url).GetData<TimeEntry>();

            return timeEntry;
        }
    }
}
