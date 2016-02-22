using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Threading;
using MahApps.Metro;
using LEPOSWPF.SplashScreen;
using LeposWPF.Helpers.Clases;

namespace LeposWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Application
        /// <summary>
        /// Load splash screen and execute main application
        /// </summary>
        [STAThread()]
        static void Main()
        {
            CompanyHelper.companyExists();
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-MX");
            Splasher.Splash = new SplashLoadingWindow();
            Splasher.ShowSplash();

            for (int i = 1; i <= 100; i += 2)
            {
                MessageListener.Instance.ReceiveMessage(string.Format("{0}%", i));
                Thread.Sleep(50);
            }

            Splasher.CloseSplash();

            App app = new App();
            app.InitializeComponent();
            app.Run();
        }

        #endregion
    }
}
