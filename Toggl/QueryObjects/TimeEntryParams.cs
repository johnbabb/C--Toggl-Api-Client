using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Toggl.QueryObjects
{
    public class TimeEntryParams
    {
        
        public DateTime start_date { get;  set; }
        public DateTime end_date { get; set; }
        public int? ProjectId { get; set; }

        public List<KeyValuePair<string,string>>GetParameters()
        {
            var lst = new List<KeyValuePair<string, string>>();
            if(start_date > DateTime.MinValue)
            lst.Add(new KeyValuePair<string, string>("start_date", GetStartParameter()));
            if (end_date > DateTime.MinValue)
            lst.Add(new KeyValuePair<string, string>("end_date", GetEndIsoDate()));
            return lst;
        }
        private string GetStartParameter()
        {
            return GetIsoDate(start_date);
        }
        private string GetEndIsoDate()
        {
            return GetIsoDate(end_date);
        }
        private string GetIsoDate(DateTime dt)
        {
            return dt.ToString("yyyy-MM-ddTHH:mm:sszzz");
        }
    }
}
