using System.Collections.Generic;
using System.Threading;

namespace ServiceDll
{
    public class Service : IService
    {
        static public Scanner scanner = new Scanner();
        static public DataBase database = new DataBase();
        static public Monitoring monitoring = new Monitoring();

        public void startScanner(string path)
        {
            Thread thread = new Thread(new ThreadStart(
                () => 
                    Service.scanner.start(path)
            ));
            thread.Start();
        }
        public void stopScanner()
        {
            Service.scanner.stop();
        }
        public string getScanResult()
        {
            return Service.scanner.getScanResult();
        }
        public bool getScanStatus()
        {
            return Service.scanner.getScanStatus();
        }



        public void startMonitoring(string path)
        {
            Thread thread = new Thread(new ThreadStart(
                () => monitoring.start(path)
            ));
            thread.Start();
        }
        public void stopMonitoring()
        {
            monitoring.stop();
        }
        public string logMonitoring()
        {
            return monitoring.result();
        }
        public bool getMonitoringStatus()
        {
            return monitoring.getStatus();
        }


        public void handlerFiles(List<FileDS> files)
        {
           try
            {
                DataBase db = new DataBase();
                foreach (FileDS file in files)
                {
                    switch (file.fileHandler)
                    {
                        case FileDS.FilesHandler.Allow:
                            db.removeFromFoundViruses(file.path);
                            break;
                        case FileDS.FilesHandler.ToQuarantine:
                            FilesWorker.addFileToQuarantine(file.path);
                            db.addToQuarantine(file.path);
                            db.removeFromFoundViruses(file.path);
                            break;
                        case FileDS.FilesHandler.RemoveFromQuarantine:
                            FilesWorker.removeFileFromQuarantine(file.path);
                            db.removeFromQuarantine(file.path);
                            db.removeFromFoundViruses(file.path);
                            break;
                        case FileDS.FilesHandler.Delete:
                            FilesWorker.deleteFile(file.path);
                            db.removeFromFoundViruses(file.path);
                            break;
                        default:
                            break;
                    }
                }
            } finally { }
        }

        public bool addPlan(PlanDS plan)
        {
            return Service.database.addPlan(plan);
        }

        public void removePlan(PlanDS plan)
        {
            Service.database.removePlan(plan);
        }
        public List<PlanDS> getAllPlans()
        {
            return Service.database.getAllPlans();
        }



        public List<string> getVirusesFiles()
        {
            return Service.database.getVirusesFiles();
        }

        public List<string> getQuarantineList()
        {
            return Service.database.getQuarantineFiles();
        }
    }
}
