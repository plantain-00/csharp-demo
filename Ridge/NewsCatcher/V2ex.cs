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
                var html = new XWebClient
                           {
                               Encoding = Encoding.UTF8
                           }.DownloadString("http://www.v2ex.com/?tab=tech");
                var doc = new Document(html);
                var models = new List<Model>();
                for (var i = 0; i < 40; i++)
                {
                    var tmp = doc["#Main"]["div", 1]["div", i + 2]["table"]["tr"];
                    var name = tmp["td", 2]["span"]["a"][0].As<PlainText>().Text.Unescape();
                    var url = tmp["td", 2]["span"]["a"].As<Tag>()["href"];
                    int count;
                    try
                    {
                        count = Convert.ToInt32(tmp["td", 3]["a"][0].As<PlainText>().Text);
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