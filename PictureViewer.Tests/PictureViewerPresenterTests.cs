﻿using System;
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
        private PictureViewerStub formStub;
        private PictureViewerPresenter presenter;

        [SetUp]
        public void SetUp()
        {
            fileDependencies = new FileDependenciesStub();
            formStub = new PictureViewerStub();
            presenter = new PictureViewerPresenter(formStub, fileDependencies);
        }

        [Test]
        public void SelectedImageChangedShouldSetCurrentImageLocationToFullFilePath()
        {
            fileDependencies.ResultsForGetFilesFromDirectoryEnumerable = new List<string>
            {
                @"C:\file1.jpg",
                @"C:\file2.jpg"
            };
            formStub.ImagesLocation = @"C:\any made up directory";
            formStub.SelectedImage = @"file1.jpg";

            presenter.SelectedImageChanged();

            Assert.That(formStub.CurrentImageLocation, Is.EqualTo(@"C:\file1.jpg"));
        }

        [Test]
        public void UpdateImagesLocationShouldSetImagesLocationToNewImagesLocation()
        {
            formStub.ShouldUpdateImagesLocation = true;
            formStub.NewImagesLocation = NewImagesLocation;

            presenter.UpdateImagesLocation();

            Assert.That(formStub.ImagesLocation, Is.EqualTo(NewImagesLocation));
        }

        [Test]
        public void InitializeShouldSetImagesLocation()
        {
            fileDependencies.ExecutablePath = @"C:\executable path\app.exe";
            fileDependencies.ResultForFileExists = true;
            fileDependencies.ResultsForReadAllFileText = NewImagesLocation;

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
