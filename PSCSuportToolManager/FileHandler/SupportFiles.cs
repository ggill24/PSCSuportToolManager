using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.IO;
namespace PSCSuportToolManager.FileHandler
{
    partial class SupportFiles
    {
        public const string DirectoryPath = @"C:\PscSupport";
        //Folder containing support tools
        public const string SupportFolderPath = @"C:\PscSupport\Tools";
        //Paths of applications
        public List<string> FilePaths { get; set; }

        public SupportFiles()
        {
            try
            {

                if (!Directory.Exists(DirectoryPath))
                {
                    Directory.CreateDirectory(DirectoryPath);
                   

                }
                if (!Directory.Exists(SupportFolderPath))
                {
                    Directory.CreateDirectory(SupportFolderPath);

                }

            }

            catch (DirectoryNotFoundException diNtFndEx)
            {
               MessageBox.Show(diNtFndEx.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

            if (Directory.GetFiles(SupportFolderPath).Count() > 0)
            {
                FilePaths = FilesInDirectory(SupportFolderPath);
                AppSelected = FilePaths[0];
                formatAndSetDisplayText();
            }
            else
            {

                var result = System.Windows.MessageBox.Show("Place your support tools in: " + SupportFolderPath, "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                if (result == MessageBoxResult.OK) Application.Current.Shutdown(); System.Diagnostics.Process.Start(SupportFolderPath);
            }
        }

        public void formatAndSetDisplayText()
        {
            string trimmedFront = AppSelected.Substring(AppSelected.LastIndexOf('\\'), AppSelected.Length - AppSelected.LastIndexOf('\\')).Replace('\\', ' ');
            string trimmedBack = trimmedFront.Substring(0, trimmedFront.LastIndexOf('.'));
            AppSelectedDisplaytxt = trimmedBack;
        }

        public List<string> FilesInDirectory(string path)
        {
            return Directory.GetFiles(path).Where(x => !x.Contains("desktop.ini")).ToList();
        }
    }
}




/*public string DirectoryPath()
{
    System.Windows.MessageBox.Show("Please point to the directory that contains all your support tools", "Setup", MessageBoxButton.OK);

    string directoryPath = "";

    using (var folderBrowserDialog = new FolderBrowserDialog())
    {
        int count = 0;

        while (String.IsNullOrEmpty(directoryPath))
        {
            folderBrowserDialog.ShowDialog();
            directoryPath = folderBrowserDialog.SelectedPath;
            count++;
            if (count >= 5) break;
        }
    }

    return directoryPath;
}*/






