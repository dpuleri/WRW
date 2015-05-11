using System;
using System.Runtime.InteropServices;

namespace WinRedditWallpaper
{
    static class WallpaperChanger
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction,
            int uParam, string lpvParam, int fuWinIni);

        private static readonly int MAX_PATH = 260;
        private static readonly int SPI_GETDESKWALLPAPER = 0x73;
        private static readonly int SPI_SETDESKWALLPAPER = 0x14;
        private static readonly int SPIF_UPDATEINIFILE = 0x01;
        private static readonly int SPIF_SENDWININICHANGE = 0x02;

        public static string getDesktopWallpaper()
        {
            string wallpaper = new string('\0', MAX_PATH);
            SystemParametersInfo(SPI_GETDESKWALLPAPER, (int)wallpaper.Length, wallpaper, 0);
            return wallpaper.Substring(0, wallpaper.IndexOf('\0'));
        }

        public static void setDesktopWallpaper(string filename)
        {
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, filename,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
    }
}
        