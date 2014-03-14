using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Windows.Forms;

namespace PictureViewer
{
    public class FileDependencies : IFileDependencies
    {
        public IEnumerable<string> GetFilesInDirectory(string location, string searchPattern)
        {
            return Directory.GetFiles(location, searchPattern);
        }

        public string ExecutablePath
        {
            get { return Application.ExecutablePath; }
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public string ReadAllFileText(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}