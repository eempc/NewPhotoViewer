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
        /// Add string[] args to Main() then check it isn't empty and the file that triggered the app exists
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            if (args != null && args.Length > 0) { // Arguments found
                string fileName = args[0];
                if (File.Exists(fileName)) { // Check file exists
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    Form1 MainFrom = new Form1();
                    MainFrom.OpenFile(fileName);
                    Application.Run(MainFrom);
                } else { // If no file exists, launch app anyway
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
