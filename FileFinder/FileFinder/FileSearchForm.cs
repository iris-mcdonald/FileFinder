﻿using System;
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

        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                string searchDirectory = searchPath.Text;
                string searchFileExt = fileExtension.Text;
                string searchString = searchFile.Text;
                resultsBox1.Text = "";

                bool inputValid = validateUserInput(searchDirectory, searchFileExt, searchString);
                if (inputValid)
                {
                    string searchFiles = "*" + fileExtension.Text;
                    //string[] fileResults = Directory.GetFiles(searchDirectory, searchFiles);
                    //added logic to search directories and all subdirectories:
                    string[] fileResults = Directory.GetFiles
                                (searchDirectory, searchFiles, SearchOption.AllDirectories);
                    if (fileResults.Count() == 0)
                    {
                        string logMsg = ($"No Files Found Matching Path: { searchDirectory}" +
                             $" with file extension: {searchFileExt} ");
                        writeLog(logMsg);
                        resultsBox1.Text = logMsg; 
                    }
                    else
                    {
                        int filesFound = 0;
                        foreach (string fileName in fileResults)
                        {
                            bool foundMatch = searchFileText(fileName, searchString);
                            if (foundMatch)
                            {
                                ++filesFound;
                                resultsBox1.AppendText(fileName + Environment.NewLine);
                            }
                        }
                        //if no files were found containing the search string, write log message;
                        if (filesFound == 0)
                        {
                            string logMsg = "No Files Found containing text: " + 
                                searchFile.Text + " on Path: " + searchDirectory +
                                " File extension: " + searchFileExt;
                            writeLog(logMsg);
                            resultsBox1.Text = logMsg;
                        }
                    }
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                string logMsg = ($"File path not Found  Exception Msg: {ex.Message}");
                writeLog(logMsg);
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                string logMsg = ($"Unexpected error:   Exception Msg: {ex.Message}");
                writeLog(logMsg);
            }
        }

        public void writeLog(string logMsg)
        {
            FileStream logFile = new FileStream("Log.txt", FileMode.Append, FileAccess.Write);
            StreamWriter swLog = new StreamWriter(logFile);
            DateTime now = DateTime.Now;
            swLog.WriteLine($"{now} {logMsg}");
            swLog.Close();
        }

        private bool validateUserInput(string searchDirectory, string searchFileExt, string searchString)
        {
            bool inputValid = true;
            if (searchDirectory == "")
                {
                    searchPath.Text = "Enter a Path to Search";
                    inputValid = false;
                }
            if (searchFileExt == "")
               {
                   fileExtension.Text = "Enter a File Extension";
                   inputValid = false;
               }
            if (searchString == "")
               {
                   searchFile.Text = "Enter Search Text";
                   inputValid = false;
               }
            return inputValid;
        }


        private bool searchFileText(string fileName, string searchString)
        {
            string searchLine;
            bool foundMatch = false;
            FileStream aFile = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(aFile);
            
            while (!sr.EndOfStream)
            {
                searchLine = sr.ReadLine();
                if (searchLine.ToUpper().Contains(searchString.ToUpper()))
                {
                    foundMatch = true;
                    break;
                }
            }
            sr.Close();
            return foundMatch;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            string logMsg = ("Clear Button Pressed; Form Cleared");
            writeLog(logMsg);
            searchPath.Text = "C:\\";
            searchFile.Text = "";
            fileExtension.Text = "";
            resultsBox1.Text = "";
            
        }

        private void useerFileExt_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void searchFile_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void searchFileForm_Load(object sender, EventArgs e)
        {
            string logMsg = "FileFinder Process Started";
            writeLog(logMsg);
        }
        
        //I attached an event named onExit(which doesn't appear on the form
        //to the form event section "form close" to get this code generated
        //It gets invoked when the user presses X to exit
        private void onExit(object sender, FormClosedEventArgs e)
        {
            string logMsg = "File Finder Process Ended";
            writeLog(logMsg);
        }
    }
}
