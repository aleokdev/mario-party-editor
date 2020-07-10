using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextTableEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var args = Environment.GetCommandLineArgs();
            string fileToEdit;
            if (args.Length > 1)
                fileToEdit = args[1];
            else
            {
                var dialog = new OpenFileDialog();
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                    fileToEdit = dialog.FileName;
                else
                    return;
            }
            Application.Run(new TextTableEditor(fileToEdit));
        }
    }
}
