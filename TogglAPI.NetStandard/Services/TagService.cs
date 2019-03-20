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
    public class TagService : ITagService
    {
        private readonly string TagsUrl = ApiRoutes.Tag.TagsUrl;


        private IApiService ToggleSrv { get; set; }


        public TagService(string apiKey)
            : this(new ApiService(apiKey))
        {

        }

        public TagService(IApiService srv)
        {
            ToggleSrv = srv;
        }

        /// <summary>
        /// 
        /// https://www.toggl.com/public/api#get_tags
        /// </summary>
        /// <returns></returns>
        public List<Tag> List()
        {
            var lstTag = new List<Tag>();
            var lstWrkSpc = ToggleSrv.Get(ApiRoutes.Workspace.ListWorkspaceUrl).GetData<List<Workspace>>();
            lstWrkSpc.ForEach(e =>
            {
                var tags = ForWorkspace(e.Id.Value);
                lstTag.AddRange(tags);
            });
            return lstTag;
        }

        public List<Tag> ForWorkspace(int id)
        {
            var url = string.Format(ApiRoutes.Workspace.ListWorkspaceTagsUrl, id);
            return ToggleSrv.Get(url).GetData<List<Tag>>();
        }

        public Tag Add(Tag tag)
        {
            return ToggleSrv.Post(TagsUrl, tag.ToJson()).GetData<Tag>();
        }
    }
}
