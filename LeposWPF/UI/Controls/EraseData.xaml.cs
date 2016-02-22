using LeposWPF.Helpers;
using LeposWPF.Helpers.Clases;
using LeposWPF.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LeposWPF.UI.Controls
{
    /// <summary>
    /// Interaction logic for EraseData.xaml
    /// </summary>
    public partial class EraseData : UserControl
    {
        #region Constructors
        /// <summary>
        /// Initialize current instance
        /// </summary>
        public EraseData()
        {
            InitializeComponent();
        }
        #endregion
        #region UI Events
        /// <summary>
        /// Cancel editing data
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            resetValues();
        }
        /// <summary>
        /// Loaded event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            messageTextBlock.Width = logoImage.ActualWidth;
            resetValues();
            logoImage.Source = WindowHelper.getLogo(CompanyHelper.getLogoPath());
        }
        /// <summary>
        /// Saving event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (startDatePicker.SelectedDate == null || endDatePicker.SelectedDate == null)
            {
                messageTextBlock.Foreground = Brushes.Red;
                messageTextBlock.Text = "No estan seleccionadas las fechas";
            }
            else
            {
                LeposWPFModel model = new LeposWPFModel();
                switch (catalogComboBox.SelectedIndex)
                {
                    case 0: // case purchases
                        var purchases = model.Purchases.Where(a=> a.Date >= startDatePicker.SelectedDate && a.Date <= endDatePicker.SelectedDate).ToList();
                        model.Purchases.RemoveRange(purchases);
                        break;
                    case 1: //case sales
                        var sales = model.Sales.Where(a => a.Date >= startDatePicker.SelectedDate && a.Date <= endDatePicker.SelectedDate).ToList();
                        model.Sales.RemoveRange(sales);
                        break;
                }
                messageTextBlock.Foreground = Brushes.Green;
                messageTextBlock.Text = "La información ha sido eliminada";
            }
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Reset values for DatePickers
        /// </summary>
        private void resetValues()
        {
            startDatePicker.SelectedDate = endDatePicker.SelectedDate = DateTime.Now;
            catalogComboBox.SelectedIndex = 0;
        }
        #endregion
    }
}
