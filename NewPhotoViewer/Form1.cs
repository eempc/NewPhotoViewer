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
        }

        string[] imageExtensions = { ".jpg", ".png", ".jpeg", ".jpe", ".gif", ".bmp", ".tiff", ".tif" };        
        private Bitmap myImage;

        // Opening start method from Program.cs if an image file was opened directly
        public void StartMethod (string filePath) {
            ShowImage(filePath);
            EnumerateFiles(filePath);
        }

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
            }
        }

        // Make picture box show an image, perform an extension check beforehand
        string currentlyLoadedFile;
        int currentlyLoadedIndex;
        public void ShowImage(string filePath) {
            if (IsImage(filePath)) {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                myImage = new Bitmap(filePath);
                pictureBox1.Image = (Image) myImage;
                //currentlyLoadedFile = filePath;
                //currentlyLoadedIndex = imageFileList.IndexOf("filePath");
                //TextBoxDisplay(currentlyLoadedFile);
                //TextBoxDisplay(currentlyLoadedIndex.ToString());
                //TextBoxDisplay(imageFileList[currentlyLoadedIndex + 1]);
            }
        }

        // Image file check
        public bool IsImage(string filePath) {
            bool isImage = false;
            foreach (string s in imageExtensions) {
                if (Path.GetExtension(filePath) == s)
                    isImage = true;
            }
            return isImage;
        }

        // Enumerate files in directory
        List<string> imageFileList = new List<string>();
        public void EnumerateFiles(string filePath) {
           string[] allFiles = Directory.GetFiles(Path.GetDirectoryName(filePath), "*");
           foreach (string file in allFiles) {
                foreach (string s in imageExtensions) {
                    if (Path.GetExtension(file) == s) {
                        imageFileList.Add(file);
                        TextBoxDisplay(file);
                    }                   
                }
           } 
        }

        private void button1_Click(object sender, EventArgs e) {
            OpenImage();
        }

        public void TextBoxDisplay(string someString) {            
            textBox1.AppendText(DateTime.Now.ToString() + " " + someString + Environment.NewLine);
        }

        private void button2_Click(object sender, EventArgs e) {

        }
    }
}
