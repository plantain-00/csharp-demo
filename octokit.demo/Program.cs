using System;
using System.Threading.Tasks;

using Octokit;

namespace octokit.demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var github = new GitHubClient(new ProductHeaderValue("MyAmazingApp"));
            var task = github.User.Get("half-ogre");
            var user = task.Result;
            Console.WriteLine(user.Followers + " folks love the half ogre!");
            Console.Read();
        }
    }
}