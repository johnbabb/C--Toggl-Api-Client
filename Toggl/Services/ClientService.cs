using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Toggl.Properties;

namespace Toggl.Services
{
    public class ClientService
    {
        private readonly string ListClientsUrl = ApiRoutes.Client.ListClientsUrl;
        

        private ITogglService ToggleSrv { get; set; }


        public ClientService(string apiKey)
            : this(new TogglService(apiKey))
        {

        }

        public ClientService()
            : this(new TogglService())
        {
        }

        public ClientService(ITogglService srv)
        {
            ToggleSrv = srv;
        }

        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#get_clients
        /// </summary>
        /// <returns></returns>
        public List<Client> List()
        {

            return ToggleSrv.Get(ListClientsUrl).GetData<List<Client>>();
        }

        public Client Get(int id)
        {

            return List().Where(w => w.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#post_clients
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
            return (!string.IsNullOrEmpty(res.related_data_updated_at.ToString()));
        }


       
    }
}
