using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace HibernateMe
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            TimeSpan timeOut;
            TimeSpan reminderTime;

            int timeOutInMinutes;
            int reminderTimeInMinutes;
            if (int.TryParse(e.Args.FirstOrDefault(), out timeOutInMinutes)
                && int.TryParse(e.Args.Skip(1).FirstOrDefault(), out reminderTimeInMinutes))
            {
                timeOut = TimeSpan.FromMinutes(timeOutInMinutes);
                reminderTime = TimeSpan.FromMinutes(reminderTimeInMinutes);
            }
            else
            {
                timeOut = TimeSpan.FromMinutes(3);
                reminderTime = TimeSpan.FromMinutes(1);
            }
            new MainWindow(timeOut, reminderTime).Show();
        }
    }
}
