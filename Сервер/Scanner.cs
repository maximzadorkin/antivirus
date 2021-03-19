using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Threading;

namespace Сервер
{
    class Scanner
    {
        public static bool work = false;
        private string path;
        private bool isFileScan;
        static int size = 2048;
        static MemoryMappedFile sharedMemory = MemoryMappedFile.CreateOrOpen("MZAntivirus_scanner_fm", size);
        static MemoryMappedViewAccessor file = sharedMemory.CreateViewAccessor(0, size);
        static string scannerMutexName = "MZAntivirus_scanner_mutex";
        static Mutex scannerMutex = new Mutex(false, scannerMutexName);

        public Scanner(string command)
        {
            string[] commandParts = command.Split('_');
            this.isFileScan = commandParts[2] == "0" ? false : true;
            this.path = command.Substring(command.IndexOf("__") + 2);
            Console.WriteLine(path);
            Console.WriteLine(scannerMutex.ToString());

            Scanner.work = true;
            this.start();
        }

        private void start()
        {
            
            while (Scanner.work)
            {
                this.logs();
            }
            this.sendResult();
        }

        private void fullStart()
        {
            while (true)
            {
                this.logs();
            }
            this.sendResult();
        }

        private void logs() {
            scannerMutex.WaitOne();
            file.Write(0, size);
            string message = "like scanner so good";
            file.WriteArray<char>(4, message.ToCharArray(), 0, message.Length);
            scannerMutex.ReleaseMutex();
        }

        private void sendResult() {
            scannerMutex.WaitOne();
            file.Write(0, size);
            string message = "endScan\n";
            file.WriteArray<char>(4, message.ToCharArray(), 0, message.Length);
            scannerMutex.ReleaseMutex();
        }
    }
}
