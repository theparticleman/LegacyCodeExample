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
    }

    public interface IFileDependencies
    {
        IEnumerable<string> GetFilesInDirectory(string location, string searchPattern);
    }

    public interface IPictureViewerForm
    {
        string CurrentImageLocation { get; set; }
        string ImagesLocation { get; set; }
        string SelectedImage { get; set; }
    }
}
