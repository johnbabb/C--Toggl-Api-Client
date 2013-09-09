using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Toggl.DataObjects;

namespace Toggl
{
    /// <summary>
    /// "id": 387715,
    /// "api_token": "2d1d95cef10e17831ec505e9e6f9f7ea",
    /// "default_wid": 303523,
    /// "email": "toggle@infocss.com",
    /// "fullname": "Toggle",
    /// "jquery_timeofday_format": "h:i A",
    /// "jquery_date_format": "m/d/Y",
    /// "timeofday_format": "h:mm A",
    /// "date_format": "MM/DD/YYYY",
    /// "store_start_and_stop_time": true,
    /// "beginning_of_week": 1,
    /// "language": "en_US",
    /// "image_url": "https:/// www.toggl.com/images/profile.png",
    /// "sidebar_piechart": false,
    /// "at": "2012-11-14T04:26:22+00:00",
    /// "created_at": "2012-10-28T19:06:04+00:00",
    /// "retention": 9,
    /// "record_timeline": false,
    /// "render_timeline": false,
    /// "timeline_enabled": false,
    /// "timeline_experiment": false,
    /// "manual_mode": false,
    /// "new_blog_post": {
    ///   "title": "6 Tools To Monitor Your Social Media Performance",
    ///   "url": "http:/// blog.toggl.com/2013/09/tools-to-monitor-your-social-media-performance/?utm_source=rss&utm_medium=rss&utm_campaign=tools-to-monitor-your-social-media-performance",
    ///   "category": "Uncategorized",
    ///   "pub_date": "2013-09-02T05:51:23Z"
    /// },
    /// "should_upgrade": false,
    /// "show_offer": false,
    /// "share_experiment": false,
    /// "achievements_enabled": false,
    /// "timezone": "America/New_York",
    /// "openid_enabled": false,
    /// "send_product_emails": true,
    /// "send_weekly_report": false,
    /// "send_timer_notifications": true,
    /// "invitation": {}
    /// </summary>
    public class Session : BaseDataObject
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
        public NewBlogPost NewBlogPost  { get; set; }

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
