using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace ServiceTestConsoleApp
{
    class DangersDetection
    {

        static public string SupportedExtensions { 
            get { return "*.exe,*.elf,*.zip"; }
            private set { SupportedExtensions = value; }
        }

        public bool detectDanger(string filePath)
        {
            FileStream file = this.getFileStream(filePath);
            if (file == null) return false;

            bool isExecutableOrZip = this.isExecutableOrZip(filePath, file);
            bool isDanger = false;
            if (isExecutableOrZip)
                isDanger = this.verifyFileStream(file, filePath);

            file.Close();
            return isDanger;
        }

        private bool isExecutableOrZip(string path, Stream file)
        {
            bool result = false;
            if (file.Length == 0) return result;

            switch ((char)file.ReadByte())
            {
                case 'M':
                    if ((char)file.ReadByte() == 'Z')
                        result = true;
                    break;
                case '.':
                    if ((char)file.ReadByte() == 'E' &&
                        (char)file.ReadByte() == 'L' &&
                        (char)file.ReadByte() == 'F')
                        result = true;
                    break;
                case 'P':
                    if ((char)file.ReadByte() == 'K')
                        this.scanZipFileStream(file, path);
                    break;
                default:
                    break;
            }

            return result;
        }

        private FileStream getFileStream(string path)
        {
            try
            {
                FileStream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                return stream;
            }
            catch (IOException)
            {
                return null;
            }
        }

        private bool verifyFileStream(Stream stream, string path)
        {
            int offset = 0;
            bool isDanger = false;
            byte[] data = this.getFileCode(stream, ref offset);
            if (data == null) return isDanger;
            
            DataBase db = new DataBase();
            for (int i = 0; i < data.Length - 4 && !isDanger; i++)
            {
                string sign = this.getStringOfBytes(data, i, 4);
                List<VirusDS> viruses = db.getViruses(sign, offset + i);
                foreach (VirusDS virus in viruses)
                {
                    if (this.signatureOnFullMatch(virus, data, i))
                    {
                        isDanger = true;
                        return isDanger;
                    }
                }
            }

            return isDanger;
        }

        private byte[] getFileCode(Stream stream, ref int offset) // return byte[] or null
        {
            bool textIsFound = false;
            while (!textIsFound)
            {
                switch ((char)stream.ReadByte())
                {
                    case '.': //.text
                        if ((char)stream.ReadByte() == 't' &&
                            (char)stream.ReadByte() == 'e' &&
                            (char)stream.ReadByte() == 'x' &&
                            (char)stream.ReadByte() == 't')
                        {
                            textIsFound = true;
                            break;
                        }
                        continue;
                    default:
                        break;
                }
                if (stream.Position == stream.Length - 1) return null;
            }

            stream.Position += 10; // stream to rawDataSize

            byte[] array = new byte[4];

            stream.Read(array, 0, 4);
            Array.Reverse(array, 0, array.Length);
            int rawDataSize = BitConverter.ToInt32(array, 0);

            stream.Read(array, 0, 4);
            Array.Reverse(array, 0, array.Length);
            int rawDataPosition = BitConverter.ToInt32(array, 0);

            array = new byte[rawDataSize];
            offset = rawDataPosition; // ref
            stream.Position = rawDataPosition;
            stream.Read(array, 0, rawDataSize - 1); // read to array
            stream.Close();
            return array;
        }

        private string getStringOfBytes(byte[] array, int index, int length)
        {
            return BitConverter.ToString(array, index, length).Replace("-", "");
        }

        private bool signatureOnFullMatch(VirusDS virus, byte[] data, int offset)
        {
            string signature = this.getStringOfBytes(data, offset, virus.signature.Length / 2);
            bool signatureInFrame = (offset + virus.signature.Length) < virus.offsetEnd;
            if (signature.Equals(virus.signature) && signatureInFrame)
                return true;
            return false;
        }

        // TODO: Check
        private void scanZipFileStream(Stream stream, string path)
        {
            stream.Position = 0;
            using (ZipArchive archive = new ZipArchive(stream))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    using (Stream reader = entry.Open())
                    {
                        this.isExecutableOrZip(path, reader);
                    }
                }
            }
        }
    }
}
