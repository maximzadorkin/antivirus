using System.Collections.Generic;
using System.Threading;

namespace ServiceDll
{
    public class Service : IService
    {
        static public Scanner scanner = new Scanner();
        static public DataBase database = new DataBase();

        public void startScanner(string path) 
        {
            Thread thread = new Thread(new ThreadStart(
                () => Service.scanner.start(path)
            ));
            thread.Start();
        }
        public void stopScanner() 
        {
            Service.scanner.stop();
        }
        public string logScanner() 
        {
            return scanner.logger();
        }
        public bool getScanStatus()
        {
            return Scanner.getScanStatus();
        }



        public void startMonitoring(string path) 
        {
            Thread thread = new Thread(new ThreadStart(
                () => Monitoring.start(path)
            ));
            thread.Start();
        }
        public void stopMonitoring() 
        {
            Monitoring.stop();
        }
        public string logMonitoring()
        {
            return Monitoring.result();
        }


        public void handlerFiles(List<FileDS> files) 
        { 
            foreach (FileDS file in files) {
                switch (file.fileHandler)
                {
                    case FileDS.FilesHandler.Allow:
                        break;
                    case FileDS.FilesHandler.ToQuarantine:
                        FilesWorker.addFileToQuarantine(file.path);
                        break;
                    case FileDS.FilesHandler.Delete:
                        FilesWorker.deleteFile(file.path);
                        break;
                    default:
                        break;
                }
            }
        }

        public void addPlan(PlanDS plan)
        {
            Service.database.addPlan(plan);
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
    }
}
