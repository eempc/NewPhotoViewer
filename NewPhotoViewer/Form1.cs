using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NewPhotoViewer {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        string[] imageFiles = { ".jpg", ".png", ".jpeg", ".jpe", ".gif", ".bmp" };        
        private Bitmap myImage;

        // Opening start method from Program.cs if an image file was opened directly
        public void StartMethod (string filePath) {
            ShowImage(filePath);
        }

        public void OpenImage() {
            // Create a string for the file dialogue filter based on the declared string array
            string filterString = "Image files|";
            foreach (string s in imageFiles) {
                filterString += "*" + s + ";"; 
            }
            filterString = filterString.Remove(filterString.Length - 1);

            // Open a file manually with the button
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.Filter = filterString;
            if (openFD.ShowDialog() == DialogResult.OK) {
                ShowImage(openFD.FileName);
            }
        }

        // Make picture box show an image, perform an extension beforehand
        public void ShowImage(string filePath) {
            foreach (string s in imageFiles) {
                if (Path.GetExtension(filePath) == s) {
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    myImage = new Bitmap(filePath);
                    pictureBox1.Image = (Image) myImage;
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            OpenImage();
        }
    }
}
