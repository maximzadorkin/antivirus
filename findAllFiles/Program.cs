using System;
using System.IO;
using System.Collections.Generic;


namespace findAllFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            string foldername = "C:\\Users\\maxim\\Desktop\\Университет\\scaner";
            Scanner scanner = new Scanner(foldername, true);
            scanner.scanner();
            string scanResult = scanner.getScanResult();
            Console.WriteLine(scanResult);

            // bool successStart = Monitoring.startMonitoring(foldername);

            // string input = Console.ReadLine();
            // while(input != "q") {
            //     if (input == "stop")
            //         Monitoring.stopMonitoring();
            //     input = Console.ReadLine();
            // }
            // Monitoring.stopMonitoring();
        }
    }
}
