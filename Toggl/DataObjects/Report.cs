using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Toggl.DataObjects
{
    public class Report : BaseDataObject
    {
        [JsonProperty(PropertyName = "total_grand")]
        public int? TotalGrand { get; set; }

        [JsonProperty(PropertyName = "total_billable")]
        public int? TotalBillable { get; set; }       
    }
}
