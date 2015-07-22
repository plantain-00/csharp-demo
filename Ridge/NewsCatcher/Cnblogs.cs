using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using NewsCatcher.Models;

using Ridge;

namespace NewsCatcher
{
    internal static class Cnblogs
    {
        public const string FAILS_MESSAGE = "Cnblogs Fails";

        public static IEnumerable<ShowItem> Do()
        {
            var result = new List<ShowItem>
                         {
                             new ShowItem
                             {
                                 Text = "cnblogs:",
                                 Url = "title"
                             }
                         };
            try
            {
                var tmp = Do(1);
                tmp.Item1.AddRange(Do(2).Item1);
                result.AddRange(tmp.Item2.Select(headline => new ShowItem
                                                             {
                                                                 Text = $"{headline.Title}",
                                                                 Url = headline.Url
                                                             }));
                result.AddRange(tmp.Item1.Where(r => r.CommentNumber > 1 || r.Digg > 1).OrderByDescending(b => b.CommentNumber).ThenByDescending(b => b.Digg).ThenByDescending(b => b.Time).Select(blog => new ShowItem
                                                                                                                                                                                                           {
                                                                                                                                                                                                               Text = $"{blog.CommentNumber} {blog.Digg} {blog.Time.ToString("yyyy-MM-dd HH:mm")} {blog.Title}",
                                                                                                                                                                                                               Url = blog.Url,
                                                                                                                                                                                                               Summary = blog.Summary.Paragraph(50)
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

        public static Tuple<List<Model>, List<Headline>> Do(int page)
        {
            var url = page == 1 ? "http://www.cnblogs.com/" : "http://www.cnblogs.com/sitehome/p/" + page;
            var html = new WebClient
                       {
                           Encoding = Encoding.UTF8
                       }.DownloadString(url);
            var doc = new Document(html);
            var result = new List<Model>();
            var headlines = new List<Headline>();
            if (page == 1)
            {
                for (var i = 0; i < 5; i++)
                {
                    try
                    {
                        var tmp = doc["#headline_block"]["ul"][i]["a"];
                        headlines.Add(new Headline
                                      {
                                          Title = tmp[0].As<PlainText>().Text.Unescape(),
                                          Url = tmp.As<Tag>()["href"].Trim()
                                      });
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            for (var i = 0; i < 20; i++)
            {
                try
                {
                    var div1 = doc["#post_list"][i]["div"];
                    var div2 = doc["#post_list"][i]["div", 1];
                    var date = new string(div2["div"][1].As<PlainText>().Text.Skip("发布于 ".Length).Take("0000-00-00 00:00".Length).ToArray());
                    var commentNumber = new string(div2["div"]["span"]["a"][0].As<PlainText>().Text.Skip("评论(".Length).TakeWhile(c => c != ')').ToArray());
                    result.Add(new Model
                               {
                                   Digg = Convert.ToInt32(div1["div"]["span"][0].As<PlainText>().Text.Trim()),
                                   Title = div2["h3"]["a"][0].As<PlainText>().Text.Unescape(),
                                   Url = div2["h3"]["a"].As<Tag>()["href"].Trim(),
                                   Summary = div2["p"][1].As<PlainText>().Text.Trim().Unescape(),
                                   Time = Convert.ToDateTime(date),
                                   CommentNumber = Convert.ToInt32(commentNumber)
                               });
                }
                catch (Exception)
                {
                }
            }
            return Tuple.Create(result, headlines);
        }

        public class Headline
        {
            public string Title { get; set; }
            public string Url { get; set; }
        }

        public class Model
        {
            public int Digg { get; set; }
            public string Title { get; set; }
            public string Summary { get; set; }
            public DateTime Time { get; set; }
            public string Url { get; set; }
            public int CommentNumber { get; set; }
        }
    }
}