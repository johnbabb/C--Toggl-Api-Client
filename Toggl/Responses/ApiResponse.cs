using System;
using System.Collections.Generic;
using System.Net;
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
            var cverts = new List<JsonConverter>();
            var iso = new IsoDateTimeConverter();
            cverts.Add(iso);

            T obj;
            if (Method == "DELETE" && StatusCode.Equals(HttpStatusCode.OK))
            {
                obj = (T)Activator.CreateInstance(typeof(T));
            }
            else if (Data != null)
            {
                if (Data is JToken)
                {
                    var token = JToken.FromObject(Data);
                    obj = token.ToObject<T>();
                }
                else
                {
                    var json = JsonConvert.SerializeObject(Data, cverts.ToArray());
                    obj = JsonConvert.DeserializeObject<T>(json, cverts.ToArray());
                }
            }
            else
            {
                obj = (T)Activator.CreateInstance(typeof(T));
            }
            return obj;
        }
    }
}
