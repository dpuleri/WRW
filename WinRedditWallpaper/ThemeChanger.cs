using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;

namespace WinRedditWallpaper
{
    public class ThemeChanger
    {
        private static string USER_PROFILE = "%userprofile%";
        private static string TASK_NAME = @"WRW Wallpaper downloader";
        private string pathToDir;

        public ThemeChanger(string pathToDir)
        {
            this.pathToDir = pathToDir;
        }

        private string getOldTheme()
        {
            // NOTE: theme location is %userprofile%\AppData\Local\Microsoft\Windows\Themes
            string path = System.Environment.ExpandEnvironmentVariables(USER_PROFILE);
            path = Path.Combine(path, "AppData", "Local", "Microsoft", "Windows", "Themes");
            DirectoryInfo d = new DirectoryInfo(path); // get all of the files
            FileInfo[] files = d.GetFiles("*.theme"); //get the old theme file
            Debug.WriteLine(files[0].FullName);
            // Before we return the old path let's store the old theme
            string sourceFile = files[0].FullName;
            //only do it if it's NOT a WRW theme
            if (!files[0].Name.StartsWith("WRW"))
            {
                string destFile = Path.Combine(pathToDir, @"OldTheme.theme");
                System.IO.File.Copy(sourceFile, destFile, false); // do not overwrite if already exists
            }
            return sourceFile;
        }

        //returns full path to new theme so that the theme can be changed on the fly
        private string createNewThemeFile(string pathToOldTheme, string pathToDirNewTheme, int interval)
        {

            //read the lines from the old theme file:
            string[] oldLines = System.IO.File.ReadAllLines(pathToOldTheme);
            List<string> newLines = new List<string>();
            newLines.Add(@"[Slideshow]");
            newLines.Add(@"Interval=" + interval);
            newLines.Add(@"Shuffle=1");
            newLines.Add(@"ImagesRootPath=" + pathToDirNewTheme);
            string newPath = Path.Combine(pathToDirNewTheme, @"WRW.theme");

            // The using statement automatically closes the stream and calls  
            // IDisposable.Dispose on the stream object.
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(newPath))
            {
                bool atSlideShow = false;
                foreach (string line in oldLines)
                {
                    // If the line contains Slideshow then take that out
                    if (line.Contains(@"[Slideshow]"))
                    {
                        atSlideShow = true;
                    }
                    else if (atSlideShow && line.Contains("[")) {
                    // then we're at the end of the slideshow section stop
                        atSlideShow = false;
                    }
                    else if (!atSlideShow)
                    // basically don't want to copy the line if atSlideShow && !line.Contains("")
                    // but otherwise it's good to copy the line to the new theme
                    {
                        file.WriteLine(line);
                    }
                }
                foreach(string line in newLines)
                {
                    file.WriteLine(line);
                }
            }

            return newPath;
        }

        public void changeTheme(int interval)
        {
            //interval is the time between changes in miliseconds
            //pathToDir is the path to the directory where the pictures will be stored
            //the created theme will be stored there too
            string pathToOldTheme = getOldTheme();
            string pathToNewTheme = createNewThemeFile(pathToOldTheme, pathToDir, interval);
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C " + pathToNewTheme;
            process.StartInfo = startInfo;
            process.Start();
        }

        public void setTask()
        {
            // Get the service on the local machine
            using (TaskService ts = new TaskService())
            {
                // Create a new task definition and assign properties
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "WRW Wallpaper Downloader";

                DailyTrigger dt = new DailyTrigger();
                dt.StartBoundary = System.DateTime.Today + System.TimeSpan.FromHours(5) +
                                    System.TimeSpan.FromMinutes(12);
                dt.DaysInterval = 1; // have it go every day

                // Create a trigger that will fire the task at this time every other day
                td.Triggers.Add(dt);

                // Create an action that will launch Notepad whenever the trigger fires
                td.Actions.Add(new ExecAction("notepad.exe", "c:\\test.log", null));

                // Register the task in the root folder
                ts.RootFolder.RegisterTaskDefinition(TASK_NAME, td);

                // Remove the task
                //ts.RootFolder.DeleteTask("Test");
            }
        }

        public void removeTask()
        {
            // Get the service on the local machine
            using (TaskService ts = new TaskService())
            {
                // Remove the task
                ts.RootFolder.DeleteTask(TASK_NAME);
            }
        }
    }
}
