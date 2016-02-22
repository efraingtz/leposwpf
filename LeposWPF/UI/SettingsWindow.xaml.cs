using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media.Animation;
using MahApps.Metro.Controls;
using LeposWPF.Model;
using LeposWPF.Helpers;
using LeposWPF.Helpers.Clases;

namespace LeposWPF.UI
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : MetroWindow, DataGridInterface
    {
        #region Declaration
        /// <summary>
        /// Store last entity object added ID
        /// </summary>
        private dynamic lastEntityID = -1;
        /// <summary>
        /// Used to animate content
        /// </summary>
        public Storyboard storyBoard { get; set; }
        /// <summary>
        /// Local instance of entity framework model
        /// </summary>
        public LeposWPFModel conn { get; private set; }
        /// <summary>
        /// Current DataGrid
        /// </summary>
        private DataGrid currentDataGrid;
        /// <summary>
        /// Last value edited from DataGrid cell
        /// </summary>
        private dynamic lastValueEdited;
        /// <summary>
        /// Last DataGrid index selected
        /// </summary>
        private int lastIndex;
        /// <summary>
        /// Tag of current button selected
        /// </summary>
        private String dataGridTag;
        /// <summary>
        /// Collections used to fill ComboBox items
        /// </summary>
        private CollectionViewSource collectionViewSource, auxiliarCollectionViewSource;
        #endregion
        #region Constructors
        /// <summary>
        /// SettingsWindow Initialization
        /// </summary>
        public SettingsWindow()
        {
            InitializeComponent();
        }
        #endregion
        #region Interface DataGridInterface
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
        /// Bind current control to static variable
        /// </summary>
        public void initInterface()
        {
            DataGridHelper.dataGrid = this;
        }

        /// <summary>
        /// Fill DataGrid content
        /// </summary>
        public void fillDataGrid()
        {
            try
            {
                mainDataGrid.Children.Clear();
                DataGrid dataGridDynamic = currentDataGrid = new DataGrid();
                dataGridDynamic.MaxColumnWidth = 200;
                dataGridDynamic.Height = dataGridContainerViewBox.ActualHeight;
                dataGridDynamic.Width = ActualWidth;
                dataGridDynamic.HeadersVisibility = DataGridHeadersVisibility.Column;
                dataGridDynamic.SelectionMode = DataGridSelectionMode.Single;
                dataGridDynamic.Background = Brushes.White;
                dataGridDynamic.AlternationCount = 2;
                dataGridDynamic.AutoGeneratingColumn += DataGridHelper.dataGrid_AutoGeneratingColumn;
                dataGridDynamic.RowEditEnding += dataGridDynamic_RowEditEnding;
                dataGridDynamic.CellEditEnding += dataGridDynamic_CellEditEnding;
                setContentDataGrid();
                dataGridDynamic.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                dataGridDynamic.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                //dataGridDynamic.CellStyle = (Style)FindResource("dataGridCellStyle");
                //dataGridDynamic.RowStyle = (Style)FindResource("dataGridRowStyle");
                currentDataGrid = dataGridDynamic;
                if (lastEntityID is int)
                {
                    if (lastEntityID != -1)
                    {
                        scrollDataGrid();
                    }
                }
                else scrollDataGrid();
                mainDataGrid.Children.Add(dataGridDynamic);
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
        /// Retrieve last index clicked in DataGrid
        /// </summary>
        /// <returns>Last index clicked in DataGrid</returns>
        public String getIndex()
        {
            return dataGridTag;
        }

        /// <summary>
        /// Retrieve last cell edited index
        /// </summary>
        /// <returns>Cell index</returns>
        public int getLastIndex()
        {
            return lastIndex;
        }

        /// <summary>
        /// Retrieve last cell edited value
        /// </summary>
        /// <returns>Cell value</returns>
        public Object getLastValueEdited()
        {
            return lastValueEdited;
        }

        /// <summary>
        /// Retrieve current connection to database
        /// </summary>
        /// <returns>Current connection</returns>
        public LeposWPFModel getConn()
        {
            return conn;
        }

        /// <summary>
        /// Retrieve current control resources
        /// </summary>
        /// <returns>Control resources</returns>
        public ResourceDictionary getResources()
        {
            return Resources;
        }

        #endregion
        #region UI Events
        /// <summary>
        /// Handle click within TextBlock
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void AccountStatus_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            int ID = int.Parse(textBlock.Tag.ToString());
            var client = conn.Clients.Where(a=> a.ID == ID).FirstOrDefault();
            if (client != null)
            {
                Window window = new AccountStatus(this, client);
                Hide();
                window.ShowDialog();
                ShowDialog();
            }
            else displayText("Error: El cliente aún no es guardado en la base de datos",false);
        }
        /// <summary>
        /// Window loaded event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            collectionViewSource = (CollectionViewSource)FindResource("collectionViewSource");
            auxiliarCollectionViewSource = (CollectionViewSource)FindResource("auxiliarCollectionViewSource");
            conn = new LeposWPFModel();
            storyBoard = (Storyboard)FindResource("animate");
            initInterface();
            dataGridTag = productsButton.Tag.ToString();
            displayText("Obteniendo información...");
            fillDataGrid();
        }
        /// <summary>
        /// Updates progress bar when value is changed
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void loadingProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.UserInterfaceRefresh();
        }

        /// <summary>
        /// Close Windows
        /// </summary>
        /// <param name="sender">Window to be closed</param>
        /// <param name="e">Event fired when closing windows</param>
        private void closeSettingsWindows(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Handle delete event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event of sender object.</param>
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataGridHelper.deleteItem(currentDataGrid);
        }
        /// <summary>
        /// Open import catalog window
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void importCatalogButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = new ImportCatalog(this);
            Hide();
            window.ShowDialog();
            ShowDialog();
        }
        /// <summary>
        /// Change content of DataGrid according to an entity
        /// </summary>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lastEntityID = -1;
                Button button = sender as Button;
                dataGridTag = button.Tag.ToString();
                displayText("Obteniendo información...");
                fillDataGrid();
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
        #endregion
        #region Handle DataGrid

        /// <summary>
        /// Scroll into last record added in DataGrid
        /// </summary>
        private void scrollDataGrid()
        {
            int x = 0;
            foreach (var element in currentDataGrid.ItemsSource)
            {
                dynamic dynamicElement = element;
                Boolean flagElement = false;
                if (lastEntityID is String)
                    flagElement = dynamicElement.ID.ToString() == lastEntityID.ToString();
                else if (lastEntityID is int)
                    flagElement = dynamicElement.ID == lastEntityID;
                if (flagElement)
                {
                    currentDataGrid.SelectedIndex = x;
                    currentDataGrid.ScrollIntoView(currentDataGrid.Items[x]);
                    break;
                }
                x++;
            }
        }

        /// <summary>
        /// Set primary content to display
        /// </summary>
        private void setContentDataGrid()
        {
            try
            {
                currentDataGrid.Tag = dataGridTag;
                dynamic obj = null;
                dynamic sourceDataGrid = null;
                switch (dataGridTag)
                {
                    case "Products":
                        var products = conn.Products.Where(a=> a.IsAlive).ToList();
                        currentDataGrid.ItemsSource = sourceDataGrid = products;
                        obj = new Product();
                        break;
                    case "Clients":
                        var clients = conn.Clients.Where(a => a.IsAlive).ToList();
                        currentDataGrid.ItemsSource = sourceDataGrid = clients;
                        obj = new Client();
                        break;
                    case "Providers":
                        var providers = conn.Providers.Where(a => a.IsAlive).ToList();
                        currentDataGrid.ItemsSource = sourceDataGrid = providers;
                        obj = new Provider();
                        break;
                    case "Users":
                        var users = conn.Users.Where(a => a.IsAlive).ToList();
                        currentDataGrid.ItemsSource = sourceDataGrid = users;
                        collectionViewSource.Source = new[]
                            {
                                new { ID = 0 , Name = "Administrador"  },
                                new { ID = 1 , Name = "Vendedor de mostrador"  }
                            }.ToList();
                        obj = new User();
                        break;
                }
                if (currentDataGrid.Items.Count <= 1)
                {
                    sourceDataGrid = currentDataGrid.ItemsSource;
                    sourceDataGrid.Add(obj);
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
        /// Setup data when finish editing cell on DataGrid.
        /// </summary>
        /// <param name="sender">DataGrid handled</param>
        /// <param name="e">Event of sender object.</param>
        private void dataGridDynamic_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                DataGrid dataGrid = sender as DataGrid;
                dynamic cellSent = e.Column.GetCellContent(e.Row);

                if (cellSent is TextBox)
                {
                    TextBox textBlock = cellSent as TextBox;
                    this.lastValueEdited = textBlock.Text;
                    this.lastIndex = e.Column.DisplayIndex;
                }

                if (dataGrid.Tag != null)
                {
                    var selectedItem = dataGrid.SelectedItem;

                    if (e.EditAction == DataGridEditAction.Commit)
                    {
                        if (e.Column.GetType().Name == "DataGridTemplateColumn")
                        {
                            ContentPresenter contentPresenter = (ContentPresenter)e.Column.GetCellContent(e.Row);
                            DataTemplate dataTemplate = contentPresenter.ContentTemplate;
                            PropertyInfo[] propertyInfos = selectedItem.GetType().GetProperties();

                            foreach (PropertyInfo propertyInfo in propertyInfos)
                            {
                                String name = propertyInfo.Name;
                                ComboBox comboBox = (dataTemplate.FindName(name, contentPresenter) as ComboBox);
                                if (comboBox != null)
                                {
                                    String dbSet = comboBox.Uid;
                                    String property = comboBox.Tag.ToString();
                                    object ID = comboBox.SelectedValue;
                                    EF.SetProperty(selectedItem, name, ID);
                                    break;
                                }
                            }
                        }
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
        /// Handle new objects added or edited
        /// </summary>
        /// <param name="sender">Data grid handled</param>
        /// <param name="e">Event fired when finishing row editing</param>
        private void dataGridDynamic_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                DataGrid dataGrid = sender as DataGrid;
                // Only act on Commit
                if (e.EditAction == DataGridEditAction.Commit)
                {
                    dynamic entitiesDbSet = DataGridHelper.getDataSetEntities();
                    int row = e.Row.GetIndex();
                    int count = entitiesDbSet.Local.Count;
                    dynamic dynamicObject = dataGrid.SelectedItem;
                    dynamic objectEntity = Activator.CreateInstance(dynamicObject.GetType());
                    bool flagNew = (dynamicObject.ID == null);

                    if (!flagNew)
                    {
                        flagNew = (dynamicObject.ID is string) ? dynamicObject.ID == "" : dynamicObject.ID == 0;
                    }

                    if (entitiesDbSet != null)
                    {
                        object[] values = new object[] { };
                        String[] properties = new String[] { };
                        String[] propertiesForeign = new String[] { };
                        if (dynamicObject is Client)
                        {
                            var name = DataGridHelper.getTextDG(dataGrid, row, 0);
                            var rfc = DataGridHelper.getTextDG(dataGrid, row, 1);
                            var address = DataGridHelper.getTextDG(dataGrid, row, 2);
                            var phone = DataGridHelper.getTextDG(dataGrid, row, 3);
                            var email = DataGridHelper.getTextDG(dataGrid, row, 4);
                            values = new object[] { name, rfc, address, phone, email};
                            properties = new String[] { "Name", "RFC", "Address", "Phone", "Email" };
                            propertiesForeign = new String[] { };
                            DataGridHelper.HandleAddUpdate(values, properties, propertiesForeign, e, flagNew, dynamicObject, objectEntity, false);
                        }
                        else if (dynamicObject is Provider)
                        {
                            var name = DataGridHelper.getTextDG(dataGrid, row, 0);
                            var rfc = DataGridHelper.getTextDG(dataGrid, row, 1);
                            var address = DataGridHelper.getTextDG(dataGrid, row, 2);
                            var phone = DataGridHelper.getTextDG(dataGrid, row, 3);
                            var email = DataGridHelper.getTextDG(dataGrid, row, 4);
                            values = new object[] { name, rfc, address, phone, email };
                            properties = new String[] { "Name", "RFC", "Address", "Phone", "Email" };
                            propertiesForeign = new String[] { };
                            DataGridHelper.HandleAddUpdate(values, properties, propertiesForeign, e, flagNew, dynamicObject, objectEntity, false);
                        }
                        else if (dynamicObject is User)
                        {
                            var ID = DataGridHelper.getTextDG(dataGrid, row, 0);
                            var type = dynamicObject.Type;
                            var password = flagNew ? UserHelper.generateRandomPassword() : dynamicObject.Password;
                            values = new object[] {ID,type ,password};
                            properties = new String[] { "ID","Type","Password" };
                            propertiesForeign = new String[] { };
                            DataGridHelper.HandleAddUpdate(values, properties, propertiesForeign, e, flagNew, dynamicObject, objectEntity, false, 1);
                        }
                        else if (dynamicObject is Product)
                        {
                            var ID = DataGridHelper.getTextDG(dataGrid, row, 0);
                            var price = DataGridHelper.getDoubleDG(dataGrid, row, 1);
                            var wholeSalePrice = DataGridHelper.getDoubleDG(dataGrid, row, 2);
                            var cost = DataGridHelper.getDoubleDG(dataGrid, row, 3);
                            var quantity = DataGridHelper.getDoubleDG(dataGrid, row, 4);
                            var minimumQuantity = DataGridHelper.getDoubleDG(dataGrid, row, 5);
                            var description = DataGridHelper.getTextDG(dataGrid, row, 6);
                            values = new object[] { ID, price,wholeSalePrice,cost,quantity,minimumQuantity,description };
                            properties = new String[] { "ID", "Price","WholeSalePrice","Cost","Quantity", "MinimumQuanity", "Description"};
                            propertiesForeign = new String[] { };
                            DataGridHelper.HandleAddUpdate(values, properties, propertiesForeign, e, flagNew, dynamicObject, objectEntity, false); 
                        }
                        if (!e.Cancel)
                        {
                            displayText("Información Guardada");
                        }
                        else
                        {
                            dataGridScrollViewer.Focus();     
                        }
                        if (flagNew  && !e.Cancel)
                        {
                            lastEntityID = objectEntity.ID;
                            fillDataGrid();
                        }
                        lastIndex = -1;
                        lastValueEdited = null;
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
        #endregion

        #region Helpers
        /// <summary>
        /// Handle when a CheckBox is clicked
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void checkBoxEvent(object sender, RoutedEventArgs e)
        {
            var selectedItem = this.currentDataGrid.SelectedItem;
            var checkBox = sender as CheckBox;
            if (selectedItem != null)
            {
                String property = checkBox.Tag.ToString();
                EF.SetProperty(selectedItem, property, checkBox.IsChecked.Value);
            }
        }
        #endregion
    }
}
public class Test
{
    [Display(Name = "Price", AutoGenerateField = true)]
    public String name { get; set; }
    [Display(Name = "Price", AutoGenerateField = true)]
    public String name2 { get; set; }
    [Display(Name = "Price", AutoGenerateField = true)]
    public String name3 { get; set; }
    [Display(Name = "Price", AutoGenerateField = true)]
    public String name4 { get; set; }
    [Display(Name = "Price", AutoGenerateField = true)]
    public String name25 { get; set; }
    [Display(Name = "Price", AutoGenerateField = true)]
    public String name33 { get; set; }
    [Display(Name = "Price", AutoGenerateField = true)]
    public String name25235235 { get; set; }
    [Display(Name = "Price", AutoGenerateField = true)]
    public String name3323 { get; set; }
    [Display(Name = "Price", AutoGenerateField = true)]
    public String A { get; set; }
    [Display(Name = "Price", AutoGenerateField = true)]
    public String B { get; set; }
    [Display(Name = "Price", AutoGenerateField = true)]
    public String C { get; set; }
    [Display(Name = "Price", AutoGenerateField = true)]
    public String D { get; set; }
    [Display(Name = "Price", AutoGenerateField = true)]
    public String E { get; set; }
    [Display(Name = "Price", AutoGenerateField = true)]
    public String F { get; set; }

}