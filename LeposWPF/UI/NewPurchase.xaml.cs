using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows;

namespace LeposWPF.UI
{
    /// <summary>
    /// Interaction logic for NewSale.xaml
    /// </summary>
    public partial class NewPurchase : MetroWindow
    {
        #region Constructors
        /// <summary>
        /// Initialize current instance
        /// </summary>
        public NewPurchase()
        {
            InitializeComponent();
        }
        #endregion
        #region UI Events
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            searchGrid.Width = ActualWidth;
            var source = new List<Test>();
            source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
            source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
            source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
            source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
            saleDataGrid.ItemsSource = source;
            saleDataGrid.Height = controlGrid.Height = dataGridContainerViewBox.ActualHeight;
            saleDataGrid.Width = ActualWidth * 0.8;
            controlGrid.Width = ActualWidth * 0.2;
        }
        /// <summary>
        /// Listen to keyboard
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void MetroWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F5)
            {
                Window window = new SearchProduct(this);
                Hide();
                window.ShowDialog();
                ShowDialog();
            }
        }
        /// <summary>
        /// Enable or disable column when checked
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void costCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Enable or disable column when checked
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void priceCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Handle new sale's event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void purchaseButton_Click(object sender, RoutedEventArgs e)
        {
            subTotalTextBlock.Text = "Subtotal: $215,000.00";
            totalTextBlock.Text = "Total: $415,000.00";
            alertTextBox.Text = "LA COMPRA NO TIENE UN PROVEEDOR SELECCIONADO NI PRODUCTOS EN LA LISTA PARA SER VENDIDOS.";
        }

        #endregion
    }
}
