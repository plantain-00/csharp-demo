using System;
using System.Net;
using System.Text;

namespace Ridge
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var parser = new Parser();
            var html = new WebClient
            {
                Encoding = Encoding.UTF8
            }.DownloadString("http://www.cnblogs.com/");
            var nodes = parser.Parse(html);
            Console.Read();
        }
    }
}