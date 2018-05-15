using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toggl.DataObjects
{
    public class MostActiveUser : BaseDataObject
    {
        [JsonProperty(PropertyName = "user_id")]
        public int UserId { get; set; }

        [JsonProperty(PropertyName = "duration")]
        public int Duration { get; set; }
    }
}
