using System;
using System.ServiceModel;

using WCF.MQ.Demo;

namespace WCF.MQ.Server
{
    internal class Program
    {
        private static void Main()
        {
            var fruitServiceHost = new ServiceHost(typeof (FruitService), new Uri("net.msmq://localhost/private/GetFruitInfoQueue"));
            fruitServiceHost.AddServiceEndpoint(typeof (IFruitService),
                                                new NetMsmqBinding
                                                {
                                                    Security =
                                                    {
                                                        Mode = NetMsmqSecurityMode.None
                                                    }
                                                },
                                                "");
            fruitServiceHost.Open();

            Console.WriteLine("The Service Is Running ... ");
            Console.ReadLine();
            fruitServiceHost.Close();
        }
    }
}