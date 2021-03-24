using System.Collections.Generic;
using System.ServiceModel;

namespace ServiceDll
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        void startScanner(string path);

        [OperationContract]
        void stopScanner();

        [OperationContract]
        string getScanResult();

        [OperationContract]
        bool getScanStatus();



        [OperationContract]
        void startMonitoring(string path);

        [OperationContract]
        void stopMonitoring();

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

        [OperationContract]
        List<string> getQuarantineList();
    }
}
