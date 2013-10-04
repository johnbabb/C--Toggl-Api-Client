using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Toggl.Extensions;

namespace Toggl.DataObjects
{
    public class BaseDataObject
    {
        public List<KeyValuePair<string,string>> ToKeyValuePair()
        {
            var lst = new List<KeyValuePair<string, string>>();

            
            this.GetType().GetProperties().ToList()
                .ForEach(p =>
                             {
                                 var pair = new KeyValuePair<string, string>(p.Name, p.GetValue(p, null).ToString());
                                 lst.Add(pair);
                             });

            return lst;
        }
        public string ToJson(string objName="")
        {
            var cverts = new List<JsonConverter>();
            cverts.Add(new IsoDateTimeConverter());
            var data = JsonConvert.SerializeObject(this,
                                                Formatting.None,
                                                new JsonSerializerSettings
                                                {
                                                    NullValueHandling = NullValueHandling.Ignore,
                                                    Converters = cverts
                                                }

                                );
            var propNm = (string.IsNullOrEmpty(objName)) ? this.GetType().Name.LowerCaseUnderscore() : objName;

            return "{\"" + propNm + "\":" + data + "}";
        }
        
    }
}
