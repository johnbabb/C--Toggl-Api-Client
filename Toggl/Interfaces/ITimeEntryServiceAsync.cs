using System.Collections.Generic;
using System.Threading.Tasks;

namespace Toggl.Interfaces
{
    public interface ITimeEntryServiceAsync
    {
        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#get-time-entries-started-in-a-specific-time-range
        /// </summary>
        /// <returns></returns>
        Task<List<TimeEntry>> ListRecent();

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#get-time-entries-started-in-a-specific-time-range
        /// </summary>
        /// <returns></returns>
        Task<List<TimeEntry>> List();

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#get-time-entries-started-in-a-specific-time-range
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<List<TimeEntry>> List(QueryObjects.TimeEntryParams obj);
        
        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#get-running-time-entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TimeEntry> Current();

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#get-time-entry-details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TimeEntry> Get(long id);

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#create-a-time-entry
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<TimeEntry> Add(TimeEntry obj);

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#update-a-time-entry
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<TimeEntry> Edit(TimeEntry obj);

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#delete-a-time-entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(long id);
    }
}