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
            if ((pic.subreddits != null && pic.subreddits.Length != 0) &&
                (!pic.path.Equals("none") && pic.path != null))
            {
                pic.scrape();
            }
            else
            {
                //show dialog box telling user to pic a path or write subreddits
                MessageBoxResult result = System.Windows.MessageBox.Show("Please choose a folder and/or subreddits", "More Information Needed Error"); 
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
                updatePathText(pic.path);
                Debug.WriteLine(pic.path);
            }
        }

        public void updatePathText(string text)
        {
            PathToDirectory.Text = text;
        }

        private void ChangeTheme_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(pic.path);
            if (!pic.path.Equals("none") && pic.path != null)
            // note that the path defaults to none because obj is instantiated from settings
            {
                ThemeChanger tc = new ThemeChanger();
                tc.changeTheme(pic.path, 600000);
            }
            else
            {
                //show dialog box telling user to pic a path
                MessageBoxResult result = System.Windows.MessageBox.Show("Please choose a folder to download files into and change the theme", "No Folder Entered Error"); 
            }
        }
    }
}
