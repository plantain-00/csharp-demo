using System.ServiceModel;

namespace WCF.MQ.Demo
{
    [ServiceContract]
    public interface IFruitResponseService
    {
        [OperationContract(IsOneWay = true)]
        void OnGetFruitInfoCompleted(string fruitInfo, ExceptionDetail error);
    }
}