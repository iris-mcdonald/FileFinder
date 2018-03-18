using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace FileFinder
{
    public partial class searchFileForm : Form
    {
        public searchFileForm()
        {
            InitializeComponent();
        }

        public void WriteLog (string logMsg)
        {
            FileStream logFile = new FileStream("Log.txt", FileMode.Append, FileAccess.Write);
            StreamWriter swLog = new StreamWriter(logFile);
            DateTime now = DateTime.Now;
            swLog.WriteLine($"{now} {logMsg}");
                swLog.Close();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                string searchDirectory = searchPath.Text;
                string searchFileName = searchFile.Text;
                string searchFileExt = fileExtension.Text;
                string searchFiles = "*" + searchFileName + "*" + searchFileExt;
                string[] fileResults = Directory.GetFiles(searchDirectory, searchFiles);
                if (fileResults.Count() == 0)
                {
                    string logMsg = "No Files Found Matching Search Specified";
                    WriteLog(logMsg);
                    textBox1.Text = "No Files Found Matching Search Specified";
                }
                else
                {
                    foreach (string fileName in fileResults)
                    {
                        //Console.WriteLine(fileName);
                        textBox1.AppendText(fileName + Environment.NewLine);
                    }
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show("No Files Found", "File Search",
                MessageBoxButtons.OK, MessageBoxIcon.Error); 
                //Console.WriteLine(ex.Message);
                string logMsg = ($"File not Found  Exception Msg: {ex.Message}");
                WriteLog(logMsg);  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Unexpected Error", "File Search",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            string logMsg = ("Clear Button Pressed; Form Cleared");
            WriteLog(logMsg);
            searchPath.Text = "C:\\Users\\CC5 STUDENT\\";
            searchFile.Text = " ";
            fileExtension.Text = "(i.e. .XML, .TXT, .JSON, .CONFIG)";
            textBox1.Text = " ";
            
        }

        //I attached a button1(which doesn't appear on the form
        //to the form event section "form close" to get this code generated
        //It gets invoked when the user presses X to exit
        private void button1(object sender, FormClosedEventArgs e)
        {
            string logMsg = ("File Finder Process Ended");
            WriteLog(logMsg);
        }
                
    }
}
