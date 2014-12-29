using System.IO;
using System.Net;
using System.Text;

namespace Ridge.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var html = new WebClient
                       {
                           Encoding = Encoding.UTF8
                       }.DownloadString("http://www.cnblogs.com");
            var result = new Parser().Parse(html);
            var streamWriter = new StreamWriter("a.txt", false);
            foreach (var node in result)
            {
                streamWriter.Write(node);
            }
            streamWriter.Flush();
        }
    }
}