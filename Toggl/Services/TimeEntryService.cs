using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggl.Services
{
    public class TimeEntryService
    {
        private const string TogglBaseUrl = "https://www.toggl.com/api/v6";
        private const string TimeEntriesUrl = TogglBaseUrl + "/time_entries.json";
        private const string TimeEntryUrl = TogglBaseUrl + "/time_entries/{0}.json";

        private ITogglService ToggleSrv { get; set; }
        
        public TimeEntryService():this(new TogglService())
        {

        }
        public TimeEntryService(string apiKey)
            : this(new TogglService(apiKey))
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
            var entries = ToggleSrv.GetResponse(TimeEntriesUrl, obj.GetParameters())
                        .GetData<List<TimeEntry>>()
                        .AsQueryable();

            if (obj.ProjectId.HasValue)
                entries = entries.Where(w => w.project.id == obj.ProjectId.Value);

            return entries.Select(s => s).ToList();
        }

        public TimeEntry GetTimeEntry(int id)
        {
            var url = string.Format(TimeEntryUrl, id);
            
            var timeEntry = ToggleSrv.GetResponse(url).GetData<TimeEntry>();

            return timeEntry;
        }
    }
}
