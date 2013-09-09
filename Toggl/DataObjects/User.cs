using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Toggl.DataObjects;

namespace Toggl
{
    /// <summary>
    /// https://github.com/toggl/toggl_api_docs/blob/master/chapters/users.md#users
    /// </summary>
    public class User : BaseDataObject
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "api_token")]
        public string ApiToken { get; set; }

        [JsonProperty(PropertyName = "default_wid")]
        public int? DefaultWorkspaceId { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "fullname")]
        public string FullName { get; set; }

        [JsonProperty(PropertyName = "jquery_timeofday_format")]
        public string JQueryTimeofdayFormat { get; set; }

        [JsonProperty(PropertyName = "jquery_date_format")]
        public string JqueryDateFormat { get; set; }

        [JsonProperty(PropertyName = "timeofday_format")]
        public string TimeOfDayFormat { get; set; }

        [JsonProperty(PropertyName = "date_format")]
        public string DateFormat { get; set; }

        [JsonProperty(PropertyName = "store_start_and_stop_time")]
        public bool? IsStartStopTime { get; set; }

        [JsonProperty(PropertyName = "beginning_of_week")]
        public int BeginningOfWeek { get; set; }

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "sidebar_piechart")]
        public bool? IsSidebarPiechart { get; set; }

        [JsonProperty(PropertyName = "at")]
        public DateTime? UpdatedOn { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime? CreatedOn { get; set; }

        [JsonProperty(PropertyName = "retention")]
        public int? Retention { get; set; }

        [JsonProperty(PropertyName = "record_timeline")]
        public bool? IsRecordTimeline { get; set; }

        [JsonProperty(PropertyName = "render_timeline")]
        public bool? IsRenderTimeline { get; set; }

        [JsonProperty(PropertyName = "timeline_enabled")]
        public bool? IsTimelineEnabled { get; set; }

        [JsonProperty(PropertyName = "timeline_experiment")]
        public bool? IsTimelineExperiment { get; set; }

        [JsonProperty(PropertyName = "manual_mode")]
        public bool? IsManualMode { get; set; }

        [JsonProperty(PropertyName = "new_blog_post")]
        public NewBlogPost NewBlogPost { get; set; }

        [JsonProperty(PropertyName = "should_upgrade")]
        public bool? IsShouldUpgrade { get; set; }

        [JsonProperty(PropertyName = "show_offer")]
        public bool? IsShowOffer { get; set; }

        [JsonProperty(PropertyName = "share_experiment")]
        public bool? IsShareExperiment { get; set; }

        [JsonProperty(PropertyName = "achievements_enabled")]
        public bool? IsAchievementsEnabled { get; set; }

        [JsonProperty(PropertyName = "timezone")]
        public string Timezone { get; set; }

        [JsonProperty(PropertyName = "openid_enabled")]
        public bool? IsOpenidEnabled { get; set; }

        [JsonProperty(PropertyName = "send_product_emails")]
        public bool? IsSendProductEmails { get; set; }

        [JsonProperty(PropertyName = "send_weekly_report")]
        public bool? IsSendWeeklyReport { get; set; }

        [JsonProperty(PropertyName = "send_timer_notifications")]
        public bool? IsSendTimerNotifications { get; set; }

    }
}
