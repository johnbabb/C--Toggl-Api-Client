using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Toggl.Extensions;
using Toggl.Interfaces;

namespace Toggl.Services
{
    public class UserService : IUserService
    {
        
        private IApiService ToggleSrv { get; set; }


        public UserService(string apiKey)
            : this(new ApiService(apiKey))
        {

        }

        public UserService(IApiService srv)
        {
            ToggleSrv = srv;
        }

        
        public User GetCurrent()
        {
            var url = ApiRoutes.User.CurrentUrl;

            var obj = ToggleSrv.Get(url).GetData<User>();

            return obj;
        }

        public UserExtended GetCurrentExtended()
        {
            var url = ApiRoutes.User.CurrentExtendedUrl;

            var obj = ToggleSrv.Get(url).GetData<UserExtended>();

            return obj;
        }

        public UserExtended GetCurrentChanged(DateTime since)
        {
            var url = string.Format(ApiRoutes.User.CurrentSinceUrl, since.ToUnixTime());

            var obj = ToggleSrv.Get(url).GetData<UserExtended>();

            return obj;
        }

        public User Edit(User u)
        {
            var url = string.Format(ApiRoutes.User.EditUrl);
            var data = u.ToJson();
            
            u = ToggleSrv.Put(url, data).GetData<User>();
            
            return u;
        }

        public string ResetApiToken()
        {
            var url = ApiRoutes.User.ResetApiTokenUrl;

            var apiToken = ToggleSrv.Post(url, null).GetData<string>();

            return apiToken;
        }

        public List<User> GetForWorkspace(int id)
        {
            var url = string.Format(ApiRoutes.Workspace.ListWorkspaceUsersUrl, id);
            return ToggleSrv.Get(url).GetData<List<User>>();
        }

        public User Add(User u)
        {
            var url = string.Format(ApiRoutes.User.AddUrl);
            var data = u.ToJson();

            u = ToggleSrv.Post(url, data).GetData<User>();

            return u;
        }
    }
}
