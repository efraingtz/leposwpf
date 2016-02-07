using MahApps.Metro.Controls;
using System.Windows;

namespace LeposWPF.UI
{
    /// <summary>
    /// Interaction logic for ReduceDebtPurchase.xaml
    /// </summary>
    public partial class ReduceDebtPurchase : MetroWindow
    {
        #region Constructors
        /// <summary>
        /// Initialize current window
        /// </summary>
        /// <param name="window"></param>
        public ReduceDebtPurchase(Window window)
        {
            this.Owner = window;
            InitializeComponent();
        }
        #endregion
        #region UI Events
        /// <summary>
        /// Add payment to debt
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void payButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

    }
}
