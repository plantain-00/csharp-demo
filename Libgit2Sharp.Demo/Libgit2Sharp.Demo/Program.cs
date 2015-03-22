using System;

using LibGit2Sharp;

namespace Libgit2Sharp.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var result = Repository.Clone("https://github.com/plantain-00/resume", @"C:\Users\York Yao\Documents\test");
            Console.Read();
        }
    }
}