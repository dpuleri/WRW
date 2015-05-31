using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace WinRedditWallpaper
{
    public partial class TimePickerWindow : Window
    {
        private DailyTriggerTime dtt;
        private MainWindow prevWindow;

        public TimePickerWindow(MainWindow prevWindow)
        {
            this.prevWindow = prevWindow;
            dtt = new DailyTriggerTime();
            InitializeComponent();
            // Add the options to the dropdowns
            for (int i = 1; i < 13; i++)
            {
                Hour_Dropdown.Items.Add(i);
            }
            for (int i = 0; i <= 60; i += 15)
            {
                Minute_Dropdown.Items.Add(i);
            }
            AMPM_Dropdown.Items.Add("AM");
            AMPM_Dropdown.Items.Add("PM");
            // set default values (7:00 pm)
            Hour_Dropdown.SelectedIndex = 6;
            Minute_Dropdown.SelectedIndex = 0;
            AMPM_Dropdown.SelectedIndex = 1;
            this.dtt.hour = 7 + 12; //24 hour time
            this.dtt.minute = 0;
        }

        private void Hour_Dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setHour();
        }

        private void AMPM_Dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setHour();
        }

        private void setHour()
        {
            int hour = 0;
            Int32.TryParse(Hour_Dropdown.SelectedItem.ToString(), out hour);
            Debug.WriteLine(hour);
            if (AMPM_Dropdown.SelectedIndex == 1)
            {
                if (hour != 12)
                {
                    hour = (hour + 12) % 24;
                }
            }
            else if (hour == 12)
            {
                hour = (hour + 12) % 24;
            }
            Debug.WriteLine(hour);
            this.dtt.hour = hour;
        }

        private void Minute_Dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int minute = 0;
            Int32.TryParse(Minute_Dropdown.SelectedItem.ToString(), out minute);
            this.dtt.minute = minute;
        }

        private void TimeOkay_Button_Click(object sender, RoutedEventArgs e)
        {
            prevWindow.AddToTriggerTimes(dtt);
            this.Close();
        }

        private void TimeCancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
