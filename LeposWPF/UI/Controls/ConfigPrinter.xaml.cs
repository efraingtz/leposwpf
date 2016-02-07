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
    /// Interaction logic for ConfigPrinter.xaml
    /// </summary>
    public partial class ConfigPrinter : UserControl
    {
        #region Constructors
        /// <summary>
        /// Initialize current instance
        /// </summary>
        public ConfigPrinter()
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
            printersListView.Items.Add("A");
            printersListView.Items.Add("B");
            printersListView.Items.Add("D");
            printersListView.Items.Add("C");
            errorTextBlock.Width = logoImage.ActualWidth;
        }
        /// <summary>
        /// Saving event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            errorTextBlock.Text = " No ha seleccionado una impresora";
        }
        #endregion
    }
}
