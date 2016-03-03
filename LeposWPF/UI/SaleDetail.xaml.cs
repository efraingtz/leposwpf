using LeposWPF.Helpers;
using LeposWPF.Model;
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
using System.Windows.Shapes;

namespace LeposWPF.UI
{
    /// <summary>
    /// Interaction logic for SaleDetail.xaml
    /// </summary>
    public partial class SaleDetail : MetroWindow
    {
        #region Declaration
        /// <summary>
        /// Sale to display values
        /// </summary>
        private Sale sale;
        #endregion
        #region Constructors
        /// <summary>
        /// Initialize current instance
        /// </summary>
        /// <param name="window">Owner window</param>
        /// <param name="sale">Sale to display values</param>
        public SaleDetail(Window window, Sale sale)
        {
            this.Owner = window;
            this.sale = sale;
            InitializeComponent();
        }
        #endregion
        #region UI Events
        /// <summary>
        /// Event for loading window
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            productsDataGrid.AutoGeneratingColumn += dataGrid_AutoGeneratingColumn;
            productsDataGrid.Width = ActualWidth;
            productsDataGrid.ItemsSource = sale.SaleProducts;
            productsDataGrid.Height = dataGridContainerViewBox.ActualHeight;
            fillDetails();
        }
        /// <summary>
        /// AutoGeneratingColumn handler for DataGrid
        /// </summary>
        /// <param name="sender">DataGrid object.</param>
        /// <param name="e">Event of sender object.</param>
        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridHelper.dataGrid_AutoGeneratingColumn(sender,e,this);
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Fill details according to current details
        /// </summary>
        private void fillDetails()
        {
            sellerTextBox.Text = sale.User_ID;
            clientTextBox.Text = sale.Client.Name;
            dateTextBox.Text = sale.Date.ToShortDateString();
            ivaTextBox.Text = Helpers.Clases.CompanyHelper.getIVAString(sale.IVAType);
            subtotalTextBox.Text = sale.SubTotal.ToString("C");
            totalTextBox.Text = sale.Total.ToString("C");
            discountTextBox.Text = sale.Discount.ToString("P");
        }
        #endregion
    }
}
