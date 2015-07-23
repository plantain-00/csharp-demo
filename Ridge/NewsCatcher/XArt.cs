using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using NewsCatcher.Models;

using Ridge;

namespace NewsCatcher
{
    internal static class XArt
    {
        public const string FAILS_MESSAGE = "Xart Fails";

        public static IEnumerable<ShowItem> Do()
        {
            var result = new List<ShowItem>
                         {
                             new ShowItem
                             {
                                 Text = "x-art:",
                                 Url = "title"
                             }
                         };
            try
            {
                var html = new XWebClient
                           {
                               Encoding = Encoding.UTF8
                           }.DownloadString("http://www.x-art.com/ajax_process.php?action=allvideos&page=1&catname=&order=recent");
                var doc = new Document(html);
                for (var i = 0; i < 2; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        var tmp = doc["ul"]?["li", i]?["ul"]?["li", j]?["a"]?["span"];
                        if (tmp != null)
                        {
                            var text = tmp["span"]?["b"]?[0]?.As<PlainText>()?.Text;
                            result.Add(new ShowItem
                                       {
                                           Text = tmp[1]?[0]?.As<PlainText>()?.Text + " " + text,
                                           Url = string.Format(TV.seedWebsite + "{0}/", HttpUtility.UrlEncode(text).Replace("+", "%20"))
                                       });
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                result.Add(new ShowItem
                           {
                               Text = FAILS_MESSAGE,
                               Summary = exception.Message,
                               Url = FAILS_MESSAGE
                           });
            }
            result.Add(new ShowItem());
            return result;
        }

        public static Task<IEnumerable<ShowItem>> DoAsync()
        {
            return Task.Factory.StartNew(() => Do());
        }
    }
}