using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ServiceDll
{
    public class Monitoring
    {
        private bool alreadyRun = false;

        private FileSystemWatcher watcher;
        private List<string> dangerFiles;

        public bool start(string path)
        {
            if (this.alreadyRun) return false;
            this.alreadyRun = true;

            this.dangerFiles = new List<string>();

            this.watcher = new FileSystemWatcher();
            this.watcher.Path = path;
            this.watcher.NotifyFilter = NotifyFilters.LastWrite;
            this.watcher.Filter = "*";
            this.watcher.Changed += new FileSystemEventHandler(this.OnChanged);
            this.watcher.EnableRaisingEvents = true;
            this.watcher.IncludeSubdirectories = true;
            return true;
        }

        public void stop()
        {
            if (this.alreadyRun) this.watcher.Dispose();
            this.alreadyRun = false;
        }

        public bool getStatus()
        {
            return this.alreadyRun;
        }

        public string result()
        {
            string TEXT_DetectedDangers = $"Обнаружено угроз: {this.dangerFiles.Count}";
            string TEXT_PathOfDangersFiles = "";
            foreach (string path in this.dangerFiles)
            {
                TEXT_PathOfDangersFiles += $"{path}\n";
            }
            string TEXT_Result = $"{TEXT_DetectedDangers}\n{TEXT_PathOfDangersFiles}";
            return TEXT_Result;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            string path = e.FullPath;
            Thread.Sleep(3000);
            bool isFile = File.Exists(path);
            if (!isFile) return;

            DangersDetection detection = new DangersDetection();
            bool isDangerFile = detection.detectDanger(path);

            if (!isDangerFile) return;

            bool isContained = this.dangerFiles.Contains(path);
            if (!isContained) {
                this.dangerFiles.Add(path);
            }
        }
    }
}
