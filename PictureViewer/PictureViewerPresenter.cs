using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureViewer
{
    public class PictureViewerPresenter
    {
        private string startDirectoryFilePath;
        private readonly IPictureViewerForm form;
        private readonly IFileDependencies fileDependencies;

        public PictureViewerPresenter(IPictureViewerForm form, IFileDependencies fileDependencies)
        {
            this.form = form;
            this.fileDependencies = fileDependencies;
        }

        public void SelectedImageChanged()
        {
            form.CurrentImageLocation = fileDependencies.GetFilesInDirectory(form.ImagesLocation, "*.jpg")
                .Single(x => x.EndsWith(form.SelectedImage));
        }

        public void UpdateImagesLocation()
        {
            if (form.ShouldUpdateImagesLocation)
            {
                form.ImagesLocation = form.NewImagesLocation;
            }
        }

        public void Initialize()
        {
            startDirectoryFilePath = Path.Combine(Path.GetDirectoryName(fileDependencies.ExecutablePath), "startpath.txt");
            if (fileDependencies.FileExists(startDirectoryFilePath))
            {
                form.ImagesLocation = fileDependencies.ReadAllFileText(startDirectoryFilePath);
            }
        }

        public void ImagesLocationChanged()
        {
            if (fileDependencies.DirectoryExists(form.ImagesLocation))
            {
                form.ClearImagesList();
                foreach (var file in fileDependencies.GetFilesInDirectory(form.ImagesLocation, "*.jpg"))
                {
                    form.AddImageToList(Path.GetFileName(file));
                }
                fileDependencies.WriteAllFileText(startDirectoryFilePath, form.ImagesLocation);
            }
        }
    }

    public interface IFileDependencies
    {
        IEnumerable<string> GetFilesInDirectory(string location, string searchPattern);
        string ExecutablePath { get; }
        bool FileExists(string path);
        string ReadAllFileText(string filePath);
        bool DirectoryExists(string path);
        void WriteAllFileText(string filePath, string text);
    }

    public interface IPictureViewerForm
    {
        string CurrentImageLocation { get; set; }
        string ImagesLocation { get; set; }
        string SelectedImage { get; set; }
        bool ShouldUpdateImagesLocation { get; }
        string NewImagesLocation { get; }
        void ClearImagesList();
        void AddImageToList(string imageName);
    }
}
