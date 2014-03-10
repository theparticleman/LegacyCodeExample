using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PictureViewer.Tests
{
    [TestFixture]
    public class PictureViewerPresenterTests
    {
        [Test]
        public void SelectedImageChangedShouldDoSomething()
        {
            var formStub = new PictureViewerStub
            {
                ImagesLocation = @"C:\Users\jonathan-turner\Pictures\Wallpapers", //This has to be a real directory on disk.
                SelectedImage = @"137801.jpg" //This has to be the name of a real file in that directory.
            };
            var presenter = new PictureViewerPresenter(formStub);

            presenter.SelectedImageChanged();
        }
    }

    class PictureViewerStub : IPictureViewerForm
    {
        public string CurrentImageLocation { get; set; }
        public string ImagesLocation { get; set; }
        public string SelectedImage { get; set; }
    }
}
