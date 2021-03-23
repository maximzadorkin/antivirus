﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ServiceDll
{
    public class Scanner
    {
        static private bool scanning = false;

        public string pathName;
        public bool isFile;

        private string[] entries;
        static private List<FileDS> filesForScan;

        static public bool getScanStatus()
        {
            return Scanner.scanning;
        }

        public bool start(string path)
        {
            if (Scanner.scanning) return false;
            this.pathName = path;
            this.isFile = File.Exists(path);
            Scanner.filesForScan = new List<FileDS>();

            Scanner.scanning = true;

            DangersDetection detection = new DangersDetection();
            if (this.isFile && detection.detectDanger(this.pathName))
                Scanner.filesForScan.Add(new FileDS(this.pathName));
            else
                this.folderScanner(this.pathName);

            this.filesScanner();

            this.stop();
            return true;
        }

        public void stop()
        {
            Scanner.scanning = false;
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
                if (!Scanner.scanning) return;
                Scanner.filesForScan.Add(new FileDS(path));
            }
        }

        private void filesScanner()
        {
            int fileIndex = 0;
            int dangerFilesCount = 0;
            DangersDetection detection = new DangersDetection();
            foreach (FileDS file in Scanner.filesForScan)
            {
                if (!Scanner.scanning) break;

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                
                file.danger = detection.detectDanger(file.path);
                file.isChecked = true;
                stopwatch.Stop();
                if (file.danger) dangerFilesCount += 1;
                fileIndex += 1;
            }
        }

        public string logger()
        {
            int countCheckedFiles = 0;
            int dangersCount = 0;

            foreach (FileDS file in Scanner.filesForScan)
            {
                if (!file.isChecked) break;
                countCheckedFiles += 1;
                if (file.danger) dangersCount += 1;
            }

            string TEXT_Scanned = $"Всего файлов: {Scanner.filesForScan.Count}";
            string TEXT_AlreadyScanned = $"Проверено: {countCheckedFiles + 1}";
            string TEXT_DangerFilesCount = $"Найдено уязвимостей: {dangersCount}";
            string TEXT_Log = TEXT_Scanned + "\n"
                + TEXT_AlreadyScanned + "\n"
                + TEXT_DangerFilesCount;
            return TEXT_Log;
        }

        public string getScanResult()
        {
            List<FileDS> dangerousFiles = Scanner.filesForScan.FindAll((FileDS file) => file.danger);
            string TEXT_CountAllFiles = $"Всего файлов найдено: {Scanner.filesForScan.Count}";
            string TEXT_DangerousFilesCount = $"Найдено уязвимостей: {dangerousFiles.Count}";
            string TEXT_DangerousFiles = "";
            foreach (FileDS file in Scanner.filesForScan)
            {
                if (file.danger) TEXT_DangerousFiles += $"{file.path}\n";
            }
            string TEXT_Result = $"{TEXT_CountAllFiles}\n{TEXT_DangerousFilesCount}\n{TEXT_DangerousFiles}";
            return TEXT_Result;
        }
    }
}
