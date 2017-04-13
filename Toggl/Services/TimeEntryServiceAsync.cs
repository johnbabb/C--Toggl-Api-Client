using System;
using System.Collections.Generic;
using System.Linq;
using Toggl.Interfaces;

namespace Toggl.Services
{
	using System.Net;

	public class TimeEntryServiceAsync : ITimeEntryServiceAsync
    {
        private IApiServiceAsync ToggleSrv { get; set; }

        public TimeEntryServiceAsync(string apiKey)
            : this(new ApiServiceAsync(apiKey))
        {

        }
        
        public TimeEntryServiceAsync(IApiServiceAsync srv)
        {
            ToggleSrv = srv;
        }

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#get-time-entries-started-in-a-specific-time-range
        /// </summary>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<List<TimeEntry>> ListRecent()
        {
            throw new NotImplementedException();
        }

        public async System.Threading.Tasks.Task<List<TimeEntry>> List()
        {
            return await List(new QueryObjects.TimeEntryParams());
        }

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#get-time-entries-started-in-a-specific-time-range
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<List<TimeEntry>> List(QueryObjects.TimeEntryParams obj)
        {
            var response = await ToggleSrv.Get(ApiRoutes.TimeEntry.TimeEntriesUrl, obj.GetParameters());
            var entries = response
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
	    public async System.Threading.Tasks.Task<TimeEntry> Current()
        {
            var url = ApiRoutes.TimeEntry.TimeEntryCurrentUrl;

            var response = await ToggleSrv.Get(url);
            var timeEntry = response.GetData<TimeEntry>();

            return timeEntry;
        }

	    /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#get-time-entry-details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<TimeEntry> Get(long id)
        {
            var url = string.Format(ApiRoutes.TimeEntry.TimeEntryUrl, id);

            var response = await ToggleSrv.Get(url);
            var timeEntry = response.GetData<TimeEntry>();

            return timeEntry;
        }
        
        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#create-a-time-entry
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<TimeEntry> Add(TimeEntry obj)
        {
            var url = ApiRoutes.TimeEntry.TimeEntriesUrl;

            var response = await ToggleSrv.Post(url, obj.ToJson());
            var timeEntry = response.GetData<TimeEntry>();

            return timeEntry;
        }

		/// <summary>
		/// Start a TimeEntry
		/// </summary>
		/// <param name="obj">A TimeEntry object.</param>
		/// <returns>The runnig TimeEntry.</returns>
        public async System.Threading.Tasks.Task<TimeEntry> Start(TimeEntry obj)
        {
            var url = ApiRoutes.TimeEntry.TimeEntryStartUrl;

            var response = await ToggleSrv.Post(url, obj.ToJson());
            var timeEntry = response.GetData<TimeEntry>();

            return timeEntry;
        }

		/// <summary>
		/// Stop a TimeEntry
		/// </summary>
		/// <param name="obj">A TimeEntry object.</param>
		/// <returns>The stopped TimeEntry.</returns>
        public async System.Threading.Tasks.Task<TimeEntry> Stop(TimeEntry obj)
        {
            var url = string.Format(ApiRoutes.TimeEntry.TimeEntryStopUrl, obj.Id);

            var response = await ToggleSrv.Put(url, obj.ToJson());
            var timeEntry = response.GetData<TimeEntry>();

            return timeEntry;
        }

        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#update-a-time-entry
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<TimeEntry> Edit(TimeEntry obj)
        {
            var url = string.Format(ApiRoutes.TimeEntry.TimeEntryUrl, obj.Id);

            var response = await ToggleSrv.Put(url, obj.ToJson());
            var timeEntry = response.GetData<TimeEntry>();

            return timeEntry;
        }

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#delete-a-time-entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<bool> Delete(long id)
        {
			var url = string.Format(ApiRoutes.TimeEntry.TimeEntryUrl, id);

            var rsp = await ToggleSrv.Delete(url);

            return rsp.StatusCode == HttpStatusCode.OK;
        }

		public async System.Threading.Tasks.Task<bool> DeleteIfAny(long[] ids)
		{
			if (!ids.Any() || ids == null)
				return true;
			return await Delete(ids);
		}

		public async System.Threading.Tasks.Task<bool> Delete(long[] ids)
		{
			if (!ids.Any() || ids == null)
				throw new ArgumentNullException("ids");

			var result = new Dictionary<long, bool>(ids.Length);
			foreach (var id in ids)
			{
				result.Add(id, await Delete(id));
			}

			return !result.ContainsValue(false);
		}       
    }
}
