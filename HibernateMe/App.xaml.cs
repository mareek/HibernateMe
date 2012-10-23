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
            
            int timeOutInMinutes;
            if (int.TryParse(e.Args.FirstOrDefault(), out timeOutInMinutes))
            {
                timeOut = TimeSpan.FromMinutes(timeOutInMinutes);
            }
            else
            {
                timeOut = TimeSpan.FromMinutes(3);
            }
            new MainWindow(timeOut).Show();
        }
    }
}
