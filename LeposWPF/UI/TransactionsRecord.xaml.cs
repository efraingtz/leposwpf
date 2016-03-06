using LeposWPF.Helpers;
using LeposWPF.Model;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LeposWPF.UI
{
    /// <summary>
    /// Interaction logic for TransactionsRecord.xaml
    /// </summary>
    public partial class TransactionsRecord : MetroWindow
    {
        #region Declaration
        /// <summary>
        /// Used to animate content
        /// </summary>
        private Storyboard storyBoard
        {
            get;
            set;
        }
        /// <summary>
        /// Flag that indicates if the purchases tab has been clicked
        /// </summary>
        private Boolean purchasesTabFlag = false;
        /// <summary>
        /// Connection to entity framework model
        /// </summary>
        private LeposWPFModel model = new LeposWPFModel();
        #endregion
        #region Constructors
        /// <summary>
        /// Inititalize new instance of current window
        /// </summary>
        public TransactionsRecord()
        {
            InitializeComponent();
        }
        #endregion
        #region UI Events
        /// <summary>
        /// Loaded window event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            storyBoard = (Storyboard)FindResource("animate");
            salesDataGrid.AutoGeneratingColumn += dataGrid_AutoGeneratingColumn;
            purchasesDataGrid.AutoGeneratingColumn += dataGrid_AutoGeneratingColumn;
            var now = DateTime.Now;
            salesDataGrid.Width = purchasesDataGrid.Width = ActualWidth;
            salesDataGrid.Height = purchasesDataGrid.Height = dataGridContainerViewBox.ActualHeight;
            salesProgressBar.Width = ActualWidth;
            purchasesProgressBar.Width = ActualWidth;
            salesStartDatePicker.SelectedDate = purchasesStartDatePicker.SelectedDate = new DateTime(now.Year, now.Month,1);
            salesEndDatePicker.SelectedDate = purchasesEndDatePicker.SelectedDate = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year,now.Month));
            displayText("Cargando información");
            fillSales();
        }
        /// <summary>
        /// Handle selection tab clicked
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void purchasesTabItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!purchasesTabFlag)
            {
                displayText("Cargando información");
                fillPurchases();
                purchasesTabFlag = true;
            }
        }
        /// <summary>
        /// Hanlde view sale event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void viewTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int ID = int.Parse((sender as TextBlock).Tag.ToString());
            if (salesTabItem.IsSelected)
            {
                Sale sale = model.Sales.Where(a => a.ID == ID).FirstOrDefault();
                if (sale != null)
                {
                    Hide();
                    new SaleDetail(this, sale).ShowDialog();
                    ShowDialog();
                }
            }
            else if (purchasesTabItem.IsSelected)
            {
                Purchase purchase = model.Purchases.Where(a => a.ID == ID).FirstOrDefault();
                if (purchase != null)
                {
                    Hide();
                    new PurchaseDetail(this, purchase).ShowDialog();
                    ShowDialog();
                }
            }
        }
        
        /// <summary>
        /// Handle search sales event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void sales_Click(object sender, RoutedEventArgs e)
        {
            if (salesStartDatePicker.SelectedDate <= salesEndDatePicker.SelectedDate)
            {
                displayText("Cargando información");
                fillSales();
            }
            else displayText("Error: verificar fechas",false);
        }
        /// <summary>
        /// Handle search purchases event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void purchasesButton_Click(object sender, RoutedEventArgs e)
        {
            if (purchasesStartDatePicker.SelectedDate <= purchasesEndDatePicker.SelectedDate)
            {
                displayText("Cargando información");
                fillPurchases();
            }
            else displayText("Error: verificar fechas",false);
        }
        /// <summary>
        /// Handle sale note event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void saleNoteButton_Click(object sender, RoutedEventArgs e)
        {
            if (salesStartDatePicker.SelectedDate <= salesEndDatePicker.SelectedDate)
            {
                Hide();
                new SalesPeriodNote(this, salesStartDatePicker.SelectedDate.Value, salesEndDatePicker.SelectedDate.Value.AddHours(23).AddMinutes(59).AddSeconds(59)).ShowDialog();
                ShowDialog();
            }
            else displayText("Error: verificar fechas", false);
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Fill sales DataGrid with current information
        /// </summary>
        private void fillSales()
        {
            DateTime start = salesStartDatePicker.SelectedDate.Value;
            DateTime end = salesEndDatePicker.SelectedDate.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
            var sales = model.Sales.Where(a=> a.Date >= start && a.Date <= end).OrderByDescending(a=> a.ID).ToList();
            salesDataGrid.ItemsSource = sales;
            decimal salesTotal = 0;
            sales.ForEach(a=> salesTotal+= (decimal)a.Total);
            salesTextBlock.Text = "Ventas : " + salesTotal.ToString("C4");
        }
        /// <summary>
        /// Fill purchases DataGrid with current information
        /// </summary>
        private void fillPurchases()
        {
            DateTime start = purchasesStartDatePicker.SelectedDate.Value;
            DateTime end = purchasesEndDatePicker.SelectedDate.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
            var purchases = model.Purchases.Where(a => a.Date >= start && a.Date <= end).OrderByDescending(a => a.ID).ToList();
            purchasesDataGrid.ItemsSource = purchases;
            double purchasesTotal = 0;
            purchases.ForEach(a => purchasesTotal += a.Total);
            purchasesTextBlock.Text = "Compras : " + purchasesTotal.ToString("C4");
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
        /// <summary>
        /// Display alert messages for the user
        /// </summary>
        /// <param name="text">Text to display to the user</param>
        /// <param name="good">Define text color, when true it is green, when bad it is red</param>
        public void displayText(string text, bool good = true)
        {
            var textBlock = salesTabItem.IsSelected ? salesAlertTextBlock : purchasesAlertTextBlock;
            var progressBar = salesTabItem.IsSelected ? salesProgressBar : purchasesProgressBar;
            WindowHelper.displayText(textBlock, progressBar, storyBoard, text, good);
        }
        #endregion
    }
}
