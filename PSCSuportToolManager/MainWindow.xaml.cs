using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PSCSuportToolManager.FileHandler;
using PSCSuportToolManager.Package_Tracker;

namespace PSCSuportToolManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SupportFiles supportFiles = new SupportFiles();

        //Used for cycling through all the files in the directory
        int currentFileIndex = 0;

        public MainWindow()
        {
            
            InitializeComponent();
            this.DataContext = supportFiles;
            this.Topmost = true;

            MouseLeftButtonDown += new MouseButtonEventHandler(MouseLeftButtonDownPrevNext);
            MouseEnter += new MouseEventHandler(MouseEnterPrevNext);
            MouseLeave += new MouseEventHandler(MouseEnterPrevNext);

        }

        private string nextFile()
        {
            if (!(currentFileIndex >= supportFiles.FilePaths.Count - 1)) currentFileIndex++;

            //returns the next file in the directory
            return supportFiles.FilePaths[currentFileIndex];
        }
        private string previousFile()
        {
            if (!(currentFileIndex <= 0)) currentFileIndex--;

            //returns the previous file in the directory
            return supportFiles.FilePaths[currentFileIndex];
        }
        

        //Event handlers
        #region
        private void MouseEnterPrevNext(object type, MouseEventArgs e)
        {
            if (type is Label)
            {
                Label lbl = (Label)type;
                lbl.Foreground = Brushes.Black;
                return;
            }
         

            
        }
        private void MouseLeavePrevNext(object type, MouseEventArgs e)
        {
            if (type is Label)
            {
                Label lbl = (Label)type;
                lbl.Foreground = Brushes.Blue;
                return;
            }
           


        }
        private void MouseLeftButtonDownPrevNext(object type, MouseButtonEventArgs e)
        {
            if (type is Label)
            {
                Label lbl = (Label)type;

                switch (lbl.Name)
                {
                    case "lblNext":
                        supportFiles.AppSelected = nextFile();
                        break;
                    case "lblPrev":
                        supportFiles.AppSelected = previousFile();
                        break;
                }
                supportFiles.formatAndSetDisplayText();
            }
            
           
        }
        
        private void lblApplicationName_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(supportFiles.AppSelected);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to start: " + supportFiles.AppSelected + "\n" + "Error: " + ex.Message);
            }
        }

        private void txtSearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            var key = e.Key;

            if (key == Key.Enter)
            {
                string userInput = txtSearchBox.Text;

                if (userInput.Equals(""))
                {
                    supportFiles.AppSelectedDisplaytxt = "Nothing entered....";
                }
                else if(userInput.ToLowerInvariant().Equals("help"))
                {
                    MessageBox.Show("SPECIAL COMMANDS" + "\n" + "Tracking Fedex Package: Paste tracking number" + "\n" + "Property Passwords: proppass:(propertynumber)" +"\n" + "Press ENTER to execute commands", "Commands");
                }
                //Tracking
                else if (userInput.StartsWith("777"))
                {
                    long results = 0;
                    if (long.TryParse(txtSearchBox.Text.Replace(" ", ""), out results)) { Track.package(Convert.ToInt64(results)); }
                }
                //Property Passwords
                else if (txtSearchBox.Text.StartsWith("proppass:"))
                {
                    string property = txtSearchBox.Text.Substring(txtSearchBox.Text.IndexOf(":") + 1);
                    int result = 0;
                    if (!(int.TryParse(property, out result))) { supportFiles.AppSelectedDisplaytxt = "Incorrect Property Number"; return; }
                    supportFiles.AppSelectedDisplaytxt = supportFiles.propertyPassword(property);
                }
                //File searching
                else
                {
                    int index = supportFiles.FilePaths.FindIndex(x => x.ToLowerInvariant().Contains(txtSearchBox.Text.ToLowerInvariant()));
                   
                    if (index != -1)
                    {
                        supportFiles.AppSelected = supportFiles.FilePaths[index];
                        supportFiles.AppSelectedDisplaytxt = supportFiles.FilePaths[index];
                        supportFiles.formatAndSetDisplayText();
                    }
                }
            }
        }

         
        private void lblViewFiles_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnListView_MouseEnter(object sender, MouseEventArgs e)
        {

        }
    }
    #endregion
}







