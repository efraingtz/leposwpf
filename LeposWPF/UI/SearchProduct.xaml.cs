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
    /// Interaction logic for SearchProduct.xaml
    /// </summary>
    public partial class SearchProduct : MetroWindow
    {
        #region Declaration
        /// <summary>
        /// Used to animate content
        /// </summary>
        public Storyboard storyBoard { get; set; }
        /// <summary>
        /// Connection to database
        /// </summary>
        private LeposWPFModel model = new LeposWPFModel();
        #endregion
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
        /// Handle enter event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                var products = model.Products.Where(a => a.IsAlive
                                                    && (a.Description.ToUpper().Contains(searchTextBox.Text.ToUpper()) || a.ID.ToUpper().Contains(searchTextBox.Text.ToUpper())))
                                                    .Select(a => new { ID = a.ID, Descripción = a.Description, Precio = a.Price, Cantidad = a.Quantity })
                                                    .OrderBy(a => a.ID)
                                                    .ToList();
                if (products.Count > 0)
                {
                    productsDataGrid.ItemsSource = products;
                    productsDataGrid.Columns.ToList().ForEach(a => a.IsReadOnly = true);
                }
                else
                {
                    displayText("No se han encontrado resultados", false);
                }
            }
        }
        /// <summary>
        /// Handle double click mouse event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void productsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dynamic selectedItem = productsDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                string ID = selectedItem.ID;
                if (Owner is NewSale)
                {
                    var newSale = Owner as NewSale;
                    newSale.addProduct(ID);
                    Close();
                }
                else if (Owner is NewPurchase)
                {
                    var newPurchase = Owner as NewPurchase;
                    newPurchase.addProduct(ID);
                    Close();
                }
            }
        }
        /// <summary>
        /// Loaded window event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            productsDataGrid.Height = dataGridContainerViewBox.ActualHeight;
            productsDataGrid.Width = searchGrid.Width = ActualWidth;
            storyBoard = (Storyboard)FindResource("animate");
        }
        #endregion
        /// <summary>
        /// Display alert messages for the user
        /// </summary>
        /// <param name="text">Text to display to the user</param>
        /// <param name="good">Define text color, when true it is green, when bad it is red</param>
        public void displayText(string text, bool good = true)
        {
            WindowHelper.displayText(alertTextBox, loadingProgressBar, storyBoard, text, good);
        }
    }
}
