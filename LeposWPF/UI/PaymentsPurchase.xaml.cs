using LeposWPF.Helpers;
using LeposWPF.Helpers.Clases;
using LeposWPF.Model;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LeposWPF.UI
{
    /// <summary>
    /// Interaction logic for PaymentsPurchase.xaml
    /// </summary>
    public partial class PaymentsPurchase : MetroWindow
    {
        #region Declaration
        /// <summary>
        /// Connection to entity framework model
        /// </summary>
        private LeposWPFModel Model;
        /// <summary>
        /// Purchase to do payment
        /// </summary>
        private Purchase Purchase;
        #endregion
        #region Constructors
       /// <summary>
       /// Initialize current instance
       /// </summary>
       /// <param name="window">Owner window</param>
       /// <param name="purchase">Pruchase to do payment</param>
       /// <param name="model">Data model</param>
        public PaymentsPurchase(Window window, Purchase purchase, LeposWPFModel model)
        {
            this.Model = model;
            this.Purchase = purchase;
            this.Owner = window;
            InitializeComponent();
        }
        #endregion
        #region UI Events
        /// <summary>
        /// Handle payment event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void payButton_Click(object sender, RoutedEventArgs e)
        {
            errorTextBlock.Text = String.Empty;
            double parseValue = 0;
            Boolean parseFlag = double.TryParse(paymentTextBox.Text, out parseValue);
            if (parseFlag)
            {
                if (parseValue <= (Purchase.Total - Purchase.Payments))
                {
                    PurchasePayment payment = new PurchasePayment();
                    payment.Date = DateTime.Now;
                    payment.Payment = parseValue;
                    payment.Purchase_ID = Purchase.ID;
                    payment.User_ID = UserHelper.loggedUser.ID;
                    Purchase.PurchasePayments.Add(payment);
                    Model.SaveChanges();
                    paymentsDataGrid.Items.Refresh();
                    errorTextBlock.Foreground = Brushes.Green;
                    paymentTextBox.Text = String.Empty;
                    errorTextBlock.Text = "Información guardada";
                    fillDetails();
                }
                else
                {
                    errorTextBlock.Foreground = Brushes.Red;
                    errorTextBlock.Text = "Error: el abono es superior a la deuda";
                }
            }
            else
            {
                errorTextBlock.Foreground = Brushes.Red;
                errorTextBlock.Text = "Error en el formato del dinero";
            }
        }
        /// <summary>
        /// Loaded window
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            fillDetails();

            paymentsDataGrid.Width = ActualWidth;
            paymentsDataGrid.AutoGeneratingColumn += dataGrid_AutoGeneratingColumn;
            paymentsDataGrid.ItemsSource = Purchase.PurchasePayments;
            paymentsDataGrid.Height = dataGridContainerViewBox.ActualHeight;
            paymentsDataGrid.Width = ActualWidth;
        }
        #endregion
        #region Helpers
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
        /// Handle event when a key is down 
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void paymentTextbox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Boolean isDigit = !(e.Key < System.Windows.Input.Key.D0 || e.Key > System.Windows.Input.Key.D9);
            Boolean singleDot = paymentTextBox.Text.Where(a => a.Equals('.')).Count() == 0;
            Boolean isDot = e.Key == System.Windows.Input.Key.OemPeriod;
            Boolean isBackSpace = e.Key == System.Windows.Input.Key.Back;
            if (!((isDot && singleDot) || isBackSpace || isDigit))
            {
                e.Handled = true;
            }
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Fill details of purchase
        /// </summary>
        private void fillDetails()
        {
            sellerTextBox.Text = Purchase.User_ID;
            paymentsTextBox.Text = Purchase.Payments.ToString("C");
            purchaseIDTextBox.Text = Purchase.ID.ToString();
            providerTextBox.Text = Purchase.Provider.Name;
            debtTextBox.Text = (Purchase.Total - Purchase.Payments).ToString("C");
            folioTextBox.Text = Purchase.Folio;
            costTextBox.Text = Purchase.Total.ToString("C");
        }
        #endregion
    }
}
