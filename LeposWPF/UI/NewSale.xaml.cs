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

namespace LeposWPF.UI
{
    /// <summary>
    /// Interaction logic for NewSale.xaml
    /// </summary>
    public partial class NewSale : MetroWindow
    {
        #region Declaration
        /// <summary>
        /// Flag that indicates if an event has happened within the DataGrid object
        /// </summary>
        private Boolean dataGridFuntionFlag = false;
        /// <summary>
        /// Used to animate content
        /// </summary>
        public Storyboard storyBoard { get; set; }
        /// <summary>
        /// Connection to database
        /// </summary>
        private Model.LeposWPFModel model = new Model.LeposWPFModel();
        /// <summary>
        /// Flag that indicates if a parsing error has happened
        /// </summary>
        private Boolean quantityParseFlag = true;
        /// <summary>
        /// Total value
        /// </summary>
        private double Total= 0;
        /// <summary>
        /// SubTotal value
        /// </summary>
        private double SubTotal= 0;
        /// <summary>
        /// IVA Value
        /// </summary>
        private double IVA= 0;
        /// <summary>
        /// Search general public client
        /// </summary>
        private dynamic generalPublic;
        #endregion
        #region Constructors
        /// <summary>
        /// Initialize current instance
        /// </summary>
        public NewSale()
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
            moneyTextBox.KeyDown += WindowHelper.validateTextBok_KeyDown;
            var clients = model.Clients.Where(a => a.IsAlive).Select(a=> new { ID = a.ID, Name = a.Name, isGeneralPublic = a.IsGeneralPublic})
                .OrderBy(a=> a.Name).ToList();
            clientComboBox.DisplayMemberPath = "Name";
            clientComboBox.ItemsSource = clients;
            if (clientComboBox.Items.Count > 0)
            {
                generalPublic = clients.Where(a => a.isGeneralPublic).FirstOrDefault();
                if (generalPublic != null)
                    clientComboBox.SelectedItem = generalPublic;
                else
                    clientComboBox.SelectedIndex = 0;
            }
            typeIVATextBlock.Text = Helpers.Clases.CompanyHelper.getIVAString(Helpers.Clases.CompanyHelper.currentCompany.IVAType);
            ivaBorder.Visibility = Helpers.Clases.CompanyHelper.currentCompany.IVAType == 0 ? Visibility.Collapsed : Visibility.Visible;
            storyBoard = (Storyboard)FindResource("animate");
            searchGrid.Width = ActualWidth;
            saleDataGrid.Height = controlGrid.Height = dataGridContainerViewBox.ActualHeight;
            saleDataGrid.Width = ActualWidth * 0.8;
            controlGrid.Width = ActualWidth * 0.2;
            showTypeOfSaleControls();
            searchTextBox.Focus();
        }
        /// <summary>
        /// Handle when value changed
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void discountIntegerUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            updateInfoDataGrid();
        }
        /// <summary>
        /// Handle new sale's event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void creditButton_Click(object sender, RoutedEventArgs e)
        {
            creditSale();
        }
        /// <summary>
        /// Update quantity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saleDataGrid_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var text = DataGridHelper.getTextDG(saleDataGrid,saleDataGrid.SelectedIndex,2);
            Boolean isDigit = !(e.Key < System.Windows.Input.Key.D0 || e.Key > System.Windows.Input.Key.D9);
            Boolean singleDot = text.Where(a => a.Equals('.')).Count() == 0;
            Boolean isDot = e.Key == System.Windows.Input.Key.OemPeriod;
            Boolean isBackSpace = e.Key == System.Windows.Input.Key.Back;
            if (e.Key == System.Windows.Input.Key.F6)
            {
                dataGridFuntionFlag = true;
                if (liquidRadioButton.IsChecked.Value)
                {
                    validateLiquideSale();
                }
                else
                {
                    creditSale();
                }
            }
            else if (!((isDot && singleDot) || isBackSpace || isDigit))
            {
                e.Handled = true;
            }
            else
            {
                char code = KeyEventUtility.GetCharFromKey(e.Key);
                if(isBackSpace)
                {
                    if(text.Length > 0)
                        DataGridHelper.setTextDG(saleDataGrid, saleDataGrid.SelectedIndex, 2, text.Substring(0,text.Length-1));
                }
                else
                    DataGridHelper.setTextDG(saleDataGrid, saleDataGrid.SelectedIndex, 2, text + code);
                updateInfoDataGrid();
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
                    moneyTextBox.Focus();
                }
                else
                {
                    creditSale();
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
            if (image.Tag != null && saleDataGrid.SelectedIndex >= 0)
            {
                saleDataGrid.Items.RemoveAt(saleDataGrid.SelectedIndex);
                updateInfoDataGrid();
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
        /// Set prices as wholesale price
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void wholesaleCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            updateInfoDataGrid();
        }
        /// <summary>
        /// Handle when input money is down
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void moneyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Boolean isDigit = !(e.Key < System.Windows.Input.Key.D0 || e.Key > System.Windows.Input.Key.D9);
            Boolean singleDot = textBox.Text.Where(a => a.Equals('.')).Count() == 0;
            Boolean isDot = e.Key == System.Windows.Input.Key.OemPeriod;
            Boolean isBackSpace = e.Key == System.Windows.Input.Key.Back;
            if (e.Key == Key.Enter)
            {
                validateLiquideSale();
            }
            else if (!((isDot && singleDot) || isBackSpace || isDigit))
            {
                e.Handled = true;
            }
            else
            {
                char key = KeyEventUtility.GetCharFromKey(e.Key);
                double money = 0;
                Double.TryParse(textBox.Text+key, out money);
                changeTextBox.Text = (((double)money-Total)).ToString("C");
            }
        }
        /// <summary>
        /// Handle type of sale
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void saleRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            showTypeOfSaleControls();
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
        /// Reset all config values
        /// </summary>
        private void resetSale()
        {
            if (generalPublic != null)
                clientComboBox.SelectedItem = generalPublic;
            else
                clientComboBox.SelectedIndex = 0;
            liquidRadioButton.IsChecked = true;
            wholesaleCheckBox.IsChecked = false;
            discountIntegerUpDown.Value = 0;
            moneyTextBox.Text = changeTextBox.Text = String.Empty;
            setTextBoxesValues(0);
            saleDataGrid.Items.Clear();
            searchTextBox.Focus();
        }
        /// <summary>
        /// Add a product to DataGrid
        /// </summary>
        /// <param name="ID">ID of product to add</param>
        internal void addProduct(string ID)
        {
            var data = model.Products.Where(a => a.ID == ID).Select(a => new { ID = a.ID, Price = a.Price, Description = a.Description, WholeSalePrice = a.WholeSalePrice }).FirstOrDefault();
            if (data != null)
            {
                var quantity = 1;
                var price = data.Price;
                var duplicatedFlag = false;
                for (int x=0; x<saleDataGrid.Items.Count; x++)
                {
                    var product = DataGridHelper.getTextDG(saleDataGrid,x,0);
                    if (product == data.ID)
                    {
                        duplicatedFlag = true;
                        double quantityParse = 1;
                        double.TryParse(DataGridHelper.getTextDG(saleDataGrid, x, 2), out quantityParse);
                        quantityParse+=1;
                        DataGridHelper.setTextDG(saleDataGrid,x,2, quantityParse.ToString());
                        break;
                    }
                }
                searchTextBox.Text = string.Empty;
                if (!duplicatedFlag)
                {
                    saleDataGrid.Items.Add(new { ID = data.ID, Description = data.Description, Quantity = quantity, Price = data.Price, WholeSalePrice = data.WholeSalePrice, Amount = price });
                    setTextBoxesValues(data.Price+Total);
                }
                else
                    updateInfoDataGrid();
            }
            else
            {
                displayText("El código no es válido",false);
            }
        }
        /// <summary>
        /// Update info in DataGrid
        /// </summary>
        private void updateInfoDataGrid()
        {
            var isWholeSale = wholesaleCheckBox.IsChecked.Value;
            double preTotal = 0;
            quantityParseFlag = true;
            for (int x = 0; x < saleDataGrid.Items.Count; x++)
            {
                dynamic product = saleDataGrid.Items[x];
                double quantity = 0;
                if(!double.TryParse(DataGridHelper.getTextDG(saleDataGrid, x, 2), out quantity))
                    quantityParseFlag = false;
                double price = isWholeSale ? product.WholeSalePrice : product.Price;
                DataGridHelper.setTextDG(saleDataGrid, x, 3, price.ToString("C"));
                double amount = price * quantity;
                preTotal += amount;
                DataGridHelper.setTextDG(saleDataGrid, x, 4, amount.ToString("C"));
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
            SubTotal = 0.0;
            Total = 0.0;
            IVA = 0.0;
            switch (Helpers.Clases.CompanyHelper.currentCompany.IVAType)
            {
                case 0:
                    SubTotal = preTotal;
                    Total = preTotal * ((double)(100.0-discountIntegerUpDown.Value.Value)/100.0);
                    break;
                case 1: //IVA Already
                    Total = preTotal * ((double)(100.0 - discountIntegerUpDown.Value.Value) / 100.0);
                    IVA = Total / 1.16 * 0.16;
                    SubTotal = Total - IVA;
                    break;
                case 2: // Add IVA
                    Total = (preTotal * 1.16)*((double)(100.0 - discountIntegerUpDown.Value.Value) / 100.0);
                    IVA = (Total / 1.16) * 0.16;
                    SubTotal = Total - IVA;
                    break;
            }
            if (ivaTextBlock !=null && totalTextBlock != null && subTotalTextBlock != null)
            {
                ivaTextBlock.Text = "IVA : " + IVA.ToString("C4");
                totalTextBlock.Text = "Total : " + Total.ToString("C4");
                subTotalTextBlock.Text = "Subtotal : " + SubTotal.ToString("C");
            }
        }
        /// <summary>
        /// Create sale
        /// </summary>
        /// <param name="isCredit">Flag that indicates whether is a credi sale or not</param>
        /// <param name="creditDays">Number of credit days</param>
        internal void doSale(Boolean isCredit, int creditDays)
        {
            Sale sale = new Sale();
            dynamic selectedClient = clientComboBox.SelectedItem;
            sale.CreditDays = creditDays;
            sale.Client_ID = selectedClient.ID;
            sale.Date = DateTime.Now;
            sale.Discount = discountIntegerUpDown.Value.Value;
            sale.IsCredit = isCredit;
            sale.IVAType = CompanyHelper.currentCompany.IVAType;
            sale.Total = Total;
            sale.SubTotal = SubTotal;
            sale.User_ID = UserHelper.loggedUser.ID;
            sale.IsWholeSale = wholesaleCheckBox.IsChecked.Value;
            model.Sales.Add(sale);
            model.SaveChanges();
            for (int x = 0; x < saleDataGrid.Items.Count; x++)
            {
                dynamic product = saleDataGrid.Items[x];
                var Product_ID = DataGridHelper.getTextDG(saleDataGrid, x, 0);
                var quantity = double.Parse(DataGridHelper.getTextDG(saleDataGrid, x, 2));

                //Update quantity record
                var mappedProduct = model.Products.Where(a => a.ID == Product_ID).FirstOrDefault();
                if (mappedProduct != null)
                    mappedProduct.Quantity -= quantity;

                var price = wholesaleCheckBox.IsChecked.Value ? product.WholeSalePrice : product.Price;
                sale.SaleProducts.Add(new SaleProduct { Sale_ID = sale.ID, Product_ID = Product_ID, Price = price, Quantity = quantity });
            }
            model.SaveChanges();
            if (ticketCheckBox.IsChecked.Value)
            {
                //do ticket shit
                String clientName = selectedClient.Name;
                TicketPOS.TicketVenta(sale, clientName);
            }
            displayText("La venta se ha guardado satisfactoriamente");
            resetSale();
        }
        /// <summary>
        /// Hide or display controls according to radio button
        /// </summary>
        private void showTypeOfSaleControls()
        {
            if (IsLoaded)
            {
                creditButton.Visibility = (liquidRadioButton.IsChecked.Value) ? Visibility.Collapsed : Visibility.Visible;
                changeBorder.Visibility = changeTextBoxBorder.Visibility
                    = cashBorder.Visibility = cashTextBoxBorder.Visibility = (!liquidRadioButton.IsChecked.Value) ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        /// <summary>
        /// Validate liquide sale
        /// </summary>
        private void validateLiquideSale()
        {
            if (quantityParseFlag)
            {
                if (saleDataGrid.Items.Count > 0)
                {
                    double payment = 0;
                    double change = 0;
                    Double.TryParse(moneyTextBox.Text, out payment);
                    Double.TryParse(changeTextBox.Text, out change);
                    if (payment >= Total)
                    {
                        doSale(false, 0);
                    }
                    else
                    {
                        displayText("Error: verificar el pago", false);
                    }
                }
                else
                {
                    displayText("Error: la venta esta vacía", false);
                }
            }
            else
            {
                displayText("Error: no todas las cantidades de la venta tienen valores correctos", false);
            }
        }
        /// <summary>
        /// Validate credit sale
        /// </summary>
        private void creditSale()
        {
            if (quantityParseFlag)
            {
                if (saleDataGrid.Items.Count > 0)
                {
                    Hide();
                    new CreditWindow(this).ShowDialog();
                    ShowDialog();
                }
                else
                {
                    displayText("Error: la venta esta vacía", false);
                }
            }
            else
            {
                displayText("Error: no todas las cantidades de la venta tienen valores correctos", false);
            }
        }
        #endregion 
    }
}