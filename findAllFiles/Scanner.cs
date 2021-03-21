using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Text;

namespace findAllFiles
{
    class Scanner
    {
        public string pathName;
        public bool isFile;
        private bool needScanLogger;

        private class FileInformation {
            public string path;
            public bool danger;
            public bool inZip = false;
            public FileInformation(string path, bool danger) {
                this.path = path;
                this.danger = danger;
            }
        }

        private string [] entries;
        private List<FileInformation> filesForScan = new  List<FileInformation>();

        public Scanner(string path, bool needScanLogger = false) {
            this.pathName = path;
            this.isFile = File.Exists(path);
            this.needScanLogger = needScanLogger;
        }

        public void scanner() {
            DangersDetection detection = new DangersDetection();
            if (this.isFile && detection.detectDanger(this.pathName)) 
                this.filesForScan.Add(new FileInformation(this.pathName, true));
            else 
                this.folderScanner(this.pathName);
            this.filesScanner();
        }

        private void folderScanner(string folderPath) {
            string[] allEntries = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
            this.entries = Array.FindAll(
                allEntries,
                s => DangersDetection.SupportedExtensions.Contains(Path.GetExtension(s).ToLower())
            );
            foreach(string path in this.entries) {
                bool isZip = Path.GetExtension(path).ToLower() == ".zip";
                if (isZip)
                    Console.WriteLine(path);
                    //this.zipScanner(path);
                else
                    this.filesForScan.Add(new FileInformation(path, false));
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

        private void filesScanner() {
            int fileIndex = 0;
            int dangerFilesCount = 0;
            DangersDetection detection = new DangersDetection();
            foreach (FileInformation file in this.filesForScan) {
                file.danger = detection.detectDanger(file.path);
                if (file.danger) dangerFilesCount += 1;
                if (this.needScanLogger) this.logger(fileIndex, dangerFilesCount);
                fileIndex += 1;
            }
        }

        // TODO
        private bool logger(int fileIndex, int dangersCount) {
            string TEXT_Scanned = $"Всего файлов: {this.filesForScan.Count}";
            string TEXT_AlreadyScanned = $"Проверено: {fileIndex + 1}";
            string TEXT_DangerFilesCount = $"Найдено уязвимостей: {dangersCount}";
            string TEXT_Log = TEXT_Scanned + "\n" 
                + TEXT_AlreadyScanned + "\n"
                + TEXT_DangerFilesCount;
            Console.WriteLine(TEXT_Log);
            // отправлять на клиент сведения
            return true;
        }

        // TODO
        public string getScanResult() {
            List<FileInformation> dangerousFiles = this.filesForScan.FindAll((FileInformation file) => file.danger);
            string TEXT_CountAllFiles = $"Всего файлов просканировано: {this.filesForScan.Count}";
            string TEXT_DangerousFilesCount = $"Найдено уязвимостей: {dangerousFiles.Count}";
            string TEXT_DangerousFiles = "";
            foreach (FileInformation file in this.filesForScan) {
                TEXT_DangerousFiles += $"{file.path}\n";
            }
            string TEXT_Result = $"{TEXT_CountAllFiles}\n{TEXT_DangerousFilesCount}\n{TEXT_DangerousFiles}";
            // отправить на клиент итоги
            return TEXT_Result;
        }
    }   
}