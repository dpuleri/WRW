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
using System.Windows.Forms;

using System.Diagnostics;

namespace WinRedditWallpaper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string subredditRawText;
        private PicScraper pic;
        public MainWindow(PicScraper pic)
        {
            InitializeComponent();
            this.pic = pic;
            //PicScraper.scrape();
        }

        private void SubredditsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            subredditRawText = SubredditsTextBox.Text;
            subredditRawText = subredditRawText.Trim().Replace(" ", "");
            string[] subreddits = subredditRawText.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            pic.subreddits = subreddits;
        }

        private void GetSubredditPics_Click(object sender, RoutedEventArgs e)
        {
            if (pic.subreddits != null)
            {
                pic.scrape();
            }
        }

        private void ChooseDir_Click(object sender, RoutedEventArgs e)
        {
            //Microsoft.Win32.OpenFileDialog browse = new Microsoft.Win32.OpenFileDialog();
            FolderBrowserDialog browse = new FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = browse.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                pic.path = browse.SelectedPath;
                Debug.WriteLine(pic.path);
            }
        }
    }
}
