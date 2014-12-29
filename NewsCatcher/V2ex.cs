using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HtmlAgilityPack;

using NewsCatcher.Models;

namespace NewsCatcher
{
    internal static class V2ex
    {
        public const string FAILS_MESSAGE = "V2ex Fails";
        public static IEnumerable<ShowItem> Do()
        {
            var result = new List<ShowItem>
                         {
                             new ShowItem
                             {
                                 Text = "v2ex:",
                                 Url = "title"
                             }
                         };
            try
            {
                var doc = new HtmlDocument();
                var html = new XWebClient
                           {
                               Encoding = Encoding.UTF8
                           }.DownloadString("http://www.v2ex.com/?tab=tech");
                doc.LoadHtml(html);
                var models = new List<Model>();
                for (var i = 0; i < 40; i++)
                {
                    var tmp = String.Format("//*[@id='Main']/div[2]/div[{0}]/table/tr", i + 3);
                    var name = doc.DocumentNode.SelectSingleNode(tmp + "/td[3]/span[1]/a").InnerText.Unescape();
                    var url = doc.DocumentNode.SelectSingleNode(tmp + "/td[3]/span[1]/a").Attributes["href"].Value;
                    int count;
                    try
                    {
                        count = Convert.ToInt32(doc.DocumentNode.SelectSingleNode(tmp + "/td[4]/a").InnerText);
                    }
                    catch (Exception)
                    {
                        count = 0;
                    }
                    models.Add(new Model
                               {
                                   Count = count,
                                   Url = "http://www.v2ex.com" + new string(url.TakeWhile(u => u != '#').ToArray()),
                                   Name = name
                               });
                }
                result.AddRange(models.Where(m => m.Count >= 10).OrderByDescending(m => m.Count).Select(m => new ShowItem
                                                                                                           {
                                                                                                               Text = m.Count + " " + m.Name,
                                                                                                               Url = m.Url
                                                                                                           }));
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

        private class Model
        {
            public string Name { get; set; }
            public string Url { get; set; }
            public int Count { get; set; }
        }
    }
}