namespace ServiceTestConsoleApp
{
    class FileDS
    {
        public string path;
        public bool danger;
        public enum FilesHandler {
            Allow,
            ToQuarantine,
            Delete
        }
        public FilesHandler fileHandler;

        public FileDS(string path)
        {
            this.path = path;
            this.fileHandler = FilesHandler.Allow;
        }
    }
}
