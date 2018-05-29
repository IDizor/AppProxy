using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AppProxy
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            try
            {
                var pathFile = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)).FullName
                    + "\\"
                    + Path.GetFileName(Application.ExecutablePath)
                    + ".path";

                if (!File.Exists(pathFile))
                {
                    File.WriteAllText(pathFile, "C:\\program.exe");
                }

                Process app = new Process();
                app.StartInfo.FileName = File.ReadAllText(pathFile).Trim();

                if (File.Exists(app.StartInfo.FileName))
                {
                    if (args.Length > 0)
                    {
                        app.StartInfo.Arguments = string.Join(" ", args);
                    }

                    app.Start();
                }
                else
                {
                    ShowMessage("File not found: " + app.StartInfo.FileName);
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.ToString());
            }
        }

        private static void ShowMessage(string message)
        {
            MessageBox.Show(message, Application.ProductName);
        }
    }
}
