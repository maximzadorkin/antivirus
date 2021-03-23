using System.Collections.Generic;
using System.ServiceModel;

namespace ServiceDll
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        void startScanner(string path); // отправить результаты

        [OperationContract]
        void stopScanner(); // отправить результаты

        [OperationContract]
        string logScanner();

        [OperationContract]
        bool getScanStatus();



        [OperationContract]
        void startMonitoring(string path); // отправить результаты

        [OperationContract]
        void stopMonitoring(); // отправить результаты

        [OperationContract]
        string logMonitoring();



        [OperationContract]
        void handlerFiles(List<FileDS> files);



        [OperationContract]
        void addPlan(PlanDS plan);

        [OperationContract]
        void removePlan(PlanDS plan);

        [OperationContract]
        List<PlanDS> getAllPlans();



        [OperationContract]
        List<string> getVirusesFiles();
    }
}
