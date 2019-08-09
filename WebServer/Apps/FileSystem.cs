using System.IO;

namespace WebServer
{
    public class FileServer
    {
        private string _rootDirectory;

        public FileServer(string rootDirectory)
        {
            _rootDirectory = rootDirectory;
        }

        public Stream ReadFile(string path)
        {

        }
    }
}