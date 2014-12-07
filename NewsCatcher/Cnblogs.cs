using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using HtmlAgilityPack;

using NewsCatcher.Models;

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
                                                                 Text = String.Format("{0}", headline.Title),
                                                                 Url = headline.Url
                                                             }));
                result.AddRange(tmp.Item1.Where(r => r.CommentNumber > 1 || r.Digg > 1).OrderByDescending(b => b.CommentNumber).ThenByDescending(b => b.Digg).ThenByDescending(b => b.Time).Select(blog => new ShowItem
                                                                                                                                                                                                           {
                                                                                                                                                                                                               Text = String.Format("{0} {1} {2} {3}", blog.CommentNumber, blog.Digg, blog.Time.ToString("yyyy-MM-dd HH:mm"), blog.Title),
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
            var doc = new HtmlDocument();
            var html = new WebClient
                       {
                           Encoding = Encoding.UTF8
                       }.DownloadString(url);
            doc.LoadHtml(html);
            var result = new List<Model>();
            var headlines = new List<Headline>();
            if (page == 1)
            {
                for (var i = 0; i < 5; i++)
                {
                    try
                    {
                        var tmp = String.Format("//*[@id='headline_block']/ul/li[{0}]/a[1]", i + 1);
                        headlines.Add(new Headline
                                      {
                                          Title = doc.DocumentNode.SelectSingleNode(tmp).InnerText.Unescape(),
                                          Url = doc.DocumentNode.SelectSingleNode(tmp).Attributes["href"].Value.Trim()
                                      });
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            for (var i = 0; i < 20; i++)
            {
                var div1 = String.Format("//*[@id='post_list']/div[{0}]/div[1]", i + 1);
                var div2 = String.Format("//*[@id='post_list']/div[{0}]/div[2]", i + 1);
                var tmp2 = new string(doc.DocumentNode.SelectSingleNode(div2 + "/div").InnerText.TrimStart(' ', '\r', '\n').SkipWhile(c => c != '\n').Skip(9).TakeWhile(c => c != 'ÔÄ').ToArray()).Trim();
                result.Add(new Model
                           {
                               Digg = Convert.ToInt32(doc.DocumentNode.SelectSingleNode(div1 + "/div[1]").InnerText.Trim()),
                               Title = doc.DocumentNode.SelectSingleNode(div2 + "/h3[1]").InnerText.Unescape(),
                               Url = doc.DocumentNode.SelectSingleNode(div2 + "/h3[1]/a[1]").Attributes["href"].Value.Trim(),
                               Summary = doc.DocumentNode.SelectSingleNode(div2 + "/p[1]").InnerText.Trim().Unescape(),
                               Time = Convert.ToDateTime(new string(tmp2.Take("0000-00-00 00:00".Length).ToArray())),
                               CommentNumber = Convert.ToInt32(new string(tmp2.Skip("0000-00-00 00:00".Length).SkipWhile(c => c != 'ÆÀ').Skip(3).ToArray()).Trim(')'))
                           });
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