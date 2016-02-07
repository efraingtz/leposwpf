using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Reflection;
using System.Windows.Media.Animation;
using MahApps.Metro.Controls;
using LeposWPF.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public WODVPEntities conn { get; private set; }
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
            //this.Owner = null;
            //collectionViewSource = (CollectionViewSource)FindResource("collectionViewSource");
            //auxiliarCollectionViewSource = (CollectionViewSource)FindResource("auxiliarCollectionViewSource");
            //conn = new WODVPEntities();
            storyBoard = (Storyboard)FindResource("animate");
            //initInterface();
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
            WPFUIUtils.displayText(alertTextBlock, loadingProgressBar, storyBoard, text, good);
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
                getGridForTable();
            }
            catch (Exception exc)
            {
                System.Text.StringBuilder message = new System.Text.StringBuilder();
                message.Append(exc.Message);
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
        public WODVPEntities getConn()
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
                displayText("Getting Data...");
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
        /// Fill DataGrid according to the selected item
        /// </summary>
        /// <returns>New DataGrid created</returns>
        private DataGrid getGridForTable()
        {
            DataGrid dataGridDynamic = this.dataGrid;
            try
            {
                dataGridDynamic.Height = dataGridContainerViewBox.ActualHeight;
                dataGridDynamic.Width = ActualWidth;
                dataGridDynamic.HeadersVisibility = DataGridHeadersVisibility.Column;
                dataGridDynamic.SelectionMode = DataGridSelectionMode.Single;
                dataGridDynamic.Background = Brushes.White;
                dataGridDynamic.AlternationCount = 2;
                dataGridDynamic.AutoGeneratingColumn += DataGridHelper.dataGrid_AutoGeneratingColumn;
                dataGridDynamic.RowEditEnding += dataGridDynamic_RowEditEnding;
                dataGridDynamic.CellEditEnding += dataGridDynamic_CellEditEnding;
                setContentDG(ref dataGridDynamic);
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

            return dataGridDynamic;
        }

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
        /// <param name="dataGridDynamic">Data grid to be filled</param>
        private void setContentDG(ref DataGrid dataGridDynamic)
        {
            try
            {
                dataGridDynamic.Tag = dataGridTag;
                var source = new List<Test>();
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
         
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
                dataGridDynamic.ItemsSource = source;
                dynamic obj = null;
                dynamic sourceDataGrid = null;
                switch (dataGridTag)
                {
                    //case "SourceFileLayouts":
                    //    var sourceFileLayouts = conn.SourceFileLayouts.ToList();
                    //    dataGridDynamic.ItemsSource = sourceDataGrid = sourceFileLayouts;
                    //    //Fill ComboBoxes
                    //    collectionViewSource.Source = conn.SourceFiles.Select(a => new { ID = a.ID, Name = a.Name }).OrderBy(a => a.Name).ToList();
                    //    auxiliarCollectionViewSource.Source = conn.BranchSourceFiles.Select(a => new { ID = a.ID, Name = a.SourceFile.Name }).OrderBy(a => a.Name).ToList();
                    //    obj = new SourceFileLayout();
                    //    break;
                    //case "FormulaTypes":
                    //    dataGridDynamic.ItemsSource = conn.FormulaTypes.OrderBy(a => a.Name).ToList();
                    //    obj = new FormulaType();
                    //    break;
                }
                //if (dataGridDynamic.Items.Count <= 1)
                //{
                //    sourceDataGrid = dataGridDynamic.ItemsSource;
                //    sourceDataGrid.Add(obj);
                //}
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
                                    DataGridHelper.setValueToProperty(selectedItem, property, DataGridHelper.getFirstorDefaultEntity(ID, dbSet));
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
                        //if (dynamicObject is SourceFileLayout)
                        //{
                        //    var sourceColumn = DataGridHelper.getTextDG(dataGrid, row, 1);
                        //    var sourceColumnStart = DataGridHelper.getIntegerOrNull(dataGrid, row, 2, lastValueEdited, lastIndex);
                        //    var sourceColumnSize = DataGridHelper.getIntegerOrNull(dataGrid, row, 3, lastValueEdited, lastIndex);
                        //    var destinationFieldName = DataGridHelper.getTextDG(dataGrid, row, 4);
                        //    values = new object[] { sourceColumn, sourceColumnStart, sourceColumnSize, destinationFieldName };
                        //    properties = new String[] { "SourceColumn", "SourceColumnStart", "SourceColumnSize", "DestinationFieldName" };
                        //    propertiesForeign = new String[] { "BranchSourceFile_ID", "SourceFile_ID" };
                        //    DataGridHelper.HandleAddUpdate(values, properties, propertiesForeign, e, flagNew, dynamicObject, objectEntity, false, 0, 5);
                        //}
                        //else
                        //{
                        //    String name = DataGridHelper.getTextDG(dataGrid, row, 0);
                        //    values = new object[] { name };
                        //    properties = new String[] { "Name" };
                        //    propertiesForeign = new String[] { };
                        //    DataGridHelper.HandleAddUpdate(values, properties, propertiesForeign, e, flagNew, dynamicObject, objectEntity, false);
                        //}
                        if (flagNew)
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