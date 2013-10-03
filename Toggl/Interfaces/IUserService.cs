using System.Collections.Generic;

namespace Toggl.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/users.md#get-current-user-data
        /// </summary>
        /// <returns></returns>
        User GetCurrent();

        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#get_me_data
        /// </summary>
        /// <returns></returns>
        User GetCurrentExtended();

    }
}