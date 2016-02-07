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

namespace LeposWPF.UI.Reports
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : MetroWindow
    {
        #region Constructors
        /// <summary>
        /// Inititalize new instance of current window
        /// </summary>
        public Reports()
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
            var source = new List<Test>();
            source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
            source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
            source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
            source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
            debtsDataGrid.ItemsSource = source;
            debtsDataGrid.Width = ActualWidth;
            debtsDataGrid.Height = dataGridContainerViewBox.ActualHeight;
        }
        /// <summary>
        /// Create report
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void reportButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

    }
}
