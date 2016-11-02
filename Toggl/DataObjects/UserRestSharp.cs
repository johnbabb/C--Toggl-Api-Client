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
    public class UserRestSharp
    {
        public int Id { get; set; }
		public string ApiToken { get; set; }
		public int default_wid { get; set; }
		public string Email { get; set; }
		public string FullName { get; set; }
		public string jquery_timeofday_format { get; set; }
		public string jquery_date_format { get; set; }
		public string timeofday_format { get; set; }
		public string date_format { get; set; }
		public bool store_start_and_stop_time { get; set; }
		public int beginning_of_week { get; set; }
		public string language { get; set; }
		public string image_url { get; set; }
		public bool sidebar_piechart { get; set; }
		public DateTime at { get; set; }
		public DateTime created_at { get; set; }
		public int retention { get; set; }
		public bool record_timeline { get; set; }
		public bool render_timeline { get; set; }
		public bool timeline_enabled { get; set; }
		public bool timeline_experiment { get; set; }
		public bool manual_mode { get; set; }
		public NewBlogPost new_blog_post { get; set; } // TODO:
		public bool should_upgrade { get; set; }
		public bool show_offer { get; set; }
		public bool share_experiment { get; set; }
		public bool achievements_enabled { get; set; }
		public string timezone { get; set; }
		public bool openid_enabled { get; set; }
		public bool send_product_emails { get; set; }
		public bool send_weekly_report { get; set; }
		public bool send_timer_notifications { get; set; }
    }
}
