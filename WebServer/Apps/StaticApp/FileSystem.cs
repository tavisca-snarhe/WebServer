using System;
using System.IO;

namespace WebServer
{
    public class FileSystem
    {
        private string _rootDirectory;

        public FileSystem(string rootDirectory)
        {
            _rootDirectory = rootDirectory;
        }

        private string ResolveVirtualPath(string virtualPath)
        {
            return _rootDirectory + virtualPath;
        }

        public string ReadFile(string virtualPath)
        {
            try
            {
                string filePath = ResolveVirtualPath(virtualPath);
                return File.ReadAllText(filePath);
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}