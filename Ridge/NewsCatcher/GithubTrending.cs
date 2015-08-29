using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NewsCatcher.Models;

using Ridge;

namespace NewsCatcher
{
    internal static class GithubTrending
    {
        public const string FAILS_MESSAGE = "GithubTrending Fails";

        public static IEnumerable<ShowItem> Do()
        {
            var result = new List<ShowItem>
                         {
                             new ShowItem
                             {
                                 Text = "github trending:",
                                 Url = "title"
                             }
                         };
            try
            {
                var html = new XWebClient
                           {
                               Encoding = Encoding.UTF8
                           }.DownloadString("https://github.com/trending");
                var doc = new Document(html);
                var trendings = new List<Model>();
                var nodes = doc["#js-pjax-container"]?["div", 1]?["div", 1]?["div"]?["div", 1]?["ol"];
                if (nodes?.Children != null)
                {
                    foreach (var node in nodes.Children)
                    {
                        try
                        {
                            var text = node["p", 1]?[0]?.As<PlainText>()?.Text?.Unescape()?.Trim()?.Split('•');
                            trendings.Add(new Model
                                          {
                                              Title = node["h3"]?["a"]?.As<Tag>()?.Text?.Unescape(),
                                              Url = "https://github.com/" + node["h3"]?["a"]?.As<Tag>()?["href"]?.Trim('/'),
                                              Summary = node["p"]?.As<Tag>()?.Text?.Unescape()?.Trim() + " - " + text[0]?.Trim() + ((text.Length == 2) ? "" : " - " + text[1]?.Trim())
                                          });
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                result.AddRange(trendings.Select(t => new ShowItem
                                                      {
                                                          Text = t.Title + " " + t.Summary,
                                                          Summary = t.Summary,
                                                          Url = t.Url
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
            public string Url { get; set; }
        }
    }
}