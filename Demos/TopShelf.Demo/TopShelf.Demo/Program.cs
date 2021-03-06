﻿using Topshelf;

namespace TopShelf.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            HostFactory.Run(x =>
                            {
                                x.Service<TownCrier>(s =>
                                                     {
                                                         s.ConstructUsing(name => new TownCrier());
                                                         s.WhenStarted(tc => tc.Start());
                                                         s.WhenStopped(tc => tc.Stop());
                                                     });
                                x.RunAsLocalSystem();

                                x.SetDescription("Sample Topshelf Host");
                                x.SetDisplayName("Stuff");
                                x.SetServiceName("stuff");
                            });
        }
    }
}