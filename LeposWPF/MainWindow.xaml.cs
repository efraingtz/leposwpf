using MahApps.Metro.Controls;
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

namespace LeposWPF
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
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion
        #region Interface
        /// <summary>
        /// Loaded event for current instance
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dateLabel.Content += DateTime.Now.ToShortDateString();
        }
        #endregion
        #region Buttons
        /// <summary>
        /// Close current window
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion
    }
}
