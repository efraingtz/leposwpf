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
    /// Interaction logic for SearchProduct.xaml
    /// </summary>
    public partial class SearchProduct : MetroWindow
    {
        #region Constructors
        /// <summary>
        /// Initialize current window
        /// </summary>
        /// <param name="window">Owner window</param>
        public SearchProduct(Window window)
        {
            this.Owner = window;
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
            productsDataGrid.ItemsSource = source;
            productsDataGrid.Height = dataGridContainerViewBox.ActualHeight;
            productsDataGrid.Width = searchGrid.Width = ActualWidth;
        }
        #endregion
    }
}
