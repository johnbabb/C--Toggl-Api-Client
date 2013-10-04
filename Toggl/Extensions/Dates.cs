using System.Diagnostics.CodeAnalysis;

namespace Toggl.Extensions
{
    using System;
    using System.Runtime.CompilerServices;

    public static class Dates
    {       
        public static string ToIsoDateStr(this DateTime date)
        {
            return date.ToString("yyyy-MM-ddTHH:mm:sszzz");
        }

        public static long ToUnixTime(this DateTime date)
        {
            return ((date.ToUniversalTime().Ticks - 621355968000000000)/10000000);
        }
    }
}

