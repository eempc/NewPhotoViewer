using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NewPhotoViewer {
    static class Program {
        /// <summary>
        /// The main entry point for the application. Where you can load files by double clicking them.
        /// Add string[] args to Main() then check it isn't empty and the file that triggered the app exists. I'm guessing the string refers to multiple files
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            if (args != null && args.Length > 0) { // Arguments found
                string fileName = args[0]; // Picking the first file if multiple files are selected?
                if (File.Exists(fileName)) { // Check file exists
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    // This is just how it works, this piece of code will do startup methods if launched externally
                    Form1 MainForm = new Form1();
                    MainForm.StartMethod(fileName); // StartMethod is run if the app was launched by clicking a file
                    Application.Run(MainForm);
                } else { // If no file exists, launch app anyway, this bit is probably redundant.
                    MessageBox.Show("No file exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                }
            } else { // No arguments found
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }
    }
}
