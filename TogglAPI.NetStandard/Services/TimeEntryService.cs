using System;
using System.Collections.Generic;
using System.Linq;
using Toggl.Interfaces;


namespace Toggl.Services
{
	using System.Net;

	public class TimeEntryService : ITimeEntryService
    {
        private IApiService ToggleSrv { get; set; }

        public TimeEntryService(string apiKey)
            : this(new ApiService(apiKey))
        {

        }
        
        public TimeEntryService(IApiService srv)
        {
            ToggleSrv = srv;
        }

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#get-time-entries-started-in-a-specific-time-range
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
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#get-time-entries-started-in-a-specific-time-range
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public List<TimeEntry> List(QueryObjects.TimeEntryParams obj)
        {
            var entries = ToggleSrv.Get(ApiRoutes.TimeEntry.TimeEntriesUrl, obj.GetParameters())
                        .GetData<List<TimeEntry>>()
                        .AsQueryable();
            
            if (obj.ProjectId.HasValue)
                entries = entries.Where(w => w.ProjectId == obj.ProjectId);

            return entries.Select(s => s).ToList();
        }

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#get-time-entry-details
        /// </summary>
        /// <returns></returns>
	    public TimeEntry Current()
        {
            var url = ApiRoutes.TimeEntry.TimeEntryCurrentUrl;

            var timeEntry = ToggleSrv.Get(url).GetData<TimeEntry>();

            return timeEntry;
        }

	    /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#get-time-entry-details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TimeEntry Get(long id)
        {
            var url = string.Format(ApiRoutes.TimeEntry.TimeEntryUrl, id);
            
            var timeEntry = ToggleSrv.Get(url).GetData<TimeEntry>();

            return timeEntry;
        }
        
        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#create-a-time-entry
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
		/// Start a TimeEntry
		/// </summary>
		/// <param name="obj">A TimeEntry object.</param>
		/// <returns>The runnig TimeEntry.</returns>
		public TimeEntry Start(TimeEntry obj)
		{
            var url = ApiRoutes.TimeEntry.TimeEntryStartUrl;

            var timeEntry = ToggleSrv.Post(url, obj.ToJson()).GetData<TimeEntry>();

            return timeEntry;
		}

		/// <summary>
		/// Stop a TimeEntry
		/// </summary>
		/// <param name="obj">A TimeEntry object.</param>
		/// <returns>The stopped TimeEntry.</returns>
		public TimeEntry Stop(TimeEntry obj)
		{
            var url = string.Format(ApiRoutes.TimeEntry.TimeEntryStopUrl, obj.Id);

            var timeEntry = ToggleSrv.Put(url, obj.ToJson()).GetData<TimeEntry>();

            return timeEntry;
		}

        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#update-a-time-entry
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
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#delete-a-time-entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
			var url = string.Format(ApiRoutes.TimeEntry.TimeEntryUrl, id);

            var rsp = ToggleSrv.Delete(url);

            return rsp.StatusCode == HttpStatusCode.OK;
        }

		public bool DeleteIfAny(long[] ids)
		{
			if (!ids.Any() || ids == null)
				return true;
			return Delete(ids);
		}

		public bool Delete(long[] ids)
		{
			if (!ids.Any() || ids == null)
				throw new ArgumentNullException("ids");

			var result = new Dictionary<long, bool>(ids.Length);
			foreach (var id in ids)
			{
				result.Add(id, Delete(id));
			}

			return !result.ContainsValue(false);
		}       
    }
}
