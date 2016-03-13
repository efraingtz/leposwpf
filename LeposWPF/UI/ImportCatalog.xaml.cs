using LeposWPF.Helpers;
using MahApps.Metro.Controls;
using Microsoft.Office.Interop.Excel;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Controls;
using LeposWPF.Model;

namespace LeposWPF.UI
{
    /// <summary>
    /// Interaction logic for ImportCatalog.xaml
    /// </summary>
    public partial class ImportCatalog : MetroWindow
    {
        #region Declaration
        /// <summary>
        /// Increase value for ProgressBar
        /// </summary>
        private double increase = 0;
        /// <summary>
        /// List of data imported
        /// </summary>
        private List<ImportExcelHelper> importExcelHelperList;
        /// <summary>
        /// Entity framework model
        /// </summary>
        private Model.LeposWPFModel model = new Model.LeposWPFModel();
        /// <summary>
        /// Indicates if there is an error loading
        /// </summary>
        private Boolean errorsFlag = false;
        /// <summary>
        /// List of products ids
        /// </summary>
        private List<String> IDProducts;
        #endregion
        #region Constructs
        /// <summary>
        /// Initialize constructrs
        /// </summary>
        /// <param name="window">Owner window</param>
        public ImportCatalog()
        {
            InitializeComponent();
        }
        #endregion
        #region UI Events
        /// <summary>
        /// Importing file event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void importButton_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Window loaded event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            excelDataGrid.Width = ActualWidth;
            excelDataGrid.Height = dataGridContainerViewBox.ActualHeight;
            IDProducts = model.Products.Select(a=> a.ID).ToList();
            excelDataGrid.AutoGeneratingColumn += dataGrid_AutoGeneratingColumn;
        }
        /// <summary>
        /// Browse path for importing file
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            int startRow = 2;
            int totalColumns = 7;
            alertTextBlock.Foreground = Brushes.Green;
            alertTextBlock.Text = "Cargando...";
            loadingProgressBar.Value = 0;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel |*.xlsx";
            openFileDialog.Title = "Importar la información de Excel";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
            {
                importExcelHelperList = new List<ImportExcelHelper>();
                Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();
                if (excelApplication == null)
                {
                    alertTextBlock.Foreground = Brushes.Red;
                    alertTextBlock.Text = "Error: no se ha podido inicial la aplicaión Excel";
                }
                Microsoft.Office.Interop.Excel.Workbook excelBook = excelApplication.Workbooks.Open(openFileDialog.FileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelBook.Worksheets.get_Item(1); ;
                Microsoft.Office.Interop.Excel.Range excelRange = excelSheet.UsedRange;
                
                if (excelRange.Columns.Count == totalColumns)
                {
                    //Load data
                    increase = (double)(50.0 / (double)excelRange.Rows.Count);
                    double progressValue = 0.0;
                    for (int row = startRow; row <= excelRange.Rows.Count; row++)
                    {
                        int x = 0;
                        ImportExcelHelper importExcelHelper = new ImportExcelHelper();
                        importExcelHelper.ID = getCellValueOrEmpty(row, ++x, excelRange);
                        importExcelHelper.Price = getCellValueOrEmpty(row, ++x, excelRange);
                        importExcelHelper.WholeSalePrice = getCellValueOrEmpty(row, ++x, excelRange);
                        importExcelHelper.Cost = getCellValueOrEmpty(row, ++x, excelRange);
                        importExcelHelper.Quantity = getCellValueOrEmpty(row, ++x, excelRange);
                        importExcelHelper.MinimumQuanity = getCellValueOrEmpty(row, ++x, excelRange);
                        importExcelHelper.Description = getCellValueOrEmpty(row, ++x, excelRange);
                        importExcelHelper.Row = row;
                        importExcelHelperList.Add(importExcelHelper);
                        progressValue += increase;
                        Dispatcher.Invoke(
                            System.Windows.Threading.DispatcherPriority.Background,
                            new System.Action(
                                delegate ()
                                {
                                    loadingProgressBar.Value = (int)progressValue;
                                }
                            )
                        );
                    }
                    alertTextBlock.Foreground = Brushes.Green;
                    Dispatcher.Invoke(
                    System.Windows.Threading.DispatcherPriority.Background,
                                new System.Action(
                                    delegate ()
                                    {
                                        alertTextBlock.Text = "Validando...";
                                    }
                                )
                            );
                    //Validate duplicated IDs in file
                    var duplicatedIDs = importExcelHelperList.GroupBy(x => x.ID)
                             .Where(g => g.Count() > 1)
                             .Select(g => g.Key)
                             .ToList();
                    if (duplicatedIDs.Count > 0)
                    {
                        excelDataGrid.ItemsSource = importExcelHelperList.Where(a => duplicatedIDs.Contains(a.ID)).OrderBy(a=> a.ID).ToList();
                        alertTextBlock.Foreground = Brushes.Red;
                        alertTextBlock.Text = "Hay códigos de barras repetidos en el archivo Excel";
                        loadingProgressBar.Value = 100;
                    }
                    else
                    {
                        var duplicatedDataBaseIDs = importExcelHelperList.Where(a=> IDProducts.Contains(a.ID)).ToList();
                        var validateImports = importExcelHelperList.Where(a=> !a.validate()).ToList();
                        if (duplicatedDataBaseIDs.Count > 0)
                        {
                            excelDataGrid.ItemsSource = duplicatedDataBaseIDs;
                            alertTextBlock.Foreground = Brushes.Red;
                            alertTextBlock.Text = "Hay códigos de barras en el archivo Excel que ya existen en la base de datos";
                            loadingProgressBar.Value = 100;
                        }
                        else if (validateImports.Count > 0)
                        {
                            excelDataGrid.ItemsSource = validateImports;
                            alertTextBlock.Foreground = Brushes.Red;
                            alertTextBlock.Text = "Hay registros con datos inválidos en el archivo Excel";
                            loadingProgressBar.Value = 100;
                        }
                        else
                        {
                            excelDataGrid.ItemsSource = null;
                            alertTextBlock.Foreground = Brushes.Green;
                            Dispatcher.Invoke(
                            System.Windows.Threading.DispatcherPriority.Background,
                                new System.Action(
                                    delegate ()
                                    {
                                        alertTextBlock.Text = "Importando...";
                                    }
                                )
                            );
                            foreach (ImportExcelHelper helper in importExcelHelperList)
                            {
                                Product product = new Product();
                                product.ID = helper.ID;
                                product.Description = helper.Description;
                                product.Price = double.Parse(helper.Price);
                                product.WholeSalePrice = double.Parse(helper.WholeSalePrice);
                                product.Cost = double.Parse(helper.Cost);
                                product.Quantity = double.Parse(helper.Quantity);
                                model.Products.Add(product);
                                progressValue += increase;
                                Dispatcher.Invoke(
                                    System.Windows.Threading.DispatcherPriority.Background,
                                    new System.Action(
                                        delegate ()
                                        {
                                            loadingProgressBar.Value = (int)progressValue;
                                        }
                                    )
                                );
                            }
                            model.SaveChanges();
                            alertTextBlock.Text = "La información ha sido importada a la base de datos";
                            loadingProgressBar.Value = 100;
                        }
                    }
                }
                else
                {
                    alertTextBlock.Foreground = Brushes.Red;
                    alertTextBlock.Text = "Las columnas no están asignadas correctamente en el archivo Excel";
                    loadingProgressBar.Value = 100;
                }
                excelBook.Close(true, null, null);
                excelApplication.Quit();
            }
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Get cell value or empty
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Column index</param>
        /// <param name="excelRange">Range of excel file</param>
        /// <returns></returns>
        private string getCellValueOrEmpty(int row, int col, Range excelRange)
        {
            if ((excelRange.Cells[row, col] as Microsoft.Office.Interop.Excel.Range).Value2 != null)
                return (excelRange.Cells[row, col] as Microsoft.Office.Interop.Excel.Range).Value2.ToString();
            return String.Empty;
        }
        /// <summary>
        /// Set cell error
        /// </summary>
        /// <param name="dataRowView">DataRowView of current row</param>
        /// <param name="col">Column index</param>
        private void setErrorDataGridCell(System.Data.DataRowView dataRowView, int col)
        {
            errorsFlag = true;
            var shit = dataRowView.Row[col];
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
        #endregion
    }
}