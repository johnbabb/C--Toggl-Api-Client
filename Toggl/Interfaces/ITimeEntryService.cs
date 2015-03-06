using System.Collections.Generic;

namespace Toggl.Interfaces
{
    public interface ITimeEntryService
    {
        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#get-time-entries-started-in-a-specific-time-range
        /// </summary>
        /// <returns></returns>
        List<TimeEntry> ListRecent();

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#get-time-entries-started-in-a-specific-time-range
        /// </summary>
        /// <returns></returns>
        List<TimeEntry> List();

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#get-time-entries-started-in-a-specific-time-range
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        List<TimeEntry> List(QueryObjects.TimeEntryParams obj);
        
        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#get-running-time-entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TimeEntry Current();

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#get-time-entry-details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TimeEntry Get(long id);

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#create-a-time-entry
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        TimeEntry Add(TimeEntry obj);

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#update-a-time-entry
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        TimeEntry Edit(TimeEntry obj);

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/time_entries.md#delete-a-time-entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(long id);
    }
}