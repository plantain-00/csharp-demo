using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using HtmlAgilityPack;

using NewsCatcher.Models;

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
                var doc = new HtmlDocument();
                var html = new XWebClient
                           {
                               Encoding = Encoding.UTF8
                           }.DownloadString("http://www.x-art.com/");
                doc.LoadHtml(html);
                for (var i = 0; i < 2; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        var tmp = String.Format("//*[@id='content']/div[2]/ul/li[{0}]/ul/li[{1}]/a/span", i + 1, j + 1);
                        var text = doc.DocumentNode.SelectSingleNode(tmp + "/span[1]/b").InnerText;
                        var type = doc.DocumentNode.SelectSingleNode(tmp + "/span[3]").InnerText;
                        var summary = doc.DocumentNode.SelectSingleNode(tmp + "/span[4]").InnerText.Trim(' ', '\n', '\"').Unescape();
                        if (type == "HD video")
                        {
                            result.Add(new ShowItem
                                       {
                                           Text = doc.DocumentNode.SelectSingleNode(tmp + "/span[2]").InnerText + " " + type + " " + text,
                                           Url = String.Format("http://kickass.to/usearch/{0}/", HttpUtility.UrlEncode(text).Replace("+", "%20")),
                                           Summary = summary
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