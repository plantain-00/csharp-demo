using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HtmlAgilityPack;

using NewsCatcher.Models;

namespace NewsCatcher
{
    internal static class CnBeta
    {
        public const string FAILS_MESSAGE = "CnBeta Fails";

        public static IEnumerable<ShowItem> Do()
        {
            var result = new List<ShowItem>
                         {
                             new ShowItem
                             {
                                 Text = "cnbeta:",
                                 Url = "title"
                             }
                         };
            try
            {
                var doc = new HtmlDocument();
                var html = new XWebClient
                           {
                               Encoding = Encoding.UTF8
                           }.DownloadString("http://www.cnbeta.com/");
                doc.LoadHtml(html);
                var cnBetas = new List<Model>();
                for (var i = 0; i < 60; i++)
                {
                    var tmp = String.Format("//*[@id='allnews_all']/div[1]/div[{0}]", i + 1);
                    try
                    {
                        cnBetas.Add(new Model
                                    {
                                        Title = doc.DocumentNode.SelectSingleNode(tmp + "/div[1]/div[1]/a[1]").InnerText.Unescape(),
                                        CommentNumber = Convert.ToInt32(doc.DocumentNode.SelectSingleNode(tmp + "/div[3]/ul[1]/li[2]/em[1]").InnerText),
                                        Url = "http://www.cnbeta.com/" + doc.DocumentNode.SelectSingleNode(tmp + "/div[1]/div[1]/a[1]").Attributes["href"].Value.Trim('/'),
                                        Time = Convert.ToDateTime(doc.DocumentNode.SelectSingleNode(tmp + "/div[1]/div[1]/div[1]/span[1]/em[1]").InnerText)
                                    });
                    }
                    catch (Exception)
                    {
                    }
                }
                result.AddRange(cnBetas.Where(r => r.CommentNumber >= 20).OrderByDescending(r => r.CommentNumber).ThenByDescending(r => r.Time).Select(cnBeta => new ShowItem
                                                                                                                                                                 {
                                                                                                                                                                     Text = cnBeta.CommentNumber + " " + cnBeta.Time.ToString("yyyy-MM-dd HH:mm") + " " + cnBeta.Title,
                                                                                                                                                                     Summary = cnBeta.Summary,
                                                                                                                                                                     Url = cnBeta.Url
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
            public string Title { get; set; }
            public string Summary { get; set; }
            public DateTime Time { get; set; }
            public string Url { get; set; }
            public int CommentNumber { get; set; }
        }
    }
}