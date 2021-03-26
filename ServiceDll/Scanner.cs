using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ServiceDll
{
    public class Scanner
    {
        private bool scanning = false;

        public string pathName;
        public bool isFile;

        private string[] entries;
        private List<FileDS> filesForScan;

        public bool getScanStatus()
        {
            return this.scanning;
        }

        public bool start(string path)
        {
            if (this.scanning) return false;
            this.pathName = path;
            this.isFile = File.Exists(path);
            this.filesForScan = new List<FileDS>();

            this.scanning = true;

            if (this.isFile)
                this.filesForScan.Add(new FileDS(this.pathName));
            else
                this.folderScanner(this.pathName);

            this.filesScanner();

            this.stop();
            return true;
        }

        public void stop()
        {
            this.scanning = false;
        }

        private void folderScanner(string folderPath)
        {
            string[] allEntries = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
            this.entries = Array.FindAll(
                allEntries,
                s => DangersDetection.SupportedExtensions.Contains(Path.GetExtension(s).ToLower())
            );

            foreach (string path in this.entries)
            {
                if (!this.scanning) return;
                this.filesForScan.Add(new FileDS(path));
            }
        }

        private void filesScanner()
        {
            int fileIndex = 0;
            int dangerFilesCount = 0;
            DangersDetection detection = new DangersDetection();
            foreach (FileDS file in this.filesForScan)
            {
                if (!this.scanning) break;
                file.danger = detection.detectDanger(file.path);
                file.isChecked = true;
                if (file.danger) dangerFilesCount += 1;
                fileIndex += 1;
            }
        }

        public string getScanResult()
        {
            string TEXT_DangerousFiles = "";
            int countCheckedFiles = 0;
            int dangersCount = 0;

            foreach (FileDS file in this.filesForScan)
            {
                if (!file.isChecked) break;
                countCheckedFiles += 1;
                if (file.danger)
                {
                    dangersCount += 1;
                    TEXT_DangerousFiles += $"{file.path}\n";
                }
            }

            string TEXT_Scanned = $"Всего файлов: {this.filesForScan.Count}";
            string TEXT_AlreadyScanned = $"Проверено: {countCheckedFiles}";
            string TEXT_DangerFilesCount = $"Найдено уязвимостей: {dangersCount}";
            
            string TEXT_Log = TEXT_Scanned + "\n"
                + TEXT_AlreadyScanned + "\n"
                + TEXT_DangerFilesCount + "\n"
                + TEXT_DangerousFiles;
            return TEXT_Log;
        }
    }
}
