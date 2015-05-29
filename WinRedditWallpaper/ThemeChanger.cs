using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace WinRedditWallpaper
{
    public class ThemeChanger
    {
        private static string USER_PROFILE = "%userprofile%";
        public ThemeChanger()
        { }

        private string getOldTheme()
        {
            // NOTE: theme location is %userprofile%\AppData\Local\Microsoft\Windows\Themes
            string path = System.Environment.ExpandEnvironmentVariables(USER_PROFILE);
            path = Path.Combine(path, "AppData", "Local", "Microsoft", "Windows", "Themes");
            DirectoryInfo d = new DirectoryInfo(path); // get all of the files
            FileInfo[] files = d.GetFiles("*.theme"); //get the old theme file
            Debug.WriteLine(files[0].FullName);
            return files[0].FullName;
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
                    // then we're at the end of the slideshow section
                        atSlideShow = false;
                    }
                    else if (!atSlideShow)
                    // basically don't want to copy the line if atSlideShow && !line.Contains("")
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

        public void changeTheme(string pathToDir, int interval)
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
    }
}
