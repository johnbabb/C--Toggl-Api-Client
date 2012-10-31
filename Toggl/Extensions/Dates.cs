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
    }
}

