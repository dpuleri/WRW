using System;

namespace WinRedditWallpaper
{
    public class ThemeChanger
    {
        public ThemeChanger()
        { }

        public void changeTheme()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C C:\\Users\\Daniel\\Desktop\\test.theme";
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
