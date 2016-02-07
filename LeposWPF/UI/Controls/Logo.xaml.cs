using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LeposWPF.UI.Controls
{
    /// <summary>
    /// Interaction logic for Logo.xaml
    /// </summary>
    public partial class Logo : UserControl
    {
        #region Constructors
        /// <summary>
        /// Initialize current instance
        /// </summary>
        public Logo()
        {
            InitializeComponent();
        }
        #endregion
        #region UI Events
        /// <summary>
        /// Cancel editing data
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Loaded event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }
        /// <summary>
        /// Saving event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Browse new logo's iage
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void browseButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
