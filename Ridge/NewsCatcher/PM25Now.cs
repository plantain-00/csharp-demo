using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using NewsCatcher.Models;

using Ridge;
using Ridge.Nodes;

namespace NewsCatcher
{
    internal static class PM25Now
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
                var html = new XWebClient
                           {
                               Encoding = Encoding.UTF8
                           }.DownloadString("http://www.chapm25.com/city/shanghai.html");
                var doc = new Document(html);
                for (var i = 1; i < 5; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        try
                        {
                            var tmp = doc["html"]["body"]["div", 1]["div"]["div", 1]["div", i]["div", j]["h2"]["div"];
                            var name = tmp["div"]["a"][0].As<PlainText>().Text;
                            var value = tmp["div", 1]["span"][0].As<PlainText>().Text;
                            var title = tmp["div", 1]["span"].As<Tag>()["title"];
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