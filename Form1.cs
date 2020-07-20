using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Windows.Forms;

namespace JTFileLister
{
    //Jonathan Tregoiing
    //cv@Tregoiing.co.uk
    //074720 78720

    //requestedExecutionLevel = "requireAdministrator"
    //This has been requested in the app.manifest file
    public partial class Form1 : Form
    {
        //Our JSON object - Note, this can output CSV too.
        public class FileInfoForOutput
        {
            public string Name { get; set; }
            public string DirectoryName { get; set; }
            public DateTimeOffset CreationTimeUtc { get; set; }
            public DateTimeOffset LastWriteTimeUtc { get; set; }
        }

        //Global BW to allow the user to still control the app
        private BackgroundWorker backgroundWorker1 = new BackgroundWorker
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };

        //Some setup and constants - (Localisation not supported in V1.0)
        private int directoryCount = 0;
        private int fileCount = 0;
        private const string EarlyShutdown = "Please use the 'Stop' button when the scan is in progress";
        private const string ProblemClosingLogFile = "Problem encountered closing the log file";
        //WantJSON = false will produce a CSV format with header and footer row
        private const bool WantJSON = true; 

        //Our starting point - Ideally needs to be user selectable at runtime
        private string root = @"C:\";
        private FileStream logZipFile;
        private ZipArchive logZipFileArchive;
        private ZipArchiveEntry readmeEntry;
        private StreamWriter logStream;
        private JsonSerializerOptions options;

        private void InitAll()
        {
            directoryCount = 0;
            fileCount = 0;
            //Go and Stop buttons are only enabled if they make sense in the current state
            stopButton.Enabled = true;
            textBox1.Clear();
            WriteToStatusLog("Start");

            var path = @".\";
            var logFileName = @"Log";
            var logCounter = 1;
            var fileName = DateTime.Now.ToString("yyyyMMdd") + ".zip";
            //Logging to a zip archive
            logZipFile = new FileStream(path + fileName, FileMode.OpenOrCreate);
            {
                logZipFileArchive = new ZipArchive(logZipFile, ZipArchiveMode.Update);
                {
                    while (logZipFileArchive.GetEntry(logFileName + logCounter.ToString() + ".txt") != null)
                    {
                        logCounter++;
                    }
                    readmeEntry = logZipFileArchive.CreateEntry($"{logFileName}{ logCounter}.txt");
                    logStream = new StreamWriter(readmeEntry.Open());
                }
            }
            if (WantJSON)
                WriteToLog(@"{""Files"":[");
            else
                WriteToLog("Name,DirectoryName,CreationTimeUtc,LastWriteTimeUtc");
        }

        public Form1()
        {
            InitializeComponent();
            stopButton.Enabled = false;
            options = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                WriteIndented = true
            };

