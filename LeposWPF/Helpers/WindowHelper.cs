using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace LeposWPF.Helpers
{
    /// <summary>
    /// Allows WPF windows and components to be refreshed
    /// from Geeks with blogs - New Things I Learned
    /// http://geekswithblogs.net/NewThingsILearned/archive/2008/08/25/refresh--update-wpf-controls.aspx
    /// </summary>
    public static class WindowHelper
    {
        #region UI Actions
        /// <summary>
        /// Validate if all fields have a text written in
        /// </summary>
        /// <param name="textBoxes">Array of text boxes</param>
        /// <returns>Whether or not all the text boxes have text written in</returns>
        internal static Boolean validateEmptyTextBoxes(params TextBox[] textBoxes)
        {
            foreach (var textBox in textBoxes)
                if (textBox.Text == string.Empty)
                    return false;
            return true;
        }

        /// <summary>
        /// Find the current logo image stored in a specific path
        /// </summary>
        /// <param name="path">Path to find the image</param>
        /// <returns>Logo image</returns>
        internal static System.Windows.Media.Imaging.BitmapImage getLogo(String path)
        {
            return new System.Windows.Media.Imaging.BitmapImage(new Uri(path));
        }

        private static Action EmptyDelegate = delegate () { };

        /// <summary>
        /// UserInterfaceRefresh User Interface Element
        /// </summary>
        /// <param name="uiElement">User Interface Element</param>
        public static void UserInterfaceRefresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, EmptyDelegate); //Render
        }

        /// <summary>
        /// Display alert messages for the user
        /// </summary>
        /// <param name="text">Text to display to the user</param>
        /// <param name="good">Define text color, when true it is green, when bad it is red</param>
        public static void displayText(TextBlock alert, string text, bool good = true)
        {
            alert.Dispatcher.Invoke(
                System.Windows.Threading.DispatcherPriority.Background,
                new Action(
                    delegate ()
                    {
                        alert.Foreground = good
                            ? Brushes.Green
                            : Brushes.Red;
                        alert.Text = text;
                    }
                )
            );
        }

        /// <summary>
        /// Display alert messages for the user
        /// </summary>
        /// <param name="text">Text to display to the user</param>
        /// <param name="good">Define text color, when true it is green, when bad it is red</param>
        public static void displayText(TextBlock alertTextBlock, ProgressBar loadingProgressBar, Storyboard storyBoard,
            string text, bool good = true, double goodLenght = 3.5, double errorLenght = 5)
        {
            alertTextBlock.Dispatcher.Invoke(
                System.Windows.Threading.DispatcherPriority.Background,
                new Action(
                    delegate ()
                    {
                        alertTextBlock.Foreground = good
                            ? Brushes.Green
                            : Brushes.Red;
                        alertTextBlock.Text = text;
                        DoubleAnimation doubleAnimation = storyBoard.Children[0] as DoubleAnimation;
                        double duration = good ? goodLenght : errorLenght;
                        loadingProgressBar.IsIndeterminate = good;
                        loadingProgressBar.Value = 100;
                        doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(duration));
                        WindowHelper.fadeOutControl(alertTextBlock, storyBoard);
                        WindowHelper.fadeOutControl(loadingProgressBar, storyBoard);
                    }
                )
            );
        }

        /// <summary>
        /// Make control fade out
        /// </summary>
        /// <param name="control">Controls to animate</param>
        public static void fadeOutControl(FrameworkElement control, Storyboard animate)
        {
            control.Dispatcher.Invoke(
                System.Windows.Threading.DispatcherPriority.Background,
                new Action(
                    delegate ()
                    {
                        control.Visibility = System.Windows.Visibility.Visible;
                        animate.Begin(control);
                    }
                )
            );
        }
        /// <summary>
        /// Valide input for only numeric / decimal values
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        internal static void validateTextBok_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Boolean isDigit = !(e.Key < System.Windows.Input.Key.D0 || e.Key > System.Windows.Input.Key.D9);
            Boolean singleDot = textBox.Text.Where(a => a.Equals('.')).Count() == 0;
            Boolean isDot = e.Key == System.Windows.Input.Key.OemPeriod;
            Boolean isBackSpace = e.Key == System.Windows.Input.Key.Back;
            if (!((isDot && singleDot) || isBackSpace || isDigit))
            {
                e.Handled = true;
            }
        }

        #endregion UI Actions

        #region 7-Zip

        /// <summary>
        /// Execute Seven Zip to compress files
        /// </summary>
        /// <param name="destinationFile">The .7z file that will be created or have files added to it</param>
        /// <param name="sources">Folders/Files to be added to .7z file</param>
        public static void SevenZip(string destinationFile, string[] sources)
        {
            Paths paths = new Paths();
            string sourcesString = "";

            foreach (string source in sources)
            {
                sourcesString += " \"" + source + "\"";
            }

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = paths.getSevenZipExe();
            psi.Arguments = " a -mx=9 \"" + destinationFile + "\"" + sourcesString;
            psi.WindowStyle = ProcessWindowStyle.Normal;

            Process proc = Process.Start(psi);
            proc.WaitForExit();
        }

        #endregion 7-Zip

        #region Published Revision

        /// <summary>
        /// Gets the Publishing Version NumberInt for actual application
        /// </summary>
        /// <returns>Version #</returns>
        public static string GetPublishVersion()
        {
            string version;

            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                System.Deployment.Application.ApplicationDeployment cd =
                System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
                version = cd.CurrentVersion.ToString();
                // show publish version in title or About box...
            }
            else
            {
                version = GetVersion();
            }

            return version;
        }

        /// <summary>
        /// Check if actual executable is the latest revision
        /// </summary>
        /// <returns>Boolean value informing if actual running application is the same as the latest published revision</returns>
        public static bool IsLatestRevision()
        {
            int actualRevision = Convert.ToInt32(GetPublishVersion().Replace(".", ""));
            int latestRevision = 0;
            int dirRevision = 0;
            string pathApps = new Paths().getBIA() + "\\WODVP\\Binary\\Application Files";
            DirectoryInfo dirApps = new DirectoryInfo(pathApps);

            foreach (DirectoryInfo dir in dirApps.GetDirectories())
            {
                dirRevision = Convert.ToInt32(dir.Name.Replace("WODVP", "").Replace("_", ""));

                if (dirRevision > latestRevision)
                {
                    latestRevision = dirRevision;
                }
            }

            return (actualRevision == latestRevision || actualRevision == 3000);
        }

        #endregion Published Revision

        #region Assembly Info

        /// <summary>
        /// Get Application Title
        /// </summary>
        /// <returns>Application Title as string</returns>
        public static string GetTitle()
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0)
            {
                AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                if (!string.IsNullOrEmpty(titleAttribute.Title))
                {
                    return titleAttribute.Title;
                }
            }

            return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
        }

        /// <summary>
        /// Get Version of Application
        /// </summary>
        /// <returns>Version # as string</returns>
        public static string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        /// <summary>
        /// Get Description of Application
        /// </summary>
        /// <returns>Description as string</returns>
        public static string GetDescription()
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attributes.Length == 0)
            {
                return "";
            }

            return ((AssemblyDescriptionAttribute)attributes[0]).Description;
        }

        /// <summary>
        /// Get Product of Application
        /// </summary>
        /// <returns>Product as string</returns>
        public static string GetProduct()
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            if (attributes.Length == 0)
            {
                return "";
            }

            return ((AssemblyProductAttribute)attributes[0]).Product;
        }

        /// <summary>
        /// Get Copyright of Application
        /// </summary>
        /// <returns>Copyright as string</returns>
        public static string GetCopyright()
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if (attributes.Length == 0)
            {
                return "";
            }

            return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
        }

        /// <summary>
        /// Get Company of Application
        /// </summary>
        /// <returns>Company as string</returns>
        public static string GetCompany()
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            if (attributes.Length == 0)
            {
                return "";
            }

            return ((AssemblyCompanyAttribute)attributes[0]).Company;
        }

        public static string GetTradeMark()
        {
            object[] attributtes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTrademarkAttribute), false);

            if (attributtes.Length == 0)
            {
                return "";
            }

            return ((AssemblyTrademarkAttribute)attributtes[0]).Trademark;
        }

        /// <summary>
        /// Get All above in counter unique string
        /// </summary>
        /// <returns>All previous gets in counter unique string</returns>
        public static string GetAssemblyComplete()
        {
            StringBuilder complete = new StringBuilder();

            complete.Append("Title......: ");
            complete.Append(GetTitle());
            complete.Append("\n\n");

            complete.Append("Version....: ");
            complete.Append(GetPublishVersion());
            complete.Append("\n\n");

            complete.Append("Description: ");
            complete.Append(GetDescription());
            complete.Append("\n\n");

            complete.Append("Product....: ");
            complete.Append(GetProduct());
            complete.Append("\n\n");

            complete.Append("Copyright..: ");
            complete.Append(GetTitle());
            complete.Append("\n\n");

            complete.Append("Company....: ");
            complete.Append(GetCompany());
            complete.Append("\n\n");

            complete.Append("Trade Mark.: ");
            complete.Append(GetTradeMark());
            complete.Append("\r\n");

            return complete.ToString();
        }

        #endregion Assembly Info

    }

}
