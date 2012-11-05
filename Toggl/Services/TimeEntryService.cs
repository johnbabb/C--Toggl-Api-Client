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

        /// <summary>
        /// https://www.toggl.com/public/api#get_time_entries
        /// </summary>
        /// <returns></returns>
        public List<TimeEntry> ListRecent()
        {
            throw new NotImplementedException();
        }

        public List<TimeEntry> List()
        {
            return List(new QueryObjects.TimeEntryParams());
        }
        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#get_time_entries_by_range
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public List<TimeEntry> List(QueryObjects.TimeEntryParams obj)
        {
            var entries = ToggleSrv.Get(ApiRoutes.TimeEntry.TimeEntriesUrl, obj.GetParameters())
                        .GetData<List<TimeEntry>>()
                        .AsQueryable();

            if (obj.Project!=null && obj.Project.Id.HasValue)
                entries = entries.Where(w => w.Project.Id == obj.Project.Id);

            return entries.Select(s => s).ToList();
        }

        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#get_time_entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TimeEntry Get(int id)
        {
            var url = string.Format(ApiRoutes.TimeEntry.TimeEntryUrl, id);
            
            var timeEntry = ToggleSrv.Get(url).GetData<TimeEntry>();

            return timeEntry;
        }
        
        /// <summary>
        /// https://www.toggl.com/public/api#post_time_entries
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public TimeEntry Add(TimeEntry obj)
        {
            var url = ApiRoutes.TimeEntry.TimeEntriesUrl;

            var timeEntry = ToggleSrv.Post(url, obj.ToJson()).GetData<TimeEntry>();

            return timeEntry;
        }

        /// <summary>
        /// https://www.toggl.com/public/api#put_time_entries
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public TimeEntry Edit(TimeEntry obj)
        {
            var url = string.Format(ApiRoutes.TimeEntry.TimeEntryUrl, obj.Id);

            var timeEntry = ToggleSrv.Put(url, obj.ToJson()).GetData<TimeEntry>();

            return timeEntry;
        }

        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#del_time_entries
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TimeEntry Delete(int id)
        {

            var url = string.Format(ApiRoutes.TimeEntry.TimeEntryUrl, id);

            var timeEntry = ToggleSrv.Delete(url).GetData<TimeEntry>();

            return timeEntry;
        }
    }
}
