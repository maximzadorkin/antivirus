﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ServiceTestConsoleApp
{
    class Scanner
    {
        static private bool scanning = false;

        public string pathName;
        public bool isFile;
        private bool needScanLogger;

        private string[] entries;
        static private List<FileDS> filesForScan;

        public bool start(string path, bool needScanLogger = false)
        {
            if (Scanner.scanning) return false;
            this.pathName = path;
            this.isFile = File.Exists(path);
            this.needScanLogger = needScanLogger;
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
                //bool isZip = Path.GetExtension(path).ToLower() == ".zip";
                //if (isZip)
                //    Console.WriteLine("isZipFile" + path);
                //this.zipScanner(path);
                //else
                Scanner.filesForScan.Add(new FileDS(path));
            }
        }

        // TODO
        //         private void zipScanner(string zipPath) {
        //             ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Read, Encoding.GetEncoding(866));
        //             string pathToZip = zipPath.Substring(0, zipPath.Length - 4) + "\\";
        //             foreach (ZipArchiveEntry entry in archive.Entries) {
        //                 archiveScanner(pathToZip + entry.FullName);
        // Console.WriteLine(pathToZip + entry.FullName);
        //             }
        //         }

        // private void archiveScanner(string path, ZipArchive archive) {
        //     this.filesForScan.Add(new FileInformation(path, false));
        // }

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

                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds / 1000);
                Console.WriteLine(file.path);
                Console.WriteLine(file.danger);
                if (file.danger) dangerFilesCount += 1;
                if (this.needScanLogger) this.logger(fileIndex, dangerFilesCount);
                fileIndex += 1;
            }
        }

        private string logger(int fileIndex, int dangersCount)
        {
            string TEXT_Scanned = $"Всего файлов: {Scanner.filesForScan.Count}";
            string TEXT_AlreadyScanned = $"Проверено: {fileIndex + 1}";
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
