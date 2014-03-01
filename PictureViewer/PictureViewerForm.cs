﻿using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PictureViewer
{
    public partial class PictureViewerForm : Form
    {
        private string startDirectoryFilePath;

        public PictureViewerForm()
        {
            InitializeComponent();
        }

        private void PictureViewerForm_Load(object sender, EventArgs e)
        {
            startDirectoryFilePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "startpath.txt");
            if (File.Exists(startDirectoryFilePath))
            {
                directoryTextBox.Text = File.ReadAllText(startDirectoryFilePath);
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                directoryTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void directoryTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(directoryTextBox.Text))
            {
                imageListBox.Items.Clear();
                foreach (var file in Directory.GetFiles(directoryTextBox.Text, "*.jpg"))
                {
                    imageListBox.Items.Add(Path.GetFileName(file));
                }
                File.WriteAllText(startDirectoryFilePath, directoryTextBox.Text);
            }
        }

        private void imageListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox.ImageLocation =
                Directory.GetFiles(directoryTextBox.Text, "*.jpg").Single(x => x.EndsWith((string)imageListBox.SelectedItem));
        }
    }
}