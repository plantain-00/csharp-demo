using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NewsCatcher.Models;

using Ridge;
using Ridge.Nodes;

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
                var html = new XWebClient
                           {
                               Encoding = Encoding.UTF8
                           }.DownloadString("http://www.cnbeta.com/");
                var doc = new Document(html);
                var cnBetas = new List<Model>();
                for (var i = 0; i < 60; i++)
                {
                    var node = doc["#allnews_all"][1];
                    if (node.Children == null || node.Children.Count <= i)
                    {
                        break;
                    }
                    node = node[i];
                    try
                    {
                        cnBetas.Add(new Model
                                    {
                                        Title = node[0][0][0][0].As<PlainText>().Text.Unescape(),
                                        CommentNumber = Convert.ToInt32(node[2][1][1][1][0].As<PlainText>().Text),
                                        Url = "http://www.cnbeta.com/" + node[0][0][0].As<Tag>()["href"].Trim('/'),
                                        Time = Convert.ToDateTime(node[0][0][1][0][1][0].As<PlainText>().Text)
                                    });
                    }
                    catch (Exception)
                    {
                    }
                }
                result.AddRange(cnBetas.Where(r => r.CommentNumber >= 10).OrderByDescending(r => r.CommentNumber).ThenByDescending(r => r.Time).Select(cnBeta => new ShowItem
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