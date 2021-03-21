using System.Threading;
using System;
using System.IO;

namespace findAllFiles
{
    class DangersDetection 
    {
        // private string supportedExtensions = "*.exe,*.elf,*.zip,*.txt";
        static public string SupportedExtensions { get; private set; }

        public bool detectDanger(string filePath) {
            FileStream stream = this.GetFileStream(filePath);
            if (stream == null) return false;

            bool isExecutable = this.isExecutable(filePath, stream);

            bool isDanger;

            return isDanger;
        }
        
        private bool exe(string fileName) {
            return false;
        }

        private bool elf(string fileName) {
            return false;
        }

        private bool zip(string fileName) {
            return false;
        }

        private bool isExecutable(string path, FileStream file, bool fromZip = false) {
            bool result = false;
            switch ((char)file.ReadByte())
            {
                case 'M': //MZ
                    if ((char)file.ReadByte() == 'Z')
                    {
                        if (fromZip) CheckFileStream(file, path);
                        result = true;
                    }
                    break;
                case '.': //.ELF
                    if ((char)file.ReadByte() == 'E' &&
                        (char)file.ReadByte() == 'L' &&
                        (char)file.ReadByte() == 'F')
                    {
                        if (fromZip) CheckFileStream(file, path);
                        result = true;
                    }
                    break;
                case 'P': //PK
                    if ((char)file.ReadByte() == 'K')
                    {
                        Console.WriteLine("Zip file found at: " + path);
                        if (fromZip) ScanZipFileStream(file, path);
                        else ArchivesToCheck.Enqueue(path);
                    }
                    break;
                default:
                    break;
            }
            file.Close();
            return result;
        }

        private bool isZip(FileStream stream) {
            return false;
        }

        private FileStream GetFileStream(string path)
        {
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    return stream;
                }
            }
            catch (IOException)
            {
                return null;
            }
        }

    }
}