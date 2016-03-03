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
    /// Interaction logic for purchaseDetail.xaml
    /// </summary>
    public partial class PurchaseDetail : MetroWindow
    {
        #region Declaration
        /// <summary>
        /// purchase to display values
        /// </summary>
        private Purchase purchase;
        #endregion
        #region Constructors
        /// <summary>
        /// Initialize current instance
        /// </summary>
        /// <param name="window">Owner window</param>
        /// <param name="purchase">Purchase to display values</param>
        public PurchaseDetail(Window window, Purchase purchase)
        {
            this.Owner = window;
            this.purchase = purchase;
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
            productsDataGrid.Height = dataGridContainerViewBox.ActualHeight;
            productsDataGrid.ItemsSource = purchase.PurchaseProducts;
            fillDetails();
        }
        /// <summary>
        /// AutoGeneratingColumn handler for DataGrid
        /// </summary>
        /// <param name="sender">DataGrid object.</param>
        /// <param name="e">Event of sender object.</param>
        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridHelper.dataGrid_AutoGeneratingColumn(sender, e, this);
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Fill details according to current details
        /// </summary>
        private void fillDetails()
        {
            sellerTextBox.Text = purchase.User_ID;
            supplierTextBox.Text = purchase.Provider.Name;
            dateTextBox.Text = purchase.Date.ToShortDateString();
            folioTextBox.Text = purchase.Folio;
            costTextBox.Text = purchase.Total.ToString("C");
        }
        #endregion
    }
}
