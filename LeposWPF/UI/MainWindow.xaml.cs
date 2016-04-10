using LeposWPF.Helpers;
using LeposWPF.Helpers.Clases;
using MahApps.Metro.Controls;
using System;
using System.Windows;

namespace LeposWPF.UI
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        /// <param name="owner">Owner window</param>
        public MainWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;
        }
        #endregion
        #region UI Events
        /// <summary>
        /// Handle when current window is closed
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        /// <summary>
        /// Loaded event for current instance
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dateLabel.Content += DateTime.Now.ToShortDateString();
            //Display UI per type of user
            if (UserHelper.loggedUser.Type == 1) //Seller user
            {
                reportsButton.IsEnabled = 
                    importMenuItem.IsEnabled =
                        settingsMenuItem.IsEnabled = false;
            }
            setLogoImage();
        }
        /// <summary>
        /// Event to handle clicking settings button
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event of sender object.</param>
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        /// <summary>
        /// Event to handle clicking settings button
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event of sender object.</param>
        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            settingsButton.ContextMenu.DataContext = settingsButton.DataContext;
            settingsButton.ContextMenu.IsOpen = true;
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Open a new window according to the clicked button
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Events of sender object</param>
        private void openWindow(object sender, RoutedEventArgs e)
        {
            if(sender.GetType().GetProperty("Tag") != null)
            {
                dynamic senderObject = sender as dynamic;
                if (senderObject.Tag != null)
                {
                    String tag = senderObject.Tag.ToString();
                    dynamic window = Activator.CreateInstance(Type.GetType(tag));
                    if(window is Window)
                        window.Owner = this;
                    Hide();
                    window.ShowDialog();
                    ShowDialog();
                }
            }
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Display logo image
        /// </summary>
        internal void setLogoImage()
        {
            logoImage.Source = WindowHelper.getLogo(CompanyHelper.getLogoPath());
        }
        #endregion
    }
}
