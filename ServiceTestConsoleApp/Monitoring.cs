using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ServiceTestConsoleApp
{
    class Monitoring
    {
        static private bool alreadyRun = false;

        static private FileSystemWatcher watcher;
        static private List<string> dangerFiles;

        static public bool startMonitoring(string path)
        {
            if (Monitoring.alreadyRun) return false;
            Monitoring.alreadyRun = true;

            Monitoring.dangerFiles = new List<string>();

            Monitoring.watcher = new FileSystemWatcher();
            Monitoring.watcher.Path = path;
            Monitoring.watcher.NotifyFilter = NotifyFilters.LastWrite;
            Monitoring.watcher.Filter = "*";
            Monitoring.watcher.Changed += new FileSystemEventHandler(Monitoring.OnChanged);
            Monitoring.watcher.EnableRaisingEvents = true;
            Monitoring.watcher.IncludeSubdirectories = true;
            return true;
        }

        static public void stopMonitoring()
        {
            Monitoring.watcher.Dispose();
            Monitoring.alreadyRun = false;
        }

        static private void OnChanged(object source, FileSystemEventArgs e)
        {
            string path = e.FullPath;
            Thread.Sleep(3000);
            bool isFile = File.Exists(path);
            if (!isFile) return;

            DangersDetection detection = new DangersDetection();
            bool isDangerFile = detection.detectDanger(path);

            if (!isDangerFile) return;

            bool isContained = Monitoring.dangerFiles.Contains(path);
            if (!isContained) {
                Monitoring.dangerFiles.Add(path);
                Monitoring.logger();
            }
            // работа с zip
        }

        static private void logger()
        {
            string TEXT_DetectedDangers = $"Обнаружено угроз: {Monitoring.dangerFiles.Count}";
            string TEXT_PathOfDangersFiles = "";
            foreach (string path in Monitoring.dangerFiles)
            {
                TEXT_PathOfDangersFiles += $"{path}\n";
            }
            string TEXT_Result = $"{TEXT_DetectedDangers}\n{TEXT_PathOfDangersFiles}";
            Console.WriteLine(TEXT_Result);
            // отправлять данные на клиент
        }
    }
}
