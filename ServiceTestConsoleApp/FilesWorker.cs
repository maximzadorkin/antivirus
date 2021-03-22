using System;
using System.IO;

namespace ServiceTestConsoleApp
{
    class FilesWorker
    {
        static public bool addFileToQuarantine(string filePath)
        {
            return true;
        }

        static public bool removeFileFromQuarantine(string filePath)
        {
            return true;
        }

        static public bool deleteFile(string filePath)
        {
            try
            {
                File.Delete(filePath);
                return true;
            }
            catch (UnauthorizedAccessException _e)
            {
                return false;
            }
        }
    }
}
