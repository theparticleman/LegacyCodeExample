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
        private Mock<IFileDependencies> fileDependencies;
        private Mock<IPictureViewerForm> formStub;
        private PictureViewerPresenter presenter;

        [SetUp]
        public void SetUp()
        {
            fileDependencies = new Mock<IFileDependencies>();
            formStub = new Mock<IPictureViewerForm>();
            presenter = new PictureViewerPresenter(formStub.Object, fileDependencies.Object);
        }

        [Test]
        public void SelectedImageChangedShouldSetCurrentImageLocationToFullFilePath()
        {
            fileDependencies.Setup(x => x.GetFilesInDirectory(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<string>
                {
                    @"C:\file1.jpg",
                    @"C:\file2.jpg"
                });
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
            fileDependencies.Setup(x => x.ExecutablePath).Returns(@"C:\executable path\app.exe");
            fileDependencies.Setup(x => x.FileExists(It.IsAny<string>())).Returns(true);
            fileDependencies.Setup(x => x.ReadAllFileText(It.IsAny<string>())).Returns(NewImagesLocation);

            presenter.Initialize();

            formStub.VerifySet(x => x.ImagesLocation = NewImagesLocation);
        }

        [Test]
        public void ImagesLocationChangedShouldAddImagesToList()
        {
            fileDependencies.Setup(x => x.DirectoryExists(It.IsAny<string>())).Returns(true);
            fileDependencies.Setup(x => x.GetFilesInDirectory(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<string>
                {
                    @"C:\file1.jpg",
                    @"C:\file2.jpg"
                });

            presenter.ImagesLocationChanged();

            formStub.Verify(x => x.AddImageToList("file1.jpg"));
        }

    }
}
