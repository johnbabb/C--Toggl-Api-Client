using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Toggl.Interfaces;
using Toggl.Properties;

namespace Toggl.Services
{
    public class ClientService : IClientService
    {
        private readonly string _listClientsUrl = ApiRoutes.Client.ClientsUrl;


        public IApiService ToggleSrv { get; set; }


        public ClientService(string apiKey)
            : this(new ApiService(apiKey))
        {

        }

        public ClientService()
            : this(new ApiService())
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
        public List<Client> List()
        {

            return ToggleSrv.Get(ApiRoutes.Client.ClientsUrl).GetData<List<Client>>();
        }

        public Client Get(int id)
        {
            var url = string.Format(ApiRoutes.Client.ClientUrl, id);
            return ToggleSrv.Get(url).GetData<Client>();
            
        }

        /// <summary>
        /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/clients.md#create-a-client
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Client Add(Client obj)
        {
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
            var url = string.Format(ApiRoutes.Client.ClientUrl, id);
            var res = ToggleSrv.Delete(url);
            return (res.StatusCode==HttpStatusCode.OK);
        }


       
    }
}
