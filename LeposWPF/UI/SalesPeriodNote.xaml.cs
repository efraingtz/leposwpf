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
    /// Interaction logic for SalesPeriodNote.xaml
    /// </summary>
    public partial class SalesPeriodNote: MetroWindow
    {
        #region Declaration
        /// <summary>
        /// Start DateTime to create sale's note
        /// </summary>
        public DateTime endDate
        {
            get;
            private set;
        }
        /// <summary>
        /// End DateTime to create sale's note
        /// </summary>
        public DateTime startDate
        {
            get;
            private set;
        }
        #endregion
        #region Constructors
        /// <summary>
        /// Initialize current window
        /// </summary>
        /// <param name="window">Owner window</param>
        /// /// <param name="startDate">Start DateTime to create sale's note</param>
        /// /// <param name="endDate">End DateTime to create sale's note</param>
        public SalesPeriodNote(Window window, DateTime startDate, DateTime endDate)
        {
            this.Owner = window;
            this.startDate = startDate;
            this.endDate = endDate;
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
            periodTextBlock.Text =  Title += startDate.ToShortDateString() + " - " + endDate.ToShortDateString();
            var source = new List<Test>();
            source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
            source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
            source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
            source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
            productsDataGrid.ItemsSource = source;
            productsDataGrid.Height = dataGridContainerViewBox.ActualHeight;
            productsDataGrid.Width  = loadingProgressBar.Width = ActualWidth;
        }
        /// <summary>
        /// Export current note to word file
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void exportWordButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
