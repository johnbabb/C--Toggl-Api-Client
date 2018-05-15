using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toggl.DataObjects
{
    public class Dashboard : BaseDataObject
    {
        [JsonProperty(PropertyName = "most_active_user")]
        public MostActiveUser[] MostActiveUser { get; set; }

        [JsonProperty(PropertyName = "activity")]
        public Activity[] Activity { get; set; }
    }
}