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
        private const string NewImagesLocation = @"C:\new images location";
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

        [Test]
        public void UpdateImagesLocationShouldSetImagesLocationToNewImagesLocation()
        {
            fileDependencies = new FileDependenciesStub();
            var formStub = new PictureViewerStub
            {
                ShouldUpdateImagesLocation = true,
                NewImagesLocation = NewImagesLocation
            };
            var presenter = new PictureViewerPresenter(formStub, fileDependencies);

            presenter.UpdateImagesLocation();

            Assert.That(formStub.ImagesLocation, Is.EqualTo(NewImagesLocation));
        }

        [Test]
        public void InitializeShouldSetImagesLocation()
        {
            fileDependencies = new FileDependenciesStub
            {
                ExecutablePath = @"C:\executable path\app.exe",
                ResultForFileExists = true,
                ResultsForReadAllFileText = NewImagesLocation
            };
            var formStub = new PictureViewerStub();
            var presenter = new PictureViewerPresenter(formStub, fileDependencies);

            presenter.Initialize();

            Assert.That(formStub.ImagesLocation, Is.EqualTo(NewImagesLocation));
        }

    }

    class PictureViewerStub : IPictureViewerForm
    {
        public string CurrentImageLocation { get; set; }
        public string ImagesLocation { get; set; }
        public string SelectedImage { get; set; }
        public bool ShouldUpdateImagesLocation { get; set; }
        public string NewImagesLocation { get; set; }
    }

    class FileDependenciesStub : IFileDependencies
    {
        //Writing everything for the file dependencies stub is starting to get a
        //little cumbersome. A mocking framework would eliminate the need to
        //write all this code manually.
        public IEnumerable<string> ResultsForGetFilesFromDirectoryEnumerable { get; set; }
        public bool ResultForFileExists { get; set; }
        public IEnumerable<string> GetFilesInDirectory(string location, string searchPattern)
        {
            return ResultsForGetFilesFromDirectoryEnumerable;
        }

        public string ExecutablePath { get; set; }
        public bool FileExists(string path)
        {
            return ResultForFileExists;
        }

        public string ReadAllFileText(string filePath)
        {
            return ResultsForReadAllFileText;
        }

        public string ResultsForReadAllFileText { get; set; }
    }
}
