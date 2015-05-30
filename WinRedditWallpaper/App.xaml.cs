using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using System.Diagnostics;

namespace WinRedditWallpaper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string path = WinRedditWallpaper.Properties.Settings.Default.DownloadDirectory;
            //System.Collections.Specialized.StringCollection
            string[] subreddits = new string[WinRedditWallpaper.Properties.Settings.Default.SubredditsToDownload.Count];
            WinRedditWallpaper.Properties.Settings.Default.SubredditsToDownload.CopyTo(subreddits, 0);

            Debug.WriteLine(path);
            foreach (string sub in subreddits)
            {
                Debug.WriteLine(sub);
            }
            Debug.WriteLine(subreddits.Length);
            Debug.WriteLine(subreddits == null);

            if (true)
            {
                MainWindow mainWindow = new MainWindow(new PicScraper(subreddits, path));
                if (path != "none")
                {
                    mainWindow.updatePathText(path);
                }

                //WallpaperChanger.setDesktopWallpaper(@"C:\\Users\\Daniel\\Desktop\\tmp2");
                //Debug.WriteLine(WallpaperChanger.getDesktopWallpaper());
                mainWindow.Show();
            }
            else
            {
                Debug.WriteLine("wohoo, execute other code");
                this.Shutdown();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}
