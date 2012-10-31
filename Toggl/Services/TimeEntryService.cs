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
        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#get_time_entries
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public List<TimeEntry> GetTimeEntries(QueryObjects.TimeEntryParams obj)
        {
            var entries = ToggleSrv.GetResponse(ApiRoutes.TimeEntry.TimeEntriesUrl, obj.GetParameters())
                        .GetData<List<TimeEntry>>()
                        .AsQueryable();

            if (obj.ProjectId.HasValue)
                entries = entries.Where(w => w.Project.Id == obj.ProjectId.Value);

            return entries.Select(s => s).ToList();
        }

        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#get_time_entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TimeEntry GetTimeEntry(int id)
        {
            var url = string.Format(ApiRoutes.TimeEntry.TimeEntryUrl, id);
            
            var timeEntry = ToggleSrv.GetResponse(url).GetData<TimeEntry>();

            return timeEntry;
        }
        
        /// <summary>
        /// https://www.toggl.com/public/api#post_time_entries
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public TimeEntry Add(TimeEntry obj)
        {
            var url = string.Format(ApiRoutes.TimeEntry.TimeEntriesUrl);

            var timeEntry = ToggleSrv.PostResponse(url, obj.ToJson()).GetData<TimeEntry>();

            return timeEntry;
        }
    }
}
