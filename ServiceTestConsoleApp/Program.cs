using System;
using System.Threading;

namespace ServiceTestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {   
            string foldername = "C:\\Users\\maxim\\Desktop\\Университет\\scaner\\realy";
            DataBase db = new DataBase();
            Scanner scanner =  new Scanner();

            string input = Console.ReadLine();
            while (input != "exit")
            {
                switch (input)
                {
                    case "startScanner":
                        scanner.startScanner(foldername, true);
                        break;
                    case "startScanner2":
                        scanner.startScanner(foldername, true);
                        scanner.startScanner(foldername, true);
                        break;
                    case "stopScanner":
                        scanner.stopScanner();
                        break;
                    case "resultScanner":
                        Console.WriteLine(scanner.getScanResult());
                        break;
                    case "startMonitoring":
                        Monitoring.startMonitoring(foldername);
                        break;
                    case "startMonitoring2":
                        Monitoring.startMonitoring(foldername);
                        Monitoring.startMonitoring(foldername + "\\papka");
                        break;
                    case "stopMonitoring":
                        Monitoring.stopMonitoring();
                        break;
                    case "addPlan":
                        
                        DateTime time = new DateTime(2000, 08, 03, 13, 12, 0);
                        PlanDS plan = new PlanDS(foldername, time);
                        db.addPlan(plan);
                        break;
                    case "removePlan":
                        DateTime time1 = new DateTime(2000, 08, 03, 13, 12, 0);
                        PlanDS plan1 = new PlanDS(foldername, time1, 1);
                        db.removePlan(plan1);
                        break;
                    case "getPlans":
                        db.getAllPlans();
                        break;
                    case "deleteFile":
                        break;
                    case "addToQ":
                        FilesWorker.addFileToQuarantine("C:\\Users\\maxim\\Desktop\\Университет\\scaner\\realy\\papka\\7z1900-x64 — копия.exe");
                        FilesWorker.addFileToQuarantine("C:\\Users\\maxim\\Desktop\\Университет\\scaner\\realy\\papka\\FigmaSetup.exe");
                        db.addToQuarantine("C:\\Users\\maxim\\Desktop\\Университет\\scaner\\realy\\papka\\7z1900-x64 — копия.exe");
                        db.addToQuarantine("C:\\Users\\maxim\\Desktop\\Университет\\scaner\\realy\\papka\\FigmaSetup.exe");
                        db.getQuarantineFiles();
                        break;
                    case "removeFromQ":
                        FilesWorker.removeFileFromQuarantine("C:\\Users\\maxim\\Desktop\\Университет\\scaner\\realy\\papka\\7z1900-x64 — копия.exe");
                        FilesWorker.removeFileFromQuarantine("C:\\Users\\maxim\\Desktop\\Университет\\scaner\\realy\\papka\\FigmaSetup.exe");
                        db.removeFromQuarantine("C:\\Users\\maxim\\Desktop\\Университет\\scaner\\realy\\papka\\7z1900-x64 — копия.exe");
                        db.removeFromQuarantine("C:\\Users\\maxim\\Desktop\\Университет\\scaner\\realy\\papka\\FigmaSetup.exe");
                        db.getQuarantineFiles();
                        break;
                    default:
                        break;
                }
                input = Console.ReadLine();
            }
        }
    }
}
