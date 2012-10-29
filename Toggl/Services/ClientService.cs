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

        public List<Client> List()
        {

            return ToggleSrv.GetResponse(ListClientsUrl).GetData<List<Client>>();
        }
       
    }
}
