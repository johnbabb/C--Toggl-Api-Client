using System.Collections.Generic;
using System.Threading.Tasks;

namespace Toggl.Interfaces
{
    public interface IClientServiceAsync
    {
        IApiServiceAsync ToggleSrv { get; set; }

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/clients.md#get-clients-visible-to-user
        /// </summary>
        /// <returns></returns>
        Task<List<Client>> List(bool includeDeleted = false);

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/clients.md#get-client-projects
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Client> Get(int id);

        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/clients.md#create-a-client
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<Client> Add(Client obj);

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/clients.md#update-a-client
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<Client> Edit(Client obj);

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/clients.md#delete-a-client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(int id);
    }
}