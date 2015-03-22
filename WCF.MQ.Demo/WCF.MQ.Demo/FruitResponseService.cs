using System;
using System.ServiceModel;

namespace WCF.MQ.Demo
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class FruitResponseService : IFruitResponseService
    {
        [OperationBehavior(TransactionScopeRequired = true)]
        public void OnGetFruitInfoCompleted(string fruitInfo, ExceptionDetail error)
        {
            if (error == null)
            {
                Console.WriteLine(fruitInfo);
            }
        }
    }
}