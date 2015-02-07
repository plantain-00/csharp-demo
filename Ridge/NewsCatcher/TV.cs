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
    public class TV
    {
        public const string FAILS_MESSAGE = "TV Fails";

        public static IEnumerable<ShowItem> Do()
        {
            var result = new List<ShowItem>
                         {
                             new ShowItem
                             {
                                 Text = "TV:",
                                 Url = "title"
                             }
                         };
            try
            {
                var html = new XWebClient
                           {
                               Encoding = Encoding.UTF8
                           }.DownloadString("https://kickass.so/");
                var doc = new Document(html);
                var tvs = new List<Model>();
                for (var i = 0; i < 10; i++)
                {
                    var tmp = doc["#wrapperInner"]["div", 5]["table"]["tr"]["td"]["div"]["table"]["tr", i + 1];
                    try
                    {
                        tvs.Add(new Model
                                {
                                    Title = tmp["td"]["div", 1]["a"][0].As<PlainText>().Text.Unescape(),
                                    Size = tmp["td", 1][0].As<PlainText>().Text + " " + tmp["td", 1]["span"][0].As<PlainText>().Text,
                                    Url = "https://kickass.so/" + tmp["td"]["div", 1]["a"].As<Tag>()["href"].Trim('/'),
                                    Age = tmp["td", 3][0].As<PlainText>().Text.Unescape()
                                });
                    }
                    catch (Exception)
                    {
                    }
                }
                for (var i = 0; i < 10; i++)
                {
                    var tmp = doc["#wrapperInner"]["div", 5]["table"]["tr"]["td"]["div"]["div"]["table"]["tr", i + 1];
                    try
                    {
                        tvs.Add(new Model
                                {
                                    Title = tmp["td"]["div", 1]["a"][0].As<PlainText>().Text.Unescape(),
                                    Size = tmp["td", 1][0].As<PlainText>().Text + " " + tmp["td", 1]["span"][0].As<PlainText>().Text,
                                    Url = "https://kickass.so/" + tmp["td"]["div", 1]["a"].As<Tag>()["href"].Trim('/'),
                                    Age = tmp["td", 3][0].As<PlainText>().Text.Unescape()
                                });
                    }
                    catch (Exception)
                    {
                    }
                }
                result.AddRange(tvs.Select(model => new ShowItem
                                                    {
                                                        Text = model.Title + " " + model.Size + " " + model.Age,
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
        }
    }
}