using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PictureViewer
{
    public partial class PictureViewerForm : Form, IPictureViewerForm
    {
        private readonly PictureViewerPresenter presenter;

        public PictureViewerForm()
        {
            InitializeComponent();
            presenter = new PictureViewerPresenter(this, new FileDependencies());
        }

        private void PictureViewerForm_Load(object sender, EventArgs e)
        {
            presenter.Initialize();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            presenter.UpdateImagesLocation();
        }

        private void directoryTextBox_TextChanged(object sender, EventArgs e)
        {
           presenter.ImagesLocationChanged();
        }

        private void imageListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            presenter.SelectedImageChanged();
        }

        public string CurrentImageLocation
        {
            get { return pictureBox.ImageLocation; }
            set { pictureBox.ImageLocation = value; }
        }

        public string ImagesLocation
        {
            get { return directoryTextBox.Text; }
            set { directoryTextBox.Text = value; }
        }

        public string SelectedImage
        {
            get { return (string)imageListBox.SelectedItem; }
            set { imageListBox.SelectedItem = value; }
        }

        public bool ShouldUpdateImagesLocation
        {
            get { return folderBrowserDialog.ShowDialog() == DialogResult.OK; }
        }

        public string NewImagesLocation
        {
            get { return folderBrowserDialog.SelectedPath; }
        }

        public void ClearImagesList()
        {
            imageListBox.Items.Clear();
        }

        public void AddImageToList(string imageName)
        {
            imageListBox.Items.Add(imageName);
        }
    }
}
