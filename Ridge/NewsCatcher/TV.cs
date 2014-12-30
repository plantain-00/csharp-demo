using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HtmlAgilityPack;

using NewsCatcher.Models;

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
                var doc = new HtmlDocument();
                var html = new XWebClient
                           {
                               Encoding = Encoding.UTF8
                           }.DownloadString("https://kickass.so/");
                doc.LoadHtml(html);
                var tvs = new List<Model>();
                for (var i = 0; i < 10; i++)
                {
                    var tmp = String.Format("//*[@id=\"wrapperInner\"]/div[6]/table/tr/td[1]/div/table/tr[{0}]", i + 2);
                    try
                    {
                        tvs.Add(new Model
                                {
                                    Title = doc.DocumentNode.SelectSingleNode(tmp + "/td[1]/div[2]/a").InnerText.Unescape(),
                                    Size = doc.DocumentNode.SelectSingleNode(tmp + "/td[2]").InnerText,
                                    Url = "https://kickass.so/" + doc.DocumentNode.SelectSingleNode(tmp + "/td[1]/div[2]/a").Attributes["href"].Value.Trim('/'),
                                    Age = doc.DocumentNode.SelectSingleNode(tmp + "/td[4]").InnerText.Unescape()
                                });
                    }
                    catch (Exception)
                    {
                    }
                }
                for (var i = 0; i < 10; i++)
                {
                    var tmp = String.Format("//*[@id=\"wrapperInner\"]/div[6]/table/tr/td[1]/div/div/table/tr[{0}]", i + 2);
                    try
                    {
                        tvs.Add(new Model
                                {
                                    Title = doc.DocumentNode.SelectSingleNode(tmp + "/td[1]/div[2]/a").InnerText.Unescape(),
                                    Size = doc.DocumentNode.SelectSingleNode(tmp + "/td[2]").InnerText,
                                    Url = "https://kickass.so/" + doc.DocumentNode.SelectSingleNode(tmp + "/td[1]/div[2]/a").Attributes["href"].Value.Trim('/'),
                                    Age = doc.DocumentNode.SelectSingleNode(tmp + "/td[4]").InnerText.Unescape()
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

        private class Model
        {
            public string Title { get; set; }
            public string Age { get; set; }
            public string Size { get; set; }
            public string Url { get; set; }
        }
    }
}