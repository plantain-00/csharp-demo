﻿using System;
using System.Timers;

namespace TopShelf.Demo
{
    public class TownCrier
    {
        private readonly Timer _timer;

        public TownCrier()
        {
            _timer = new Timer(1000)
                     {
                         AutoReset = true
                     };
            _timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is {0} an all is well", DateTime.Now);
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}