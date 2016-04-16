using LeposWPF.Helpers;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace LeposWPF.UI.Reports
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : MetroWindow
    {
        #region Declaration
        /// <summary>
        /// Entity framework model object
        /// </summary>
        private Model.LeposWPFModel model = new Model.LeposWPFModel();
        /// <summary>
        /// Used to animate content
        /// </summary>
        public Storyboard storyBoard { get; set; }
        /// <summary>
        /// Data for graphics
        /// </summary>
        private List<dynamic> graphics = null;
        /// <summary>
        /// Start date of report
        /// </summary>
        private String startDate = string.Empty;
        /// <summary>
        /// End date of report
        /// </summary>
        private String endDate = string.Empty;
        /// <summary>
        /// Name of report type
        /// </summary>
        private String reportName = string.Empty;
        /// <summary>
        /// Name for ID column
        /// </summary>
        private String IDColumn = string.Empty;
        /// <summary>
        /// Name for value column
        /// </summary>
        private String valueColumn = string.Empty;
        #endregion
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
            storyBoard = (Storyboard)FindResource("animate");
            alertStackPanel.Width = reportDataGrid.Width = ActualWidth;
            reportDataGrid.Height = dataGridContainerViewBox.ActualHeight;
            var now = DateTime.Now;
            startDatePicker.SelectedDate = new DateTime(now.Year, now.Month, 1);
            endDatePicker.SelectedDate = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));
        }
        /// <summary>
        /// Create report
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void reportButton_Click(object sender, RoutedEventArgs e)
        {
            filterIntegerUpDown.Value = 0;
            fillDataGrid();
        }
        /// <summary>
        /// Graphic view of report
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void chartButton_Click(object sender, RoutedEventArgs e)
        {
            if (reportDataGrid.Items.Count > 0)
            {
                graphics = new List<dynamic>();
                for (int x = 0; x < reportDataGrid.Items.Count; x++)
                {
                    dynamic rowItem = reportDataGrid.Items[x];
                    var valueIDName = IDColumn;
                    var propertyInfo = rowItem.GetType().GetProperty(valueIDName);
                    String ID= propertyInfo.GetValue(rowItem, null).ToString();

                    var valueColumnName = valueColumn;
                    propertyInfo = rowItem.GetType().GetProperty(valueColumnName);
                    String strValue = propertyInfo.GetValue(rowItem, null).ToString();
                    double Value = double.Parse(strValue);

                    graphics.Add(new { ID = ID, Value = Value});
                }
                var chart = new GraphicReport(graphics, " Reporte: " + reportName + " Período : " + startDate + " : " + endDate,reportName);
                Hide();
                chart.ShowDialog();
                ShowDialog();
            }
            else
            {
                displayText("No hay información para crear gráficos",false);
            }
        }
        /// <summary>
        /// Selection changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void typeReportComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (frecuencyReportComboBox != null)
                frecuencyReportComboBox.IsEnabled =  typeReportComboBox.SelectedIndex > 4;
        }
        /// <summary>
        /// Analiza item event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            //graphics = new List<dynamic>();
            //for (int x = 0; x < reportDataGrid.Items.Count; x++)
            //{
            //    graphics.Add(new { ID = DataGridHelper.getTextDG(reportDataGrid, x, IDColumn), Value = DataGridHelper.getTextDG(reportDataGrid, x, valueColumn) });
            //}
            //var chart = new GraphicReport(graphics, " Reporte: " + reportName + " Período : " + startDate + " : " + endDate, reportName);
            //Hide();
            //chart.ShowDialog();
            //ShowDialog();
        }
        /// <summary>
        /// Value changed event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void filterIntegerUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            fillDataGrid();
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
        /// <summary>
        /// Fill datagrid data
        /// </summary>
        private void fillDataGrid()
        {
            if (reportDataGrid == null)
                return;
            reportDataGrid.Columns.Clear();
            reportDataGrid.ItemsSource = null;
            DateTime start = startDatePicker.SelectedDate.Value;
            DateTime end = endDatePicker.SelectedDate.Value;
            if (start < end)
            {
                IEnumerable<dynamic> reportData = null;
                switch (typeReportComboBox.SelectedIndex)
                {
                    //General
                    case 1:

                        double totalSales = model.Sales.Where(a => a.Date >= start && a.Date <= end).Select(a=> a.Total).DefaultIfEmpty(0).Sum();
                        double totalPurchases= model.Purchases.Where(a => a.Date >= start && a.Date <= end).Select(a => a.Total).DefaultIfEmpty(0).Sum();
                        var products = model.SaleProducts.Where(a => a.Sale.Date >= start && a.Sale.Date <= end).GroupBy(a => a.Product).Select(a => new { Producto = a.Key.ID, Cantidad = a.Sum(c => c.Quantity) })
                                         .OrderByDescending(a => a.Cantidad).Take(10).ToList();
                        List<GeneralReportProduct> generalReportProduct = new List<GeneralReportProduct>();
                        products.ForEach(a=> generalReportProduct.Add(new GeneralReportProduct() { ProductID = a.Producto, Quantity = a.Cantidad}));
                        var generalReport = new GeneralReport(start,end, generalReportProduct, totalSales,totalPurchases);
                        Hide();
                        generalReport.ShowDialog();
                        ShowDialog();
                        return;
                    //Products
                    case 2:
                        reportData = model.SaleProducts.Where(a => a.Sale.Date >= start && a.Sale.Date <= end).GroupBy(a => a.Product).Select(a => new { Producto = a.Key.ID, Descripción = a.Key.Description, Cantidad = a.Sum(c => c.Quantity) })
                                    .OrderByDescending(a => a.Cantidad).ToList();
                        break;
                    //Providers
                    case 3:
                        reportData = model.Purchases.Where(a => a.Date >= start && a.Date <= end).GroupBy(a => a.Provider).Select(a => new { ID = a.Key.ID, Nombre = a.Key.Name, Total_Compras = a.Sum(c => c.Total) }).OrderByDescending(a => a.Total_Compras).ToList();
                        break;
                    //Clients
                    case 4:
                        reportData = model.Sales.Where(a => a.Date >= start && a.Date <= end).GroupBy(a => a.Client).Select(a => new { ID = a.Key.ID, Nombre = a.Key.Name, Total_Ventas = a.Sum(c => c.Total) }).OrderByDescending(a => a.Total_Ventas).ToList();
                        break;
                    //Purchases
                    case 5:
                        switch (frecuencyReportComboBox.SelectedIndex)
                        {
                            case 0:
                                displayText("Debe seleccionar una frecuencia para generar el reporte", false);
                                return;
                            //Per year
                            case 1:
                                IDColumn = "Año";
                                valueColumn = "Total_Compras";
                                reportData = model.Purchases.Where(a => a.Date >= start && a.Date <= end).GroupBy(a => a.Date.Year).Select(a => new { Año = a.Key, Total_Compras = a.Sum(c => c.Total) }).OrderByDescending(a => a.Total_Compras).ToList();
                                break;
                            //Per month
                            case 2:
                                IDColumn = "Mes";
                                valueColumn = "Total_Compras";
                                reportData = model.Purchases.Where(a => a.Date >= start && a.Date <= end).GroupBy(a => new { a.Date.Month, a.Date.Year })
                                            .Select(a => new { Mes = a.Key.Month, Año = a.Key.Year, Total_Compras = a.Sum(c => c.Total) }).OrderByDescending(a => a.Total_Compras).ToList()
                                            .Select(a => new { Mes = getMonthDate(a.Mes, a.Año), Total_Compras = a.Total_Compras }).ToList();
                                break;
                            //Per day
                            case 3:
                                IDColumn = "Fecha";
                                valueColumn = "Total_Compras";
                                reportData = model.Purchases.Where(a => a.Date >= start && a.Date <= end).GroupBy(a => new { a.Date.Day, a.Date.Month, a.Date.Year }).Select(a => new { Fecha = a.Key.Day + "/" + a.Key.Month + "/" + a.Key.Year, Total_Compras = a.Sum(c => c.Total) }).OrderByDescending(a => a.Total_Compras).ToList();
                                break;
                        }
                        break;
                    //Sales
                    case 6:
                        switch (frecuencyReportComboBox.SelectedIndex)
                        {
                            case 0:
                                displayText("Debe seleccionar una frecuencia para generar el reporte", false);
                                return;
                            //Per year
                            case 1:
                                IDColumn = "Año";
                                valueColumn = "Total_Ventas";
                                reportData = model.Sales.Where(a => a.Date >= start && a.Date <= end).GroupBy(a => a.Date.Year).Select(a => new { Año = a.Key, Total_Ventas = a.Sum(c => c.Total) }).OrderByDescending(a => a.Total_Ventas).ToList();
                                break;
                            //Per month
                            case 2:
                                IDColumn = "Mes";
                                valueColumn = "Total_Ventas";
                                reportData = model.Sales.Where(a => a.Date >= start && a.Date <= end).GroupBy(a => new { a.Date.Month, a.Date.Year })
                                             .Select(a => new { Mes = a.Key.Month, Año = a.Key.Year, Total_Ventas = a.Sum(c => c.Total) }).OrderByDescending(a => a.Total_Ventas).ToList()
                                             .Select(a=> new { Mes = getMonthDate(a.Mes, a.Año) , Total_Ventas = a.Total_Ventas }).ToList();
                                break;
                            //Per day
                            case 3:
                                IDColumn = "Fecha";
                                valueColumn = "Total_Ventas";
                                reportData = model.Sales.Where(a => a.Date >= start && a.Date <= end).GroupBy(a => new { a.Date.Day, a.Date.Month, a.Date.Year }).Select(a => new { Fecha = a.Key.Day + "/" + a.Key.Month + "/" + a.Key.Year, Total_Ventas = a.Sum(c => c.Total) }).OrderByDescending(a => a.Total_Ventas).ToList();
                                break;
                        }
                        break;
                    //Type of report
                    case 0:
                        displayText("Debe seleccionar un tipo de reporte", false);
                        return;

                }
                filterIntegerUpDown.Maximum = reportData.Count();
                if (filterIntegerUpDown.Value.Value > 0)
                {
                    reportData = reportData.Take(filterIntegerUpDown.Value.Value);
                }
                reportDataGrid.ItemsSource = reportData;
                // Create counter new template column.
                //DataGridTemplateColumn templateColumn = new DataGridTemplateColumn();
                //templateColumn.Header = "Analizar";
                //templateColumn.CellTemplate = (DataTemplate)Resources["analizeTemplate"];
                //reportDataGrid.Columns.Add(templateColumn);
                filterIntegerUpDown.IsEnabled = (reportDataGrid.Items.Count > 0);
                chartButton.IsEnabled = (reportDataGrid.Items.Count > 0) && typeReportComboBox.SelectedIndex > 1;
                switch (typeReportComboBox.SelectedIndex)
                {
                    //Products
                    case 2:
                        IDColumn = "Producto";
                        valueColumn = "Cantidad";
                        break;
                    //Providers
                    case 3:
                        IDColumn = "Nombre";
                        valueColumn = "Total_Compras";
                        break;
                    //Clientes
                    case 4:
                        IDColumn = "Nombre";
                        valueColumn = "Total_Ventas";
                        break;
                }
                this.startDate = start.ToShortDateString();
                this.endDate = end.ToShortDateString();
                this.reportName = typeReportComboBox.Text;
            }
            else
            {
                displayText("La fecha inicial no puede ser superior a la final", false);
            }
        }

        private String getMonthDate(int month, int year)
        {
            return (CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month) + "/" + year);
        }
        #endregion
    }
}
