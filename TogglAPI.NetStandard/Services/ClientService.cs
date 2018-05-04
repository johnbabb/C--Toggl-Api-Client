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

	public class ClientService : IClientService
    {
        private static Dictionary<int, Client> cachedClients;

		private void EnsureCacheLoaded()
		{
			if (cachedClients == null)
				List();
		}

		public IApiService ToggleSrv { get; set; }
		
        public ClientService(string apiKey)
            : this(new ApiService(apiKey))
        {

        }

		public ClientService(IApiService srv)
        {
            ToggleSrv = srv;
        }

        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/clients.md#get-clients-visible-to-user
        /// </summary>
        /// <returns></returns>
        public List<Client> List(bool includeDeleted = false)
        {
	        var result = ToggleSrv.Get(ApiRoutes.Client.ClientsUrl).GetData<List<Client>>();

	        cachedClients = result.ToDictionary(client => client.Id.Value, client => client);

			return includeDeleted 
				? result
				: result.Where(client => client.DeletedAt == null).ToList();
        }

        public Client Get(int id)
        {
	        if (cachedClients != null && cachedClients.ContainsKey(id))
		        return cachedClients[id];

			var url = string.Format(ApiRoutes.Client.ClientUrl, id);
            return ToggleSrv.Get(url).GetData<Client>();
            
        }

	    public Client GetByName(string name)
	    {
		    EnsureCacheLoaded();

		    return cachedClients
				.Values
				.Single(client => client.Name == name && client.DeletedAt == null);
	    }

        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/clients.md#create-a-client
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Client Add(Client obj)
        {
	        cachedClients = null;
            var url = ApiRoutes.Client.ClientsUrl;
            return ToggleSrv.Post(url, obj.ToJson()).GetData<Client>();

        }
        
        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#put_clients
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Client Edit(Client obj)
        {
	        cachedClients = null;
            var url = string.Format(ApiRoutes.Client.ClientUrl, obj.Id);
            return ToggleSrv.Put(url, obj.ToJson()).GetData<Client>();

        }

        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#del_clients
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
	        cachedClients = null;
            var url = string.Format(ApiRoutes.Client.ClientUrl, id);
            var res = ToggleSrv.Delete(url);
            return (res.StatusCode==HttpStatusCode.OK);
        }

		public bool DeleteIfAny(int[] ids)
		{
			if (!ids.Any() || ids == null)
				return true;

			return Delete(ids);
		}

	    public bool Delete(int[] ids)
	    {
			if (!ids.Any() || ids == null)
				throw new ArgumentNullException("ids");

		    cachedClients = null;

		    var result = new Dictionary<int, bool>(ids.Length);
		    foreach (var id in ids)
		    {
			    result.Add(id, Delete(id));
		    }

		    return !result.ContainsValue(false);
	    }       
    }
}
