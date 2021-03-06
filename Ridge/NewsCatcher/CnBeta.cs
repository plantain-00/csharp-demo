using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NewsCatcher.Models;

using Ridge;

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
                var nodes = doc.GetElementById("allnews_all")?["div"];
                if (nodes?.Children != null)
                {
                    foreach (var node in nodes.Children)
                    {
                        try
                        {
                            cnBetas.Add(new Model
                                        {
                                            Title = node["div"]?["div"]?["a"]?[0]?.As<PlainText>()?.Text?.Unescape(),
                                            CommentNumber = Convert.ToInt32(node["div", 2]?["ul"]?["li", 1]?["em"]?[0]?.As<PlainText>()?.Text),
                                            Url = "http://www.cnbeta.com/" + node["div"]?["div"]?["a"]?.As<Tag>()?["href"]?.Trim('/'),
                                            Time = Convert.ToDateTime(node["div"]?["div"]?["div"]?["span"]?["em"]?[0]?.As<PlainText>()?.Text)
                                        });
                        }
                        catch (Exception)
                        {
                        }
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

        private sealed class Model
        {
            public string Title { get; set; }
            public string Summary { get; set; }
            public DateTime Time { get; set; }
            public string Url { get; set; }
            public int CommentNumber { get; set; }
        }
    }
}