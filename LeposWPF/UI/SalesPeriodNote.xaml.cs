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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
        /// Connection to entity framework model
        /// </summary>
        private LeposWPFModel model = new LeposWPFModel();
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
        /// <summary>
        /// Used to animate content
        /// </summary>
        public Storyboard storyBoard
        {
            get;
            set;
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
            storyBoard = (Storyboard)FindResource("animate");
            displayText("Obteniendo información");
            var products = model.SaleProducts.Where(a => model.Sales.Where(s => s.Date >= startDate && s.Date <= endDate).Select(v=> v.ID)
                           .Contains(a.Sale_ID))
                           .GroupBy(a=> new {
                                              a.Product,
                                              a.Price,
                                              a.Sale.IsWholeSale,
                                              a.Sale.IVAType,
                                              a.Sale.Discount})
                           .Select(a => new { Product_ID = a.Key.Product.ID,
                                              Description = a.Key.
                                              Product.Description,
                                              Quantity = a.Sum(t=> t.Quantity),
                                              Price = a.Key.Price,
                                              Discount = (a.Key.Discount),
                                              IVA = a.Key.IVAType,
                                              Amount = (a.Key.Price * a.Sum(t => t.Quantity)),
                                              IsWholeSale = a.Key.IsWholeSale })
                           .ToList()
                           .Select(a => new { Producto = a.Product_ID,
                                              Descripción = a.Description,
                                              Mayoreo = a.IsWholeSale,
                                              Cantidad = a.Quantity,
                                              Precio = a.IVA == 2 ? 
                                                       (a.Price * 1.16 * ((double)(100.0 - a.Discount) / 100.0)).ToString("C4") 
                                                       : 
                                                       (a.Price * ((double)(100.0 - a.Discount) / 100.0)).ToString("C4"),
                                              Descuento = (a.Discount / 100.0).ToString("p"),
                                              Importe = a.IVA == 2 ? ((a.Price * ((double)(100.0 - a.Discount) / 100.0) * 1.16) * a.Quantity).ToString("C4") 
                                                        : 
                                                        (a.Price * ((double)(100.0 - a.Discount) / 100.0) * a.Quantity).ToString("C4"),
                                              IVA = Helpers.Clases.CompanyHelper.getIVAString(a.IVA)})
                           .OrderBy(a=> a.Producto)
                           .ThenBy(a=> a.Mayoreo)
                           .ThenByDescending(a=> a.IVA)
                           .ThenByDescending(a=> a.Importe)
                           .ToList();
            var sales= model.Sales.Where(s => s.Date >= startDate && s.Date <= endDate);
            double total = 0;
            if (sales.Count() > 0)
            {
                total = model.Sales.Where(s => s.Date >= startDate && s.Date <= endDate)
                                .Sum(a => a.Total);
            }
            salesTotalTextBlock.Text = "Total : " + total.ToString("C4");
            periodTextBlock.Text =  Title += startDate.ToShortDateString() + " - " + endDate.ToShortDateString();
            productsDataGrid.ItemsSource = products;
            productsDataGrid.Height = dataGridContainerViewBox.ActualHeight;
            productsDataGrid.Width  = loadingProgressBar.Width = ActualWidth;
        }
        /// <summary>
        /// Export current note to word file
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void exportExcelButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel |*.xlsx";
            saveFileDialog.Title = "Exportar la información a Excel";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                displayText("Exportando...");
                Dispatcher.Invoke(
                    System.Windows.Threading.DispatcherPriority.Background,
                        new Action(
                            delegate ()
                            {
                                ExcelHelper excelHelper = new ExcelHelper(saveFileDialog.FileName, productsDataGrid, startDate, endDate);
                                KeyValuePair<String, Boolean> keyValuePair = excelHelper.createExcelFile();
                                displayText(keyValuePair.Key, keyValuePair.Value);
                        }
                    )
                );
            }
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Display alert messages for the user
        /// </summary>
        /// <param name="text">Text to display to the user</param>
        /// <param name="good">Define text color, when true it is green, when bad it is red</param>
        public void displayText(string text, bool good = true)
        {
            WindowHelper.displayText(alertTextBlock, loadingProgressBar, storyBoard, text, good);
        }
        #endregion
    }
}
