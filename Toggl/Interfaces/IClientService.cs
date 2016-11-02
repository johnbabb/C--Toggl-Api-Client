using System.Collections.Generic;

namespace Toggl.Interfaces
{
    public interface IClientService
    {
        IApiService ToggleSrv { get; set; }

        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/clients.md#get-clients-visible-to-user
        /// </summary>
        /// <returns></returns>
        List<Client> List(bool includeDeleted = false);

        Client Get(int id);

        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/clients.md#create-a-client
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Client Add(Client obj);

        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#put_clients
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Client Edit(Client obj);

        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#del_clients
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
    }
}