using System;

namespace RestSharp.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var client = new RestClient("https://api.github.com/gists");
            var request = new RestRequest();
            var response = client.Execute(request);
            var content = response.Content;
            Console.WriteLine(content);
            Console.Read();
        }
    }
}