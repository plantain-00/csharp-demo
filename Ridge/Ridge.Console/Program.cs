using System;
using System.IO;
using System.Net;
using System.Text;

namespace Ridge.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var html = new XWebClient
                       {
                           Encoding = Encoding.UTF8
                       }.DownloadString("https://kickass.so/");
            var document = new Document(html);
            var streamWriter = new StreamWriter("a.txt", false);
            streamWriter.Write(document);
            streamWriter.Flush();
            var node = document["#cb_search"];
            System.Console.WriteLine(node);
            System.Console.Read();
        }
    }

    public class XWebClient : WebClient
    {
        public XWebClient()
        {
            Cookies = new CookieContainer();
        }
        public CookieContainer Cookies { get; private set; }
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