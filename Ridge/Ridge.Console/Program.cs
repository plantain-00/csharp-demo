using System;
using System.IO;
using System.Net;
using System.Text;

using ParseLibrary;

namespace Ridge.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var html = new XWebClient
                       {
                           Encoding = Encoding.UTF8
                       }.DownloadString("http://katproxy.com/usearch/Czech%20Massage/?field=time_add&sorder=desc");
            var document = new Document(html);
            var streamWriter = new StreamWriter("a.txt", false);
            streamWriter.Write(document.ToString(Formatting.Indented));
            streamWriter.Flush();
        }
    }

    public class XWebClient : WebClient
    {
        public XWebClient()
        {
            Cookies = new CookieContainer();
        }
        public CookieContainer Cookies { get; }
        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address) as HttpWebRequest;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            if (request.CookieContainer == null)
            {
                request.CookieContainer = Cookies;
            }
            return request;
        }
    }
}