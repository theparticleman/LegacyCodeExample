using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace PictureViewer.Tests
{
    [TestFixture]
    public class PictureViewerPresenterTests
    {
        private const string NewImagesLocation = @"C:\new images location";
        private FileDependenciesStub fileDependencies;
        private Mock<IPictureViewerForm> formStub;
        private PictureViewerPresenter presenter;

        [SetUp]
        public void SetUp()
        {
            fileDependencies = new FileDependenciesStub();
            formStub = new Mock<IPictureViewerForm>();
            presenter = new PictureViewerPresenter(formStub.Object, fileDependencies);
        }

        [Test]
        public void SelectedImageChangedShouldSetCurrentImageLocationToFullFilePath()
        {
            fileDependencies.ResultsForGetFilesFromDirectory = new List<string>
            {
                @"C:\file1.jpg",
                @"C:\file2.jpg"
            };
            formStub.Setup(x => x.ImagesLocation).Returns(@"C:\any made up directory");
            formStub.Setup(x => x.SelectedImage).Returns(@"file1.jpg");

            presenter.SelectedImageChanged();

            formStub.VerifySet(x => x.CurrentImageLocation = @"C:\file1.jpg");
        }

        [Test]
        public void UpdateImagesLocationShouldSetImagesLocationToNewImagesLocation()
        {
            formStub.Setup(x => x.ShouldUpdateImagesLocation).Returns(true);
            formStub.Setup(x => x.NewImagesLocation).Returns(NewImagesLocation);

            presenter.UpdateImagesLocation();

            formStub.VerifySet(x => x.ImagesLocation = NewImagesLocation);
        }

        [Test]
        public void InitializeShouldSetImagesLocation()
        {
            fileDependencies.ExecutablePath = @"C:\executable path\app.exe";
            fileDependencies.ResultForFileExists = true;
            fileDependencies.ResultsForReadAllFileText = NewImagesLocation;

            presenter.Initialize();

            formStub.VerifySet(x => x.ImagesLocation = NewImagesLocation);
        }

        [Test]
        public void ImagesLocationChangedShouldAddImagesToList()
        {
            fileDependencies.ResultsForDirectoryExists = true;
            fileDependencies.ResultsForGetFilesFromDirectory = new List<string>
            {
                @"C:\file1.jpg",
                @"C:\file2.jpg"
            };

            presenter.ImagesLocationChanged();

            formStub.Verify(x => x.AddImageToList("file1.jpg"));
        }

    }

    class FileDependenciesStub : IFileDependencies
    {
        //Writing everything for the file dependencies stub is starting to get a
        //little cumbersome. A mocking framework would eliminate the need to
        //write all this code manually.
        public IEnumerable<string> ResultsForGetFilesFromDirectory { get; set; }
        public bool ResultForFileExists { get; set; }
        public IEnumerable<string> GetFilesInDirectory(string location, string searchPattern)
        {
            return ResultsForGetFilesFromDirectory;
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

        public bool DirectoryExists(string path)
        {
            return ResultsForDirectoryExists;
        }

        public void WriteAllFileText(string filePath, string text)
        {
            //Don't do anything until our tests check for this.
        }

        public bool ResultsForDirectoryExists { get; set; }

        public string ResultsForReadAllFileText { get; set; }
    }
}
