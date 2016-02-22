using MahApps.Metro.Controls;
using System;
using System.Windows;

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
        #endregion
        #region Constructors
        /// <summary>
        /// Initialize current window
        /// </summary>
        public CashRegister()
        {
            InitializeComponent();
            initDateTime = endDateTime = DateTime.Now;
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

        }
        /// <summary>
        /// Cut money to zero
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void boxCutZeroButton_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Open sales period note
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void salesPeriodNoteButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = new SalesPeriodNote(this,DateTime.Now, DateTime.Now);
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
            this.boxCutFontAwesome.Text = "Corte a " + "$23,423.00";
        }
        /// <summary>
        /// Handle when a CheckBox is checked in the Box Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Handle when a CheckBox is checked in the Utily Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxUtiliy_Checked(object sender, System.Windows.RoutedEventArgs e)
        {

        }
        #endregion
    }
}
