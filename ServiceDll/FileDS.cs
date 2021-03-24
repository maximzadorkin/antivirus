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
            RemoveFromQuarantine,
            Delete
        }
        public FilesHandler fileHandler;

        public FileDS() { }
        public FileDS(string path)
        {
            this.isChecked = false;
            this.path = path;
            this.fileHandler = FilesHandler.Allow;
        }
    }
}
