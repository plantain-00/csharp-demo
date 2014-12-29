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
                       }.DownloadString("http://www.cnbeta.com/");
            var document = new Document(html);
            var streamWriter = new StreamWriter("a.txt", false);
            streamWriter.Write(document);
            streamWriter.Flush();
            var node = document.GetElementById("cb_search");
            System.Console.WriteLine(node);
            System.Console.Read();
        }
    }
}