namespace ServiceDll
{
    public class FileDS
    {
        public string path;
        public bool danger;
        public bool isChecked;
        public enum FilesHandler {
            Allow,
            ToQuarantine,
            Delete
        }
        public FilesHandler fileHandler;

        public FileDS(string path)
        {
            this.isChecked = false;
            this.path = path;
            this.fileHandler = FilesHandler.Allow;
        }
    }
}
