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
        private FileDependenciesStub fileDependencies;

        [Test]
        public void SelectedImageChangedShouldSetCurrentImageLocationToFullFilePath()
        {
            fileDependencies = new FileDependenciesStub
            {
                ResultsForGetFilesFromDirectoryEnumerable = new List<string> { @"C:\file1.jpg", @"C:\file2.jpg" }
            };
            var formStub = new PictureViewerStub
            {
                ImagesLocation = @"C:\any made up directory", 
                SelectedImage = @"file1.jpg"
            };
            var presenter = new PictureViewerPresenter(formStub, fileDependencies);

            presenter.SelectedImageChanged();

            Assert.That(formStub.CurrentImageLocation, Is.EqualTo(@"C:\file1.jpg"));
        }
    }

    class PictureViewerStub : IPictureViewerForm
    {
        public string CurrentImageLocation { get; set; }
        public string ImagesLocation { get; set; }
        public string SelectedImage { get; set; }
    }

    class FileDependenciesStub : IFileDependencies
    {
        public IEnumerable<string> ResultsForGetFilesFromDirectoryEnumerable { get; set; }
        public IEnumerable<string> GetFilesInDirectory(string location, string searchPattern)
        {
            return ResultsForGetFilesFromDirectoryEnumerable;
        }
    }
}
