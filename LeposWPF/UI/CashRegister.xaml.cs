using LeposWPF.Helpers.Clases;
using MahApps.Metro.Controls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace LeposWPF.UI
{
    /// <summary>
    /// Interaction logic for CashRegister.xaml
    /// </summary>
    public partial class CashRegister : MetroWindow
    {
        #region Declaration
        /// <summary>
        /// Start date for the cash register
        /// </summary>
        public DateTime initDateTime { get; set; }
        /// <summary>
        /// End date for the cash register
        /// </summary>
        public DateTime endDateTime { get; set; }
        /// <summary>
        /// Model instance
        /// </summary>
        private Model.LeposWPFModel model = new Model.LeposWPFModel();
        /// <summary>
        /// Total money on cash register
        /// </summary>
        private double total = 0;
        /// <summary>
        /// Value of selected utility
        /// </summary>
        private double utility = 0;
        #endregion
        #region Constructors
        /// <summary>
        /// Initialize current window
        /// </summary>
        public CashRegister()
        {
            InitializeComponent();
            initDateTime = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
            endDateTime = initDateTime.AddHours(23).AddMinutes(59).AddSeconds(59);
        }
        #endregion
        #region UI Events
        /// <summary>
        /// Cut money according to last status
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void boxCutButton_Click(object sender, RoutedEventArgs e)
        {
            CompanyHelper.currentCompany.Cash = total;
            messageTextBlock.Foreground = Brushes.Green;
            messageTextBlock.Text = "Corte Realizado";
            fillRegisterDetails();
        }
        /// <summary>
        /// Cut money to zero
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void boxCutZeroButton_Click(object sender, RoutedEventArgs e)
        {
            CompanyHelper.currentCompany.Cash = 0;
            messageTextBlock.Foreground = Brushes.Green;
            messageTextBlock.Text = "Corte Realizado";
            fillRegisterDetails();
        }
        /// <summary>
        /// Open sales period note
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void salesPeriodNoteButton_Click(object sender, RoutedEventArgs e)
        {
            var date = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
            var finish = date.AddHours(23).AddMinutes(59).AddSeconds(59);
            Window window = new SalesPeriodNote(this, date, finish);
            Hide();
            window.ShowDialog();
            ShowDialog();
        }
        /// <summary>
        /// Loaded event of current window
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var period = initDateTime.ToShortDateString() + " - " + endDateTime.ToShortDateString();
            Title = "Caja Registradora " + period;
            fillRegisterDetails();
            fillUtilityDetails();
        }
        /// <summary>
        /// Reset message values
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void TabItem_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            messageTextBlock.Text = utilityMessageTextBlock.Text = String.Empty;
        }
        /// <summary>
        /// Handle when a CheckBox is checked in the Box Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            fillRegisterDetails();
        }
        /// <summary>
        /// Handle when a CheckBox is checked in the Utily Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxUtiliy_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            fillUtilityDetails();
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Fill form's data
        /// </summary>
        private void fillRegisterDetails()
        {
            if (IsLoaded)
            {
                initalMoneyTextBlock.Text =
                    salesTextBlock.Text =
                        depositCreditSalesTextBlock.Text =
                            purchasesTextBlock.Text =
                                paymentsPurchasesTextBlock.Text = String.Empty;
                total = 0;
                if (initialMoneyCheckBox.IsChecked.Value)
                {
                    total += CompanyHelper.currentCompany.Cash;
                    initalMoneyTextBlock.Text = CompanyHelper.currentCompany.Cash.ToString("C");
                }
                if (salesCheckBox.IsChecked.Value)
                {
                    double totalSales = 0;
                    var sales = model.Sales.Where(a => a.Date >= initDateTime && a.Date <= endDateTime);
                    if (sales.Count() > 1)
                    {
                        totalSales = sales.Sum(a => a.Total);
                    }
                    total += totalSales;
                    salesTextBlock.Text = totalSales.ToString("C4");
                }
                if (paymentsCreditSalesCheckBox.IsChecked.Value)
                {
                    double totalPaymentsCreditSales = 0;
                    var salePayments = model.SalePayments.Where(a => a.Sale.IsCredit && a.Date >= initDateTime && a.Date <= endDateTime);
                    if (salePayments.Count() > 1)
                    {
                        totalPaymentsCreditSales = salePayments.Sum(a => a.Payment);
                    }
                    total += totalPaymentsCreditSales;
                    depositCreditSalesTextBlock.Text = totalPaymentsCreditSales.ToString("C4");
                }
                if (purchasesCheckBox.IsChecked.Value)
                {
                    double totalPurchases = 0;
                    var purchases = model.Purchases.Where(a => a.Date >= initDateTime && a.Date <= endDateTime);
                    if (purchases.Count() > 1)
                    {
                        totalPurchases = purchases.Sum(a => a.Total);
                    }
                    total -= totalPurchases;
                    purchasesTextBlock.Text = totalPurchases.ToString("C4");
                }
                if (paymentsPurchasesCheckBox.IsChecked.Value)
                {
                    double totalPaymentsPurchases = 0;
                    var paymentsPurchases = model.PurchasePayments.Where(a => a.Purchase.IsCredit && a.Date >= initDateTime && a.Date <= endDateTime);
                    if (paymentsPurchases.Count() > 1)
                    {
                        totalPaymentsPurchases = paymentsPurchases.Sum(a => a.Payment);
                    }
                    total -= totalPaymentsPurchases;
                    paymentsPurchasesTextBlock.Text = totalPaymentsPurchases.ToString("C4");
                }
                cashRegisterTextBlock.Text = total.ToString("C4");
                boxCutFontAwesome.Text = "Corte a " + total.ToString("C4");
            }
        }
        /// <summary>
        /// Fill utility form's data
        /// </summary>
        private void fillUtilityDetails()
        {
            if (IsLoaded)
            {
                utility = 0;
                totalSalesTextBlock.Text = 
                    totalCreditSalesTextBlock.Text = 
                        costSalesTextBlock.Text =
                            costCreditSalesTextBlock.Text = String.Empty;
                if (totalSalesCheckBox.IsChecked.Value)
                {
                    double totalSales = 0;
                    var sales = model.Sales.Where(a => !a.IsCredit && a.Date >= initDateTime && a.Date <= endDateTime);
                    if (sales.Count() > 1)
                    {
                        totalSales = sales.Sum(a => a.Total);
                    }
                    utility += totalSales;
                    totalSalesTextBlock.Text = totalSales.ToString("C4");

                    double costTotalSales = 0;
                    var products = model.SaleProducts.Where(a => !a.Sale.IsCredit && a.Sale.Date >= initDateTime && a.Sale.Date <= endDateTime);  
                    if(products.Count() > 0)
                        costTotalSales = products.Sum(a => (a.Quantity * a.Product.Cost));
                    costSalesTextBlock.Text = costTotalSales.ToString("C4");
                    utility -= costTotalSales;
                }
                if (totalCreditSalesCheckBox.IsChecked.Value)
                {
                    double totalPaymentsCreditSales = 0;
                    var creditSales = model.Sales.Where(a => a.IsCredit && a.Date >= initDateTime && a.Date <= endDateTime);
                    if (creditSales.Count() > 1)
                    {
                        totalPaymentsCreditSales = creditSales.Sum(a => a.Total);
                    }
                    utility += totalPaymentsCreditSales;
                    totalCreditSalesTextBlock.Text = totalPaymentsCreditSales.ToString("C4");

                    double costCreditTotalSales = 0;
                    var products = model.SaleProducts.Where(a => a.Sale.IsCredit && a.Sale.Date >= initDateTime && a.Sale.Date <= endDateTime);
                    if (products.Count() > 0)
                        costCreditTotalSales = products.Sum(a => (a.Quantity * a.Product.Cost));
                    costCreditSalesTextBlock.Text = costCreditTotalSales.ToString("C4");
                    utility -= costCreditTotalSales;
                }
                utilityTextBlock.Text = utility.ToString("C4");
            }
        }
        #endregion
    }
}
