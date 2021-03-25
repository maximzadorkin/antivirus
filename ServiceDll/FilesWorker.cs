using System;
using System.IO;
using System.Linq;

namespace ServiceDll
{
    public class FilesWorker
    {
        public FilesWorker() { }

        static public bool addFileToQuarantine(string filePath)
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            byte[] qBytes = { (byte)'Q' };
            byte[] changedBytes = new byte[qBytes.Length + bytes.Length];
            qBytes.CopyTo(changedBytes, 0);
            bytes.CopyTo(changedBytes, qBytes.Length);
            File.WriteAllBytes(filePath, changedBytes);
            DataBase db = new DataBase();
            db.addToQuarantine(filePath);
            return true;
        }

        static public bool removeFileFromQuarantine(string filePath)
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            byte[] qBytes = { (byte)'Q' };
            byte[] changedBytes = new byte[bytes.Length - qBytes.Length];
            bytes.Skip(qBytes.Length).ToArray().CopyTo(changedBytes, 0);
            File.WriteAllBytes(filePath, changedBytes);
            DataBase db = new DataBase();
            db.removeFromQuarantine(filePath);
            return true;
        }

        static public bool deleteFile(string filePath)
        {
            try
            {
                File.Delete(filePath);
                DataBase db = new DataBase();
                return true;
            }
            catch (UnauthorizedAccessException _e)
            {
                return false;
            }
        }
    }
}