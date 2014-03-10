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

        public PictureViewerPresenter(IPictureViewerForm form)
        {
            this.form = form;
        }

        public void SelectedImageChanged()
        {
            //pictureBox.ImageLocation =
            //Directory.GetFiles(directoryTextBox.Text, "*.jpg").Single(x => x.EndsWith((string)imageListBox.SelectedItem));
            form.CurrentImageLocation =
            Directory.GetFiles(form.ImagesLocation, "*.jpg").Single(x => x.EndsWith(form.SelectedImage));
        }
    }

    public interface IPictureViewerForm
    {
        string CurrentImageLocation { get; set; }
        string ImagesLocation { get; set; }
        string SelectedImage { get; set; }
    }
}
