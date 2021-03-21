using System.ServiceModel;

namespace ServiceDll
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        string Method1(string x);
        [OperationContract]
        string Method2(string x);
    }
}
