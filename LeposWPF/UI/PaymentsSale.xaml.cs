using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows;

namespace LeposWPF.UI
{
    /// <summary>
    /// Interaction logic for PaymentsSale.xaml
    /// </summary>
    public partial class PaymentsSale : MetroWindow
    {
        #region Constructors
        /// <summary>
        /// Initialize current window
        /// </summary>
        /// <param name="window"></param>
        public PaymentsSale(Window window)
        {
            this.Owner = window;
            InitializeComponent();
        }
        #endregion
        #region UI Events
        /// <summary>
        /// Loaded window
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            paymentsDataGrid.Width = ActualWidth;
            var source = new List<Test>();
            source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
            source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
            source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
            source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
            paymentsDataGrid.ItemsSource = source;
            paymentsDataGrid.Height = dataGridContainerViewBox.ActualHeight;
            paymentsDataGrid.Width = ActualWidth;
        }
        #endregion
    }
}
