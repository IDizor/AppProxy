using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
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
                var userFolder = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)).FullName;
                var appExe = Path.GetFileName(Application.ExecutablePath);
                var pathFile = userFolder + "\\" + appExe + ".path";
                //var logFile = userFolder + "\\" + appExe + ".log";

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
                        var commandArgs = Regex.Replace(Environment.CommandLine, @"^.+?\.exe""?\s*", "", RegexOptions.IgnoreCase);
                        //File.WriteAllText(logFile, commandArgs);

                        if (!String.IsNullOrEmpty(commandArgs))
                        {
                            if (!commandArgs.Contains("\""))
                            {
                                if (File.Exists(commandArgs))
                                {
                                    commandArgs = "\"" + commandArgs + "\"";
                                }
                            }
                        }

                        app.StartInfo.Arguments = commandArgs;
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
