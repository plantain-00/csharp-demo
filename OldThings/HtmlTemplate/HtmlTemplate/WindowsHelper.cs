using System.Linq;

using Microsoft.Win32;

namespace HtmlTemplate
{
    public static class WindowsHelper
    {
        private static string defaultBrowser;
        public static string GetDefaultBrowser()
        {
            if (defaultBrowser == null)
            {
                var key = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");
                var s = key.GetValue("").ToString();
                defaultBrowser = new string(s.SkipWhile(c => c != '"').Skip(1).TakeWhile(c => c != '"').ToArray()).Trim().Trim('"');
            }
            return defaultBrowser;
        }
    }
}