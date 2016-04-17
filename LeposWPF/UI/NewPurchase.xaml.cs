using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using LeposWPF.Helpers;
using System.Windows.Media.Animation;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Input;
using LeposWPF.Model;
using LeposWPF.Helpers.Clases;
using System.Globalization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace LeposWPF.UI
{
    /// <summary>
    /// Interaction logic for NewPurchase.xaml
    /// </summary>
    public partial class NewPurchase : MetroWindow
    {
        #region Declaration
        /// <summary>
        /// Used to animate content
        /// </summary>
        public Storyboard storyBoard { get; set; }
        /// <summary>
        /// Connection to database
        /// </summary>
        private Model.LeposWPFModel model = new Model.LeposWPFModel();
        /// <summary>
        /// Flag that indicates if an event has happened within the DataGrid object
        /// </summary>
        private Boolean dataGridFuntionFlag = false;
        /// <summary>
        /// Purchase's total
        /// </summary>
        private double Total = 0;
        /// <summary>
        /// Source for DataGrid
        /// </summary>
        private List<PurchaseHelper> purchaseHelperSource;
        #endregion
        #region Constructors
        /// <summary>
        /// Initialize current instance
        /// </summary>
        public NewPurchase()
        {
            InitializeComponent();
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
            purchaseDataGrid.AutoGeneratingColumn += dataGrid_AutoGeneratingColumn;
            var providers = model.Providers.Where(a => a.IsAlive).Select(a => new { ID = a.ID, Name = a.Name }).OrderBy(a => a.Name).ToList();
            providerComboBox.DisplayMemberPath = "Name";
            providerComboBox.ItemsSource = providers;
            if (providerComboBox.Items.Count > 0)
                providerComboBox.SelectedIndex = 0;
            storyBoard = (Storyboard)FindResource("animate");
            searchGrid.Width = ActualWidth;
            purchaseDataGrid.Height = controlGrid.Height = dataGridContainerViewBox.ActualHeight;
            purchaseDataGrid.Width = ActualWidth * 0.8;
            controlGrid.Width = ActualWidth * 0.2;
            searchTextBox.Focus();
            purchaseHelperSource = new List<PurchaseHelper>();
        }
        /// <summary>
        /// Do purchase
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void purchaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (liquidRadioButton.IsChecked.Value)
            {
                validateLiquidePurchase();
            }
            else
            {
                creditPurchase();
            }
        }
        /// <summary>
        /// Update quantity
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void purchaseDataGrid_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F6)
            {
                dataGridFuntionFlag = true;
                if (liquidRadioButton.IsChecked.Value)
                {
                    validateLiquidePurchase();
                }
                else
                {
                    creditPurchase();
                }
            }
        }
        /// <summary>
        /// Listen to keyboard
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void MetroWindow_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F5)
            {
                Window window = new SearchProduct(this);
                Hide();
                window.ShowDialog();
                ShowDialog();
            }
            else if (e.Key == System.Windows.Input.Key.F6 && !dataGridFuntionFlag)
            {
                if (liquidRadioButton.IsChecked.Value)
                {
                    validateLiquidePurchase();
                }
                else
                {
                    creditPurchase();
                }
            }
            if (dataGridFuntionFlag)
                dataGridFuntionFlag = false;
        }
        /// <summary>
        /// Handle remove row event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void removeImage_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            if (image.Tag != null && purchaseDataGrid.SelectedIndex >= 0)
            {
                purchaseHelperSource.RemoveAt(purchaseDataGrid.SelectedIndex);
                updateInfoDataGrid();
                refresDataGridSource();
            }
        }
        /// <summary>
        /// Handle enter event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void searchTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                addProduct(searchTextBox.Text);
        }
        /// <summary>
        /// Change state of price field
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void priceCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            purchaseDataGrid.Columns.Where(a => a.SortMemberPath.Equals("Price")).ToList().ForEach(a=> a.IsReadOnly = !priceCheckBox.IsChecked.Value);
            if (!priceCheckBox.IsChecked.Value)
            {
                purchaseHelperSource.ForEach(a=> a.Price = a.OriginalPrice);
                updateInfoDataGrid();
                refresDataGridSource();
            }
        }

        /// <summary>
        /// Change state of cost field
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void costCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            purchaseDataGrid.Columns.Where(a => a.SortMemberPath.Equals("Cost")).ToList().ForEach(a => a.IsReadOnly = !costCheckBox.IsChecked.Value);
            if (!costCheckBox.IsChecked.Value)
            {
                purchaseHelperSource.ForEach(a => a.Cost = a.OriginalCost);
                updateInfoDataGrid();
                refresDataGridSource();
            }
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Cell edit ending event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void purchaseDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                DataGrid dataGrid = sender as DataGrid;
                var selectedItem = dataGrid.SelectedItem;
                dynamic cellSent = e.Column.GetCellContent(e.Row);
                if (e.EditAction == DataGridEditAction.Commit)
                {
                    if (cellSent is TextBox)
                    {
                        TextBox textBox = cellSent as TextBox;
                        double value = 0;
                        Double.TryParse(textBox.Text, out value);
                        DataGridHelper.setValueToProperty(selectedItem, e.Column.SortMemberPath.ToString(), value);
                        refresDataGridSource();
                        updateInfoDataGrid();
                        e.Cancel = true;
                        dataGrid.CancelEdit();
                    }
                }
            }
            catch (Exception exc)
            {
                System.Text.StringBuilder message = new System.Text.StringBuilder();

                message.Append(exc.Message);

                if (exc.InnerException != null)
                {
                    message.Append("\n" + exc.InnerException.Message);
                }

                displayText(message.ToString(), false);
            }
            finally
            {
                GC.Collect();
            }
        }
        /// <summary>
        /// AutoGeneratingColumn handler for DataGrid
        /// </summary>
        /// <param name="sender">DataGrid object.</param>
        /// <param name="e">Event of sender object.</param>
        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
                DisplayAttribute displayAttribute = (DisplayAttribute)propertyDescriptor.Attributes[typeof(DisplayAttribute)];

                if (displayAttribute.AutoGenerateField)
                {
                    e.Column.Header = displayAttribute.Name;

                    if (displayAttribute.Description == "Template")
                    {
                        // Create counter new template column.
                        DataGridTemplateColumn templateColumn = new DataGridTemplateColumn();
                        templateColumn.Header = displayAttribute.Name;
                        templateColumn.CellTemplate = (DataTemplate)Resources[e.PropertyName + "CellTemplate"];
                        templateColumn.CellEditingTemplate = (DataTemplate)Resources[e.PropertyName + "CellEditingTemplate"];
                        templateColumn.SortMemberPath = e.PropertyName;
                        e.Column = templateColumn;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception exc)
            {
                System.Text.StringBuilder message = new System.Text.StringBuilder();
                message.Append(exc.Message);
                if (exc.InnerException != null)
                {
                    message.Append("\n" + exc.InnerException.Message);
                }
                displayText(message.ToString(), false);
            }
            finally
            {
                GC.Collect();
            }
        }
        /// <summary>
        /// Display alert messages for the user
        /// </summary>
        /// <param name="text">Text to display to the user</param>
        /// <param name="good">Define text color, when true it is green, when bad it is red</param>
        public void displayText(string text, bool good = true)
        {
            WindowHelper.displayText(alertTextBox, loadingProgressBar, storyBoard, text, good);
        }
        /// <summary>
        /// Reset all config values
        /// </summary>
        private void resetPurchase()
        {
            if (providerComboBox.Items.Count > 0)
                providerComboBox.SelectedIndex = 0;
            liquidRadioButton.IsChecked = true;
            setTextBoxesValues(0);
            folioTextBox.Text = String.Empty;
            purchaseHelperSource.Clear();
            refresDataGridSource();
            searchTextBox.Focus();
        }
        /// <summary>
        /// Add a product to DataGrid
        /// </summary>
        /// <param name="ID">ID of product to add</param>
        internal void addProduct(string ID)
        {
            var data = model.Products.Where(a => a.ID == ID).Select(a => new { ID = a.ID, Price = a.Price, Description = a.Description, Cost = a.Cost }).FirstOrDefault();
            if (data != null)
            {
                var searchProduct = purchaseHelperSource.Where(a=> a.Product_ID == data.ID).FirstOrDefault();
                if (searchProduct != null)
                {
                    searchProduct.Quantity+=1;
                }
                else
                {
                    purchaseHelperSource.Add(new PurchaseHelper { Remove = "", Product_ID = data.ID, Description = data.Description, Quantity = 1, Price = data.Price, Cost = data.Cost, OriginalPrice = data.Price, OriginalCost = data.Cost, Amount = data.Cost });
                }
                searchTextBox.Text = string.Empty;
                refresDataGridSource();
                updateInfoDataGrid();
            }
            else
            {
                displayText("El código no es válido", false);
            }
        }
        /// <summary>
        /// Refresh DataGrid's source
        /// </summary>
        private void refresDataGridSource()
        {
            purchaseDataGrid.ItemsSource = null;
            purchaseDataGrid.ItemsSource = purchaseHelperSource;
            purchaseDataGrid.Columns.Where(a => a.SortMemberPath.Equals("Cost")).ToList().ForEach(a => a.IsReadOnly = !costCheckBox.IsChecked.Value);
            purchaseDataGrid.Columns.Where(a => a.SortMemberPath.Equals("Price")).ToList().ForEach(a => a.IsReadOnly = !priceCheckBox.IsChecked.Value);
        }
        /// <summary>
        /// Update info in DataGrid
        /// </summary>
        private void updateInfoDataGrid()
        {
            double preTotal = 0;
            foreach (var item in purchaseHelperSource)
            {
                item.Amount = item.Quantity * item.Cost;
                preTotal += item.Amount;
            }
            setTextBoxesValues(preTotal);
        }

        /// <summary>
        /// Set values for defined fields
        /// </summary>
        /// <param name="iva">Iva value</param>
        /// <param name="subTotal">SubTotal value</param>
        /// <param name="total">Total value</param>
        private void setTextBoxesValues(double preTotal)
        {
            Total = preTotal;
            if (totalTextBlock != null )
            {
                totalTextBlock.Text = "Total : " + Total.ToString("C");
            }
        }
        /// <summary>
        /// Create purchase
        /// </summary>
        /// <param name="isCredit">Flag that indicates whether is a credi purchase or not</param>
        /// <param name="creditDays">Number of credit days</param>
        internal void doPurchase(Boolean isCredit, int creditDays)
        {
            Purchase purchase = new Purchase();
            dynamic selectedprovider = providerComboBox.SelectedItem;
            purchase.CreditDays = creditDays;
            purchase.Folio = folioTextBox.Text;
            purchase.Provider_ID = selectedprovider.ID;
            purchase.Date = DateTime.Now;
            purchase.IsCredit = isCredit;
            purchase.Total = Total;
            purchase.User_ID = UserHelper.loggedUser.ID;
            model.Purchases.Add(purchase);
            model.SaveChanges();
            foreach(var product in purchaseHelperSource)
            {
                var cost = product.Cost;

                //Update product record
                var mappedProduct = model.Products.Where(a => a.ID == product.Product_ID).FirstOrDefault();
                if (mappedProduct != null)
                {
                    mappedProduct.Quantity += product.Quantity;
                    var price = product.Price;
                    if (price != mappedProduct.Price && priceCheckBox.IsChecked.Value)
                    {
                        mappedProduct.Price = price;
                        ProductPrice productPrice = new ProductPrice();
                        productPrice.Date = DateTime.Now;
                        productPrice.Price = price;
                        productPrice.Product_ID = product.Product_ID;
                        productPrice.User_ID = UserHelper.loggedUser.ID;
                        purchase.ProductPrices.Add(productPrice);
                    }
                    if (costCheckBox.IsChecked.Value)
                    {
                        mappedProduct.Cost = cost;
                    }
                }
                //___________________________________________________________
                purchase.PurchaseProducts.Add(new PurchaseProduct { Purchase_ID = purchase.ID, Product_ID = product.Product_ID, Price = cost, Quantity = product.Quantity});
            }
            model.SaveChanges();
            displayText("La compra se ha guardado satisfactoriamente");
            resetPurchase();
        }
        /// <summary>
        /// Validate liquide purchase
        /// </summary>
        private void validateLiquidePurchase()
        {
            if (purchaseDataGrid.Items.Count > 0)
            {
                doPurchase(false, 0);
            }
            else
            {
                displayText("Error: la compra esta vacía", false);
            }
        }
        /// <summary>
        /// Validate credit purchase
        /// </summary>
        private void creditPurchase()
        {
           if (purchaseDataGrid.Items.Count > 0)
           {
               Hide();
               new CreditWindow(this).ShowDialog();
               ShowDialog();
           }
           else
           {
               displayText("Error: la compra esta vacía", false);
           }
        }
        #endregion
    }
}