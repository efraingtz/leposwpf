using MahApps.Metro.Controls;
using System.Windows;

namespace LeposWPF.UI
{
    /// <summary>
    /// Interaction logic for ImportCatalog.xaml
    /// </summary>
    public partial class ImportCatalog : MetroWindow
    {
        #region Constructs
        /// <summary>
        /// Initialize constructrs
        /// </summary>
        /// <param name="window">Owner window</param>
        public ImportCatalog(Window window)
        {
            this.Owner = window;
            InitializeComponent();
        }
        #endregion
        #region UI Events
        /// <summary>
        /// Importing file event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void importButton_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Browse path for importing file
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void browseButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
