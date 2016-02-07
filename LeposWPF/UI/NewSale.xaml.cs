using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LeposWPF.UI
{
    /// <summary>
    /// Interaction logic for NewSale.xaml
    /// </summary>
    public partial class NewSale : MetroWindow
    {
        #region Constructors
        /// <summary>
        /// Initialize current instance
        /// </summary>
        public NewSale()
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
        /// Handle new sale's event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void saleButton_Click(object sender, RoutedEventArgs e)
        {
            subTotalTextBlock.Text = "Subtotal: $215,000.00";
            totalTextBlock.Text = "Total: $415,000.00";
            alertTextBox.Text = "LA VENTA NO TIENE UN CLIENTE SELECCIONADO NI PRODUCTOS EN LA LISTA PARA SER VENDIDOS.";
        }
        /// <summary>
        /// Handle selection changed for ivaComboBox
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void ivaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
        #endregion
    }
}
