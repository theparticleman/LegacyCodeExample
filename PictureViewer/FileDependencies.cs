using System.Collections.Generic;
using System.IO;

namespace PictureViewer
{
    public class FileDependencies : IFileDependencies
    {
        public IEnumerable<string> GetFilesInDirectory(string location, string searchPattern)
        {
            return Directory.GetFiles(location, searchPattern);
        }
    }
}