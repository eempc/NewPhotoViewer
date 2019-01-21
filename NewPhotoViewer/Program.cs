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
            if (args != null && args.Length > 0) { // Arguments found then check file 1 of the arguments exists              
                if (File.Exists(args[0]))
                    StartFormWithArgument(args[0]);
                else
                    StartFormWithoutArgument();
            } else {
                StartFormWithoutArgument();
            }
        }

        // This is just how it works, this piece of code will do startup methods if launched externally
        public static void StartFormWithArgument(string fileName) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 MainForm = new Form1();
            MainForm.StartMethod(fileName);
            Application.Run(MainForm);
        }

        public static void StartFormWithoutArgument() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
