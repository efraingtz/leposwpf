using MahApps.Metro.Controls;
using System;
using System.Reflection;
using System.Windows;

namespace LeposWPF.UI
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : MetroWindow
    {
        #region Assembly Attribute Accessors
        /// <summary>
        /// Get title of the current assembly
        /// </summary>
        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }
        /// <summary>
        /// Get version of current version
        /// </summary>
        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
        /// <summary>
        /// Get description of current assembly
        /// </summary>
        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }
        /// <summary>
        /// Get name of current assembly's product
        /// </summary>
        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }
        /// <summary>
        /// Get copyright
        /// </summary>
        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }
        /// <summary>
        /// Get name of current assembly's company
        /// </summary>
        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
        #region Constructors
        /// <summary>
        /// Initialize window
        /// </summary>
        public AboutWindow()
        {
            InitializeComponent();
        }
        #endregion
        #region UI Events
        /// <summary>
        /// Close window
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Loaded event window
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.productNameTextBox.Text += String.Format("About {0}", AssemblyTitle);
            this.versionTextBox.Text = String.Format("Version {0}", AssemblyVersion);
            this.copyrightTextBox.Text = AssemblyCopyright;
            this.companyTextBox.Text += AssemblyCompany;
        }
        #endregion
    }
}
