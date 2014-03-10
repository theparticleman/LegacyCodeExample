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
            var presenter = new PictureViewerPresenter(null);

            presenter.SelectedImageChanged();
        }
    }
}
