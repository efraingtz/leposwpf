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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LeposWPF.UI
{
    /// <summary>
    /// Interaction logic for AccountStatus.xaml
    /// </summary>
    public partial class AccountStatus : MetroWindow
    {
        #region Declaration 
        /// <summary>
        /// Used to animate content
        /// </summary>
        public Storyboard storyBoard { get; set; }
        /// <summary>
        /// Connection to entity framework model
        /// </summary>
        private LeposWPFModel model = new LeposWPFModel();
        /// <summary>
        /// Client to whom status is going to be displayed
        /// </summary>
        public Client Client { get; set; }
        #endregion
        #region Constructors
        /// <summary>
        /// Inititalize new instance of current window
        /// </summary>
        public AccountStatus(Window window, Client client)
        {
            this.Client = client;
            this.Owner = Owner;
            InitializeComponent();
        }
        #endregion
        #region UI Events
        /// <summary>
        /// Filter by ID
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void saleIDButton_Click(object sender, RoutedEventArgs e)
        {
            fillDebts(true);
        }
        /// <summary>
        /// Loaded window event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            gridGroupBox.Header += Client.Name;
            Title += Client.Name;
            debtsDataGrid.AutoGeneratingColumn += dataGrid_AutoGeneratingColumn;
            storyBoard = (Storyboard)FindResource("animate");
            var now = DateTime.Now;
            purchasesStartDatePicker.SelectedDate = new DateTime(now.Year, now.Month, 1);
            purchasesEndDatePicker.SelectedDate = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));
            debtsDataGrid.Width = loadingProgressBar.Width = ActualWidth;
            debtsDataGrid.Height = dataGridContainerViewBox.ActualHeight;
            fillDebts();
        }
        /// <summary>
        /// Filter results between the selected dates
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            fillDebts();
        }
        /// <summary>
        /// Hanlde view sale event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void viewTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int ID = int.Parse((sender as TextBlock).Tag.ToString());
            Sale sale = model.Sales.Where(a => a.ID == ID).FirstOrDefault();
            if (sale != null)
            {
                Hide();
                new SaleDetail(this, sale).ShowDialog();
                ShowDialog();
            }
        }
        /// <summary>
        /// Hanlde add payment event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void payTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int ID = int.Parse((sender as TextBlock).Tag.ToString());
            Sale sale = model.Sales.Where(a => a.ID == ID).FirstOrDefault();
            if (sale != null)
            {
                Hide();
                new PaymentsSale(this, sale, model).ShowDialog();
                fillDebts();
                ShowDialog();
            }
        }
        /// <summary>
        /// Color DataGrid according to rule business
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void debtsDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DataGridRow row = e.Row;
            dynamic item = row.Item;
            if (item.Total > item.Payments)
            {
                DateTime payDate = DateTime.Parse(item.Date.ToShortDateString()).AddDays(item.CreditDays);
                int differenceDays = (payDate - DateTime.Parse(DateTime.Now.ToShortDateString())).Days;
                if (differenceDays <= 0)
                    row.Background = Brushes.Red;
                else if (differenceDays <= 5)
                {
                    row.Background = Brushes.Green;
                }
            }
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Filter sales
        /// </summary>
        /// <param name="byIdFlag"></param>
        private void fillDebts(Boolean byIdFlag = false)
        {
            displayText("Obteniendo información");
            if (byIdFlag)
            {
                int ID = 0;
                Boolean textBoxBoolean = int.TryParse(saleIDTextBox.Text, out ID);
                if (textBoxBoolean)
                {
                    var debts = model.Sales.Where(a => a.ID == ID).OrderByDescending(a => a.ID).ToList();
                    debtsDataGrid.ItemsSource = debts;
                    double totalDebt = 0;
                    debts.ForEach(a => totalDebt += (a.Total - a.Payments));
                    debtTextBlock.Text = "Adeudo  : " + totalDebt.ToString("C");
                }
                else displayText("Error : el ID ingresado no es válido",false);
            }
            else
            { 
                if (purchasesStartDatePicker.SelectedDate <= purchasesEndDatePicker.SelectedDate)
                {
                    DateTime start = purchasesStartDatePicker.SelectedDate.Value;
                    DateTime end = purchasesEndDatePicker.SelectedDate.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
                    var debts = model.Sales.Where(a => a.Date >= start && a.Date <= end && a.IsCredit && a.Client_ID == Client.ID).OrderByDescending(a => a.ID).ToList();
                    debtsDataGrid.ItemsSource = debts;
                    double totalDebt = 0;
                    debts.ForEach(a => totalDebt += (a.Total - a.Payments));
                    debtTextBlock.Text = "Adeudo  : " + totalDebt.ToString("C");
                }
                else
                    displayText("Error: verificar fechas", false);
            }
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
            WindowHelper.displayText(alertTextBlock, loadingProgressBar, storyBoard, text, good);
        }
        #endregion
    }
}
