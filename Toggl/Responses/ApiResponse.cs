using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public HttpStatusCode StatusCode { get; set; }

        public string Method { get; set; }

        public T GetData<T>()
        {
            
            var obj = (T)Activator.CreateInstance(typeof(T));
            var cverts = new List<JsonConverter>();
            var iso = new IsoDateTimeConverter();
            cverts.Add(iso);

            if (Method == "DELETE" && StatusCode.Equals(HttpStatusCode.OK))
            {
                return obj;
            }
            
            if (Data != null)
            {

                if (Data is JArray)
                {
                    var jray = ((JArray) Data).ToString();
                    return JsonConvert.DeserializeObject<T>(jray, cverts.ToArray());
                }
                return JsonConvert.DeserializeObject<T>(((JObject)Data).ToString(), cverts.ToArray());
            }
            return obj;
        }
    }
}
