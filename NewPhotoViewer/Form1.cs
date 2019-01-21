using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace NewPhotoViewer {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
        }

        string[] imageExtensions = { ".jpg", ".png", ".jpeg", ".jpe", ".gif", ".bmp", ".tiff", ".tif" };        
        private Bitmap currentImage;

        // Opening start method from Program.cs if an image file was opened directly        
        int currentlyLoadedIndex;
        public void StartMethod (string filePath) {
            ShowImage(filePath);
            EnumerateFiles(filePath);
        }

        // Enumerate files in directory
        List<string> imageFileList = new List<string>();
        public void EnumerateFiles(string filePath) {
            string[] allFiles = Directory.GetFiles(Path.GetDirectoryName(filePath));
            foreach (string file in allFiles) {
                if (IsImage(file)) 
                    imageFileList.Add(file);               
            }
            currentlyLoadedIndex = imageFileList.IndexOf(filePath);
        }

        // Do not use until files have been re-enumerated
        public void OpenImage() {
            // Create a string for the file dialogue filter based on the declared string array
            string filterString = "Image files|";
            foreach (string s in imageExtensions) {
                filterString += "*" + s + ";"; 
            }
            filterString = filterString.Remove(filterString.Length - 1); // Remove last char

            // Open a file manually with the button
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.Filter = filterString;
            if (openFD.ShowDialog() == DialogResult.OK) {
                ShowImage(openFD.FileName);
                EnumerateFiles(openFD.FileName);
            }
        }

        // Make picture box show an image, perform an extension check beforehand
        string currentlyLoadedFile;
        public void ShowImage(string filePath) {
            if (IsImage(filePath)) {
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                currentImage = new Bitmap(filePath);
                pictureBox1.Image = (Image) currentImage;
                currentlyLoadedFile = filePath;
                currentlyLoadedIndex = imageFileList.IndexOf(filePath);
                currentZoomFactor = 1;
            }
        }

        // Image file check
        public bool IsImage(string filePath) {
            bool isImage = false;
            foreach (string s in imageExtensions) {
                if (Path.GetExtension(filePath) == s) {
                    isImage = true;
                    break;
                }
            }
            return isImage;
        }

        private void button1_Click(object sender, EventArgs e) {
            OpenImage();
        }

        // Diagnostic text box
        public void TextBoxDisplay(string someString) {            
            textBox1.AppendText(DateTime.Now.ToString() + " - " + someString + Environment.NewLine);
        }

        // Next pic
        private void button2_Click(object sender, EventArgs e) {
            ShowImage(imageFileList[(currentlyLoadedIndex + 1) % imageFileList.Count]); // Formula for rollover at > Count
        }

        // Previous pic
        private void button3_Click(object sender, EventArgs e) {
            ShowImage(imageFileList[(currentlyLoadedIndex + (imageFileList.Count - 1)) % imageFileList.Count]); // Formula for rollover at <0
        }

        double currentZoomFactor = 1;
        double zoomStep = 0.1;
        private void buttonZoomIn_Click(object sender, EventArgs e) {
            if (currentZoomFactor < 2) Zoom(1);
        }

        private void buttonZoomOut_Click(object sender, EventArgs e) {
            if (currentZoomFactor > 0.1) Zoom(-1);
        }

        public void Zoom(int x) {
            Size newSize = new Size(
                (int) (currentImage.Width * (currentZoomFactor + zoomStep * x)), 
                (int) (currentImage.Height * (currentZoomFactor + zoomStep * x))
            );
            Bitmap bmp = new Bitmap(currentImage, newSize);
            pictureBox1.Image = bmp;
            currentZoomFactor += zoomStep * x;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenImage();
        }
    }
}
