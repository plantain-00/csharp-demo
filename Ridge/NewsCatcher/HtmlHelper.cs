using System.Collections.Generic;
using System.Linq;

namespace NewsCatcher
{
    public static class HtmlHelper
    {
        private static readonly Dictionary<string, string> data = new Dictionary<string, string>();

        static HtmlHelper()
        {
            data.Add("&nbsp;", " ");
            data.Add("&lt;", "<");
            data.Add("&gt;", ">");
            data.Add("&amp;", "&");
            data.Add("&apos;", "'");
            data.Add("&quot;", "\"");
            data.Add("&ldquo;", "“");
            data.Add("&rdquo;", "”");
            data.Add("&mdash;", "-");
            data.Add("&#8226;", "•");
        }

        public static string Escape(this string source)
        {
            return data.Aggregate(source, (current, a) => current.Replace(a.Value, a.Key));
        }

        public static string Unescape(this string source)
        {
            return data.Aggregate(source, (current, a) => current.Replace(a.Key, a.Value));
        }
    }
}