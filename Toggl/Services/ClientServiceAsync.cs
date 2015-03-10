using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Toggl.Interfaces;

namespace Toggl.Services
{
	using System.Resources;

	public class ClientServiceAsync : IClientServiceAsync
    {
        private static Dictionary<int, Client> cachedClients;

		private async System.Threading.Tasks.Task EnsureCacheLoaded()
		{
			if (cachedClients == null)
				await List();
		}

		public IApiServiceAsync ToggleSrv { get; set; }
		
        public ClientServiceAsync(string apiKey)
            : this(new ApiServiceAsync(apiKey))
        {

        }

		public ClientServiceAsync(IApiServiceAsync srv)
        {
            ToggleSrv = srv;
        }

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/clients.md#get-clients-visible-to-user
        /// </summary>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<List<Client>> List(bool includeDeleted = false)
        {
            var response = await ToggleSrv.Get(ApiRoutes.Client.ClientsUrl);
	        var result = response.GetData<List<Client>>();

	        cachedClients = result.ToDictionary(client => client.Id.Value, client => client);

			return includeDeleted 
				? result
				: result.Where(client => client.DeletedAt == null).ToList();
        }

        public async System.Threading.Tasks.Task<Client> Get(int id)
        {
	        if (cachedClients != null && cachedClients.ContainsKey(id))
		        return cachedClients[id];

			var url = string.Format(ApiRoutes.Client.ClientUrl, id);
            var response = await ToggleSrv.Get(url);
            var data = response.GetData<Client>();
            return data;
            
        }

        public async System.Threading.Tasks.Task<Client> GetByName(string name)
	    {
		    await EnsureCacheLoaded();

		    return cachedClients
				.Values
				.Single(client => client.Name == name && client.DeletedAt == null);
	    }

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/clients.md#create-a-client
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<Client> Add(Client obj)
        {
	        cachedClients = null;
            var url = ApiRoutes.Client.ClientsUrl;
            var response = await ToggleSrv.Post(url, obj.ToJson());
            var data = response.GetData<Client>();
            return data;

        }
        
        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/clients.md#update-a-client
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<Client> Edit(Client obj)
        {
	        cachedClients = null;
            var url = string.Format(ApiRoutes.Client.ClientUrl, obj.Id);
            var response = await ToggleSrv.Put(url, obj.ToJson());
            var data = response.GetData<Client>();
            return data;
        }

        /// <summary>
        /// 
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/clients.md#delete-a-client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<bool> Delete(int id)
        {
	        cachedClients = null;
            var url = string.Format(ApiRoutes.Client.ClientUrl, id);
            var res = await ToggleSrv.Delete(url);
            return (res.StatusCode == HttpStatusCode.OK);
        }

		public async System.Threading.Tasks.Task<bool> DeleteIfAny(int[] ids)
		{
			if (!ids.Any() || ids == null)
				return true;

			return await Delete(ids);
		}

	    public async System.Threading.Tasks.Task<bool> Delete(int[] ids)
	    {
			if (!ids.Any() || ids == null)
				throw new ArgumentNullException("ids");

		    cachedClients = null;

		    var result = new Dictionary<int, bool>(ids.Length);
		    foreach (var id in ids)
		    {
			    result.Add(id, await Delete(id));
		    }

		    return !result.ContainsValue(false);
	    }       
    }
}
