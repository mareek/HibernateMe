using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Threading;

namespace HibernateMe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimeSpan timeOut;
        private TimeSpan reminderTime;
        private DateTime limitDate;
        private DateTime reminderDate;
        private bool lastMinute = false;

        private DispatcherTimer refreshTimer;

        public MainWindow(TimeSpan timeOut, TimeSpan reminderTime)
        {
            this.timeOut = timeOut;
            this.reminderTime = reminderTime;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            limitDate = DateTime.Now + timeOut;
            reminderDate = limitDate - reminderTime;

            this.Topmost = false;

            refreshTimer_Tick(null, null);

            refreshTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.1) };
            refreshTimer.Tick += new EventHandler(refreshTimer_Tick);
            refreshTimer.Start();
        }

        void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (limitDate == DateTime.MinValue)
            {
                this.Close();
            }
            else if (limitDate < DateTime.Now)
            {
                limitDate = DateTime.MinValue;
                this.Visibility = Visibility.Collapsed;
                HibernateMe();
            }
            else if (!lastMinute && reminderDate < DateTime.Now)
            {
                lastMinute = true;
                DelayButton.IsEnabled = true;

                this.Background = Brushes.OrangeRed;
                this.CountDownLabel.Foreground = Brushes.White;

                this.Topmost = true;
            }
            else if ((limitDate - DateTime.Now) < TimeSpan.FromSeconds(30))
            {
                this.Topmost = true;
            }
            else if (lastMinute)
            {
                this.Topmost = false;
            }

            CountDownLabel.Content = string.Format("{0} avant hibenation", (limitDate - DateTime.Now).ToString(@"mm\:ss"));
        }

        private void DelayButton_Click(object sender, RoutedEventArgs e)
        {
            limitDate += reminderTime;
            reminderDate += reminderTime;
            DelayButton.IsEnabled = false;
            lastMinute = false;
            this.Background = Brushes.White;
            this.CountDownLabel.Foreground = Brushes.Black;
            this.Topmost = false;
        }

        private void HibernateMe()
        {
            Process.Start("shutdown", "/h /f");
        }
    }
}
