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
            string path = System.Environment.ExpandEnvironmentVariables(USER_PROFILE);
            path = Path.Combine(path, "AppData", "Local", "Microsoft", "Windows", "Themes");
            DirectoryInfo d = new DirectoryInfo(path); // get all of the files
            FileInfo[] files = d.GetFiles("*.theme"); //get the old theme file
            Debug.WriteLine(files[0].FullName);
            return files[0].FullName;
        }

        private string createNewThemeFile(string pathToOldTheme, string pathToDirNewTheme, int interval)
        {

            //read the lines from the old theme file:
            string[] oldLines = System.IO.File.ReadAllLines(pathToOldTheme);
            List<string> newLines = new List<string>();
            newLines.Add(@"[Slideshow]");
            newLines.Add(@"Interval=" + interval);
            newLines.Add(@"Shuffle=1");
            newLines.Add(@"ImagesRootPath=" + pathToDirNewTheme);

            // The using statement automatically closes the stream and calls  
            // IDisposable.Dispose on the stream object.
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Public\TestFolder\WriteLines2.txt"))
            {
                bool atSlideShow = false;
                foreach (string line in oldLines)
                {
                    // If the line doesn't contain the word 'Second', write the line to the file. 
                    if (line.Contains(@"[Slideshow]"))
                    {
                        atSlideShow = true;
                    }
                }
            }
        }

        public void changeTheme(string pathToDir, int interval)
        {
            //interval is the time between changes in miliseconds
            if (pathToDir == null)
            {
                MessageBox.Show("Please choose a download directory", "No Folder Entered", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            string pathToOldTheme = getOldTheme();
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C C:\\Users\\Daniel\\Desktop\\test.theme";
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
