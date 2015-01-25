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
        public MainWindow()
        {
            InitializeComponent();
            //PicScraper.scrape();
        }

        private void SubredditsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            subredditRawText = SubredditsTextBox.Text;
        }

        private void GetSubredditPics_Click(object sender, RoutedEventArgs e)
        {
            if (subredditRawText != null)
            {
                subredditRawText = subredditRawText.Trim().Replace(" ", "");
                PicScraper.scrape(subredditRawText.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries));
            }
        }

        private void ChooseDir_Click(object sender, RoutedEventArgs e)
        {
            //Microsoft.Win32.OpenFileDialog browse = new Microsoft.Win32.OpenFileDialog();
            FolderBrowserDialog browse = new FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = browse.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string path = browse.SelectedPath;
                Debug.WriteLine(path);
            }
        }
    }
}
