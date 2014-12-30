using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using HtmlAgilityPack;

using NewsCatcher.Models;

namespace NewsCatcher
{
    static internal class PM25Now
    {
        public const string FAILS_MESSAGE = "PM2.5Now Fails";
        public static IEnumerable<ShowItem> Do()
        {
            var result = new List<ShowItem>
                         {
                             new ShowItem
                             {
                                 Text = "PM 2.5:",
                                 Url = "title"
                             }
                         };
            try
            {
                var doc = new HtmlDocument();
                var html = new XWebClient
                           {
                               Encoding = Encoding.UTF8
                           }.DownloadString("http://www.chapm25.com/city/shanghai.html");
                doc.LoadHtml(html);
                for (var i = 1; i < 5; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        try
                        {
                            var name = doc.DocumentNode.SelectSingleNode(String.Format("/html/body/div[2]/div/div[2]/div[{0}]/div[{1}]/h2/div/div[1]/a", i + 1, j + 1)).InnerText;
                            var value = doc.DocumentNode.SelectSingleNode(String.Format("/html/body/div[2]/div/div[2]/div[{0}]/div[{1}]/h2/div/div[2]/span", i + 1, j + 1)).InnerText;
                            var title = doc.DocumentNode.SelectSingleNode(String.Format("/html/body/div[2]/div/div[2]/div[{0}]/div[{1}]/h2/div/div[2]/span", i + 1, j + 1)).Attributes["title"].Value;
                            result.Add(new ShowItem
                                       {
                                           Text = name + ":" + value,
                                           Url = "http://www.chapm25.com/city/shanghai.html",
                                           Summary = title
                                       });
                        }
                        catch (Exception)
                        {
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