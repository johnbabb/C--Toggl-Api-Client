using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Toggl
{
    public class ApiResponse
    {
        [JsonProperty(PropertyName = "data")]
        public dynamic Data { get; set; }
        
        [JsonProperty(PropertyName = "related_data_updated_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime related_data_updated_at{ get; set; }

        public T GetData<T>()
        {
            var obj = (T)Activator.CreateInstance(typeof(T));
            if (Data != null)
            {

                if (Data is JArray)
                {
                    return JsonConvert.DeserializeObject<T>(((JArray)Data).ToString());
                }
                return JsonConvert.DeserializeObject<T>(((JObject)Data).ToString());
            }
            return obj;
        }
    }
}
