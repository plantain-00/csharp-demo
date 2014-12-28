using System;
using System.Net;
using System.Text;

namespace Ridge
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var html = new WebClient
            {
                Encoding = Encoding.UTF8
            }.DownloadString("http://www.cnblogs.com/");
            var result = new Parser().Parse(html);
            //foreach (var node in result)
            //{
            //    Console.WriteLine(node);
            //}
            Console.Read();
        }
    }
}