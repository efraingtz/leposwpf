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
    /// Interaction logic for PaymentsSale.xaml
    /// </summary>
    public partial class PaymentsSale : MetroWindow
    {
        #region Declaration
        /// <summary>
        /// Connection to entity framework model
        /// </summary>
        private LeposWPFModel Model;
        /// <summary>
        /// Sale to do payment
        /// </summary>
        private Sale Sale;
        #endregion
        #region Constructors
       /// <summary>
       /// Initialize current instance
       /// </summary>
       /// <param name="window">Owner window</param>
       /// <param name="sale">Sale to do payment</param>
       /// <param name="model">Data model</param>
        public PaymentsSale(Window window, Sale sale, LeposWPFModel model)
        {
            this.Model = model;
            this.Sale = sale;
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
                if (parseValue > 0)
                {
                    float debt = (float)(Sale.Total - Sale.Payments);
                    if (parseValue <= debt)
                    {
                        SalePayment payment = new SalePayment();
                        payment.Date = DateTime.Now;
                        payment.Payment = parseValue;
                        payment.Sale_ID = Sale.ID;
                        payment.User_ID = UserHelper.loggedUser.ID;
                        Sale.SalePayments.Add(payment);
                        Model.SaveChanges();
                        paymentsDataGrid.Items.Refresh();
                        errorTextBlock.Foreground = Brushes.Green;
                        paymentTextBox.Text = String.Empty;
                        errorTextBlock.Text = "Información guardada";
                        fillDetails();
                        if (ticketCheckBox.IsChecked.Value)
                        {
                            //do ticket shit
                            String clientName = Sale.Client.Name;
                            TicketPOS.TicketAbonoVentaCredito(payment, Sale, debt , clientName);
                        }
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
                    errorTextBlock.Text = "Error: el abono debe ser mayor a cero";
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
            paymentsDataGrid.ItemsSource = Sale.SalePayments;
            paymentsDataGrid.Height = dataGridContainerViewBox.ActualHeight;
            paymentsDataGrid.Width = ActualWidth;
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
        /// Fill details of Sale
        /// </summary>
        private void fillDetails()
        {
            sellerTextBox.Text = Sale.User_ID;
            paymentsTextBox.Text = Sale.Payments.ToString("C");
            saleIDTextBox.Text = Sale.ID.ToString();
            clientTextBox.Text = Sale.Client.Name;
            debtTextBox.Text = (Sale.Total - Sale.Payments).ToString("C");
            costTextBox.Text = Sale.Total.ToString("C");
        }
        #endregion
    }
}
