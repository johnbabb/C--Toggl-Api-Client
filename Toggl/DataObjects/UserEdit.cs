using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Toggl.DataObjects;

namespace Toggl
{
    /// <summary>
    /// 
    /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/users.md#update-user-data
    /// </summary>
    public class UserEdit : BaseDataObject
    {
        [JsonProperty(PropertyName = "fullname")]
        public string FullName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "send_product_emails")]
        public bool? IsSendProductEmails { get; set; }

        [JsonProperty(PropertyName = "send_weekly_report")]
        public bool? IsSendWeeklyReport { get; set; }

        [JsonProperty(PropertyName = "send_timer_notifications")]
        public bool? IsSendTimerNotifications { get; set; }

        [JsonProperty(PropertyName = "store_start_and_stop_time")]
        public bool? IsStartStopTime { get; set; }

        [JsonProperty(PropertyName = "beginning_of_week")]
        public int BeginningOfWeek { get; set; }

        [JsonProperty(PropertyName = "timezone")]
        public string Timezone { get; set; }
        
        [JsonProperty(PropertyName = "timeofday_format")]
        public string TimeOfDayFormat { get; set; }

        [JsonProperty(PropertyName = "date_format")]
        public string DateFormat { get; set; }

        [JsonProperty(PropertyName = "current_password")]
        public string CurrentPassword { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

    }
}
