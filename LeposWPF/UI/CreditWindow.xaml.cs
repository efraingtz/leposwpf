using LeposWPF.Helpers;
using LeposWPF.Helpers.Clases;
using LeposWPF.Model;
using MahApps.Metro.Controls;
using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace LeposWPF.UI
{
    /// <summary>
    /// Interaction logic for CreditWindow.xaml
    /// </summary>
    public partial class CreditWindow : MetroWindow
    {
        #region Constructors
        /// <summary>
        /// Initialize current instance
        /// </summary>
        /// <param name="window">Owner window</param>
        public CreditWindow(Window window)
        {
            this.Owner = window;
            InitializeComponent();
            logoImage.Source = WindowHelper.getLogo(CompanyHelper.getLogoPath());
        }
        #endregion
        #region UI Events
        /// <summary>
        /// Window loaded
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dateDatePicker.SelectedDate = DateTime.Now;
        }
        /// <summary>
        /// Handle log in event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void paymentButton_Click(object sender, RoutedEventArgs e)
        {
            if (dateDatePicker.SelectedDate >= DateTime.Now)
            {
                doCredit();
                Close();
            }
            else
            {
                errorTextBlock.Text = "Error: verificar la fecha de pago";   
            }
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Handle credit payment
        /// </summary>
        private void doCredit()
        {
            int days = (dateDatePicker.SelectedDate.Value.Date - DateTime.Now.Date).Days;
            if (Owner is NewPurchase)
            {
                NewPurchase newPurchase = Owner as NewPurchase;
                newPurchase.doPurchase(true,days);
            }
            else if (Owner is NewSale)
            {
                NewSale newSale = Owner as NewSale;
                newSale.doSale(true, days);
            }
        }
        #endregion
    }
}
