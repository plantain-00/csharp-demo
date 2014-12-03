using System.ServiceModel;

namespace WCF.MQ.Demo
{
    [ServiceContract]
    public interface IFruitService
    {
        [OperationContract(IsOneWay = true)]
        void GetFruitInfo(string fruitName, string price);
    }
}
