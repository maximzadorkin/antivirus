using System;

namespace ServiceTestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {   
            string foldername = "C:\\Users\\maxim\\Desktop\\Университет\\scaner\\realy";
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
                        break;
                    case "removePlan":
                        break;
                    case "deleteFile":
                        break;
                    case "addToQuarantine":
                        break;
                    case "removeToQuarantine":
                        break;
                    default:
                        break;
                }
                input = Console.ReadLine();
            }
        }
    }
}