            //Set the events
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                //Let's start!
                InitAll();
                goButton.Enabled = false;
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            //Early termination requested
            stopButton.Enabled = false;
            if (backgroundWorker1.WorkerSupportsCancellation)
                backgroundWorker1.CancelAsync();
        }

        private void addTologFromThread(string logMessage)
        {
            //Threadsafe UI conduit 
            this.Invoke((MethodInvoker)delegate ()
            {
                WriteToLog(logMessage);
            });
        }
        private void addToStatusLogFromThread(string logMessage)
        {
            //Threadsafe UI conduit 
            this.Invoke((MethodInvoker)delegate ()
            {
                WriteToStatusLog(logMessage);
            });
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs args)
        {
            //The main event!
            //Scan through everything that we've got access to.
            //Running as a Windows Service would allow us to gave greater access
            //Check for errors as we go - log exceptions onto the screen - file info goes into the archive file
            var worker = sender as BackgroundWorker;
            //We're going to use a stack for this - This should allow better resource management for the potentially large number of folders
            var dirs = new Stack<string>(20);

            if (!System.IO.Directory.Exists(root))
                throw new ArgumentException();
            dirs.Push(root);

            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                string[] subDirs;
                this.Invoke((MethodInvoker)delegate ()
                {
                    //Request update to the current directory label
                    UpdateDirectoryInformation(currentDir);
                });
                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
                }
                catch (UnauthorizedAccessException e)
                {
                    //An error...
                    addToStatusLogFromThread(e.Message);
                    continue;
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    addToStatusLogFromThread(e.Message);
                    continue;
                }
                string[] files = null;
                try
                {
                    files = System.IO.Directory.GetFiles(currentDir);
                }
                catch (UnauthorizedAccessException e)
                {
                    addToStatusLogFromThread(e.Message);
                    continue;
                }

                catch (System.IO.DirectoryNotFoundException e)
                {
                    addToStatusLogFromThread(e.Message);
                    continue;
                }

                foreach (string file in files)
                {
                    //Was a 'Stop' requested?
                    if (backgroundWorker1.CancellationPending)
                    {
                        args.Cancel = true;
                        dirs.Clear();
                        break;
                    }
                    try
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {

                            System.IO.FileInfo fi = new System.IO.FileInfo(file);
                            //Request update to the current file label
                            UpdateFileInformation(fi.Name);

                            //Setup JSON object
                            var fileInfoForOutput = new FileInfoForOutput
                            {
                                Name = fi.Name,
                                DirectoryName = fi.DirectoryName,
                                CreationTimeUtc = fi.CreationTimeUtc,
                                LastWriteTimeUtc = fi.LastWriteTimeUtc
                            };
                            if (WantJSON)
                            {
                                //Dump out JSON object
                                if (fileCount > 1)
                                    addTologFromThread(",");
                                addTologFromThread(JsonSerializer.Serialize(fileInfoForOutput, options));
                            }
                            else
                            {
                                //Dump out CSV line
                                addTologFromThread(String.Format("{0},{1},{2},{3}", fi.Name, fi.DirectoryName, fi.CreationTimeUtc, fi.LastWriteTimeUtc));
                            }
                        });
                    }
                    catch (System.IO.FileNotFoundException e)
                    {
                        addToStatusLogFromThread(e.Message);
                        continue;
                    }
                }

                foreach (string str in subDirs)
                    dirs.Push(str);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                WriteToStatusLog("Canceled!");
            else if (e.Error != null)
                WriteToStatusLog("Error: " + e.Error.Message);
            else
            {
                WriteToStatusLog("Done!");
                if (!WantJSON)
                    WriteToLog("End of data");
            }
            //Properly end the JSON file
            if (WantJSON)
                WriteToLog("]}");
            CloseFile();
            goButton.Enabled = true;
        }
        private void UpdateDirectoryInformation(string directoryName)
        {
            directoryCount++;
            currDirLbl.Text = directoryName;
            dirCountLbl.Text = directoryCount.ToString();
        }
        private void UpdateFileInformation(string fileName)
        {
            fileCount++;
            currFileLbl.Text = fileName;
            fileCountLbl.Text = fileCount.ToString();
        }

        private void WriteToLog(string textToLog)
        {
            if (logStream != null)
                logStream.WriteLine(textToLog);
        }
        private void WriteToStatusLog(string textToLog)
        {
            //Update the on screen textbox
            textBox1.AppendText(textToLog + Environment.NewLine);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Click Stop then close the app didn't happen - remind the user!
            if (backgroundWorker1.IsBusy)
            {
                stopButton_Click(sender, e);
                e.Cancel = true;
                MessageBox.Show(EarlyShutdown, this.Text, MessageBoxButtons.OK);
            }
            CloseFile();
        }

        private void CloseFile()
        {
            try
            {
                //Shutdown the file 
                if (logStream != null)
                {
                    logStream.Close();
                    logStream.Dispose();
                    logZipFileArchive.Dispose();
                    logZipFile.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ProblemClosingLogFile + Environment.NewLine + ex.InnerException.Message, this.Text, MessageBoxButtons.OK);
            }
        }
    }
}
