using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NewsCatcher.Models;

using Ridge;
using Ridge.Nodes;

namespace NewsCatcher
{
    public class CzechMassage
    {
        public const string FAILS_MESSAGE = "Czech Massage Fails";
        private static readonly string seedWebsite = ConfigurationManager.AppSettings["seed_website"];

        public static IEnumerable<ShowItem> Do()
        {
            var result = new List<ShowItem>
                         {
                             new ShowItem
                             {
                                 Text = "Czech Massage:",
                                 Url = "title"
                             }
                         };
            try
            {
                var html = new XWebClient
                           {
                               Encoding = Encoding.UTF8
                           }.DownloadString(seedWebsite + "usearch/Czech%20Massage/?field=time_add&sorder=desc");
                var doc = new Document(html);
                var tvs = new List<Model>();
                var root = doc["#mainSearchTable"][0][0]["div", 3]["table"];
                for (var i = 0; i < 25; i++)
                {
                    try
                    {
                        var tmp = root["tr", i + 1];
                        var a = tmp["td"]["div", 1]["div"]["a"];
                        tvs.Add(new Model
                                {
                                    Title = a.As<Tag>().Text.Unescape(),
                                    Size = tmp["td", 1].As<Tag>().Text,
                                    Url = seedWebsite + a.As<Tag>()["href"].Trim('/'),
                                    Files = Convert.ToInt32(tmp["td", 2].As<Tag>().Text),
                                    Age = tmp["td", 3].As<Tag>().Text.Unescape(),
                                    Seed = Convert.ToInt32(tmp["td", 4].As<Tag>().Text),
                                    Leech = Convert.ToInt32(tmp["td", 5].As<Tag>().Text)
                                });
                    }
                    catch (Exception)
                    {
                    }
                }
                result.AddRange(tvs.Where(m => m.Leech > 1 || m.Seed > 1).Select(model => new ShowItem
                                                                                          {
                                                                                              Text = model.Size + " " + model.Age + " " + model.Seed + " " + model.Leech + " " + model.Title,
                                                                                              Summary = "",
                                                                                              Url = model.Url
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
            public string Age { get; set; }
            public string Size { get; set; }
            public string Url { get; set; }
            public int Seed { get; set; }
            public int Leech { get; set; }
            public int Files { get; set; }
        }
    }
}