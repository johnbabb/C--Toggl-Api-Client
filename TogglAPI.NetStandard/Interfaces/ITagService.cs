using System.Collections.Generic;

namespace Toggl.Interfaces
{
    public interface ITagService
    {
        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#get_tags
        /// </summary>
        /// <returns></returns>
        List<Client> List();
    }
}