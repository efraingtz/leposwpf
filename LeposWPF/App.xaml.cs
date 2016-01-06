using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Threading;
using MahApps.Metro;
using LEPOSWPF.SplashScreen;

namespace Lepos
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

            Splasher.Splash = new SplashLoadingWindow();
            Splasher.ShowSplash();

            for (int i = 1; i <= 10; i += 1)
            {
                MessageListener.Instance.ReceiveMessage(string.Format("{0}%", i*10));
                Thread.Sleep(1000);
            }

            Splasher.CloseSplash();

            App app = new App();
            app.InitializeComponent();
            app.Run();
        }

        #endregion
    }
}
