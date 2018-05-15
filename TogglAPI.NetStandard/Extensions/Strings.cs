using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Toggl.Extensions
{
    

    public static class Strings
    {
        public static string LowerCaseUnderscore(this string pascalCasedWord)
        {
            return Regex.Replace(Regex.Replace(Regex.Replace(pascalCasedWord, "([A-Z]+)([A-Z][a-z])", "$1_$2"), @"([a-z\d])([A-Z])", "$1_$2"), @"[-\s]", "_").ToLower();
        }

    }
}

