using System;
using System.Messaging;
using System.ServiceModel;

namespace WCF.MQ.Demo
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class FruitService : IFruitService
    {
        [OperationBehavior(TransactionScopeRequired = true)]
        public void GetFruitInfo(string fruitName, string price)
        {
            var info = string.Empty;
            ExceptionDetail error = null;
            try
            {
                info = string.Format("The Fruit Name Is {0} And Price Is {1} ", fruitName, price);
            }
            catch (Exception ex)
            {
                error = new ExceptionDetail(ex);
            }
            finally
            {
                if (!MessageQueue.Exists(".\\private$\\FruitResponseQueue"))
                {
                    MessageQueue.Create(".\\private$\\FruitResponseQueue", true);
                }
                using (var factory = new ChannelFactory<IFruitResponseService>(new NetMsmqBinding
                                                                               {
                                                                                   Security =
                                                                                   {
                                                                                       Mode = NetMsmqSecurityMode.None
                                                                                   }
                                                                               },
                                                                               new EndpointAddress("net.msmq://localhost/private/FruitResponseQueue")))
                {
                    var response = factory.CreateChannel();
                    response.OnGetFruitInfoCompleted(info, error);
                }
            }
        }
    }
}