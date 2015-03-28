using System;
using System.Messaging;
using System.ServiceModel;

namespace WCF.MQ.Demo
{
    internal class Program
    {
        private static void Main()
        {
            if (!MessageQueue.Exists(".\\private$\\GetFruitInfoQueue"))
            {
                MessageQueue.Create(".\\private$\\GetFruitInfoQueue", true);
            }
            if (!MessageQueue.Exists(".\\private$\\FruitResponseQueue"))
            {
                MessageQueue.Create(".\\private$\\FruitResponseQueue", true);
            }

            var fruitHost = new ServiceHost(typeof (FruitResponseService), new Uri("net.msmq://localhost/private/FruitResponseQueue"));
            fruitHost.AddServiceEndpoint(typeof (IFruitResponseService),
                                         new NetMsmqBinding
                                         {
                                             Security =
                                             {
                                                 Mode = NetMsmqSecurityMode.None
                                             }
                                         },
                                         "");
            fruitHost.Open();

            using (var factory = new ChannelFactory<IFruitService>(new NetMsmqBinding
                                                                   {
                                                                       Security =
                                                                       {
                                                                           Mode = NetMsmqSecurityMode.None
                                                                       }
                                                                   },
                                                                   new EndpointAddress("net.msmq://localhost/private/GetFruitInfoQueue")))
            {
                var fruit = factory.CreateChannel();
                fruit.GetFruitInfo("banana", "6.00");
            }
            Console.WriteLine("The Client Is Running ... ");
            Console.ReadLine();
            fruitHost.Close();
        }
    }
}