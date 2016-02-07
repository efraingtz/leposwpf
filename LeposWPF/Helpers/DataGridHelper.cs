using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Data;
using System.Windows.Data;
using System.Reflection;
using System.Globalization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace LeposWPF.Helpers
{
    /// <summary>
    /// Interface implemented for controls that manipulates DataGrid objects
    /// </summary>
    public interface DataGridInterface
    {
        /// <summary>
        /// Method used for displaying messages to the user
        /// </summary>
        /// <param name="text">Text that will be displayed to the user</param>
        /// <param name="good">Flag to set color of text displayed</param>
        void displayText(string text, bool good = true);

        /// <summary>
        /// Method that will bind the current DataGrid
        /// </summary>
        void initInterface();

        /// <summary>
        /// Fill current DataGrid content
        /// </summary>
        void fillDataGrid();

        /// <summary>
        /// Retrieve selected index from accordion
        /// </summary>
        /// <returns>Index</returns>
        String getIndex();

        /// <summary>
        /// Retrieve selected index from accordion
        /// </summary>
        /// <returns>Index</returns>
        int getLastIndex();

        /// <summary>
        /// Retrieve last content edited from DataGrid
        /// </summary>
        /// <returns>Last value edited</returns>
        Object getLastValueEdited();

        /// <summary>
        /// Retrieve current entity framework connection
        /// </summary>
        /// <returns>Current connection</returns>
        WODVPEntities getConn();

        /// <summary>
        /// Retrieve Window Resources
        /// </summary>
        /// <returns>Current Window Resources</returns>
        ResourceDictionary getResources();
    }

    /// <summary>
    /// Helper class for DataGrid objects
    /// </summary>
    public static class DataGridHelper
    {
        #region Definition
        /// <summary>
        /// Property related to current data grid been processed
        /// </summary>
        internal static DataGridInterface dataGrid;
        #endregion
        #region DataGrid Manipulation

        /// <summary>
        /// Get value from TextBlock contained on DataGrid
        /// </summary>
        /// <param name="dataGrid">DataGrid container</param>
        /// <param name="row">Row index</param>
        /// <param name="column">Column index</param>
        /// <returns>Text from TextBlock</returns>
        internal static String getTextDG(DataGrid dataGrid, int row, int column)
        {
            return ((TextBlock)dataGrid.Columns[column].GetCellContent(dataGrid.Items[row])).Text;
        }

        /// <summary>
        /// Get value from TextBlock contained on DataGrid
        /// </summary>
        /// <param name="dataGrid">DataGrid container</param>
        /// <param name="row">Row index</param>
        /// <param name="column">Column index</param>
        /// <returns>Text from TextBlock</returns>
        internal static Object getValueOrNull(DataGrid dataGrid, int row, int column, Object lastValueEdited, int lastIndexEdited)
        {
            Decimal outVal = 0;
            var content = dataGrid.Columns[column].GetCellContent(dataGrid.Items[row]);
            String textToConvert = "";
            if (column != lastIndexEdited)
            {
                TextBlock textBlock = (TextBlock)dataGrid.Columns[column].GetCellContent(dataGrid.Items[row]);
                textToConvert = textBlock.Text;
            }
            else
            {
                textToConvert = lastValueEdited.ToString();
            }
            Boolean parseFlag = Decimal.TryParse(textToConvert, out outVal);
            if (parseFlag)
                return outVal;
            else return null;
        }

        /// <summary>
        /// Get value from TextBlock contained on DataGrid
        /// </summary>
        /// <param name="dataGrid">DataGrid container</param>
        /// <param name="row">Row index</param>
        /// <param name="column">Column index</param>
        /// <returns>Text from TextBlock</returns>
        internal static Object getIntegerOrNull(DataGrid dataGrid, int row, int column, Object lastValueEdited, int lastIndexEdited)
        {
            int outVal = 0;
            var content = dataGrid.Columns[column].GetCellContent(dataGrid.Items[row]);
            String textToConvert = "";
            if (column != lastIndexEdited)
            {
                TextBlock textBlock = (TextBlock)dataGrid.Columns[column].GetCellContent(dataGrid.Items[row]);
                textToConvert = textBlock.Text;
            }
            else
            {
                textToConvert = lastValueEdited.ToString();
            }
            Boolean parseFlag = int.TryParse(textToConvert, out outVal);
            if (parseFlag)
                return outVal;
            else return null;
        }

        /// <summary>
        /// Change a TextBlock text
        /// </summary>
        /// <param name="dataGrid">DataGrid container</param>
        /// <param name="row">Row index</param>
        /// <param name="column">Column index</param>
        /// <param name="name">TextBlock's name</param>
        /// <param name="text">New text that will be displayed inside the TextBlock</param>
        internal static void setTextDG(DataGrid dataGrid, int row, int column, String name, String text)
        {
            int outVal = 0;
            int.TryParse(((TextBlock)dataGrid.Columns[column].GetCellContent(dataGrid.Items[row])).Text, out outVal);
            TextBlock textBlock = ((TextBlock)dataGrid.Columns[column].GetCellContent(dataGrid.Items[row]));
            textBlock.Text = text;

        }

        /// <summary>
        /// Get value from TextBlock contained on DataGrid
        /// </summary>
        /// <param name="dataGrid">DataGrid container</param>
        /// <param name="row">Row index</param>
        /// <param name="column">Column index</param>
        /// <returns>Decimal value from TextBlock</returns>
        internal static Decimal getDecimalDG(DataGrid dataGrid, int row, int column)
        {
            Decimal outVal = 0;
            Decimal.TryParse(((TextBlock)dataGrid.Columns[column].GetCellContent(dataGrid.Items[row])).Text, out outVal);
            return outVal;
        }
        /// <summary>
        /// Get value from TextBlock contained on DataGrid
        /// </summary>
        /// <param name="dataGrid">DataGrid container</param>
        /// <param name="row">Row index</param>
        /// <param name="column">Column index</param>
        /// <returns>Double value from TextBlock</returns>
        internal static Double getDoubleDG(DataGrid dataGrid, int row, int column)
        {
            Double outVal = 0;
            Double.TryParse(((TextBlock)dataGrid.Columns[column].GetCellContent(dataGrid.Items[row])).Text, out outVal);
            return outVal;
        }


        /// <summary>
        /// Get value from TextBlock contained on DataGrid
        /// </summary>
        /// <param name="template">TextBlock's identifier</param>
        /// <param name="dataGrid">DataGrid container</param>
        /// <param name="row">Row index</param>
        /// <param name="column">Column index</param>
        /// <returns>Integer value from TextBlock</returns>
        internal static int getIntegerDG(String template, DataGrid dataGrid, int row, int column)
        {
            ContentPresenter contentPresenter = ((ContentPresenter)dataGrid.Columns[column].GetCellContent(dataGrid.Items[row]));
            DataTemplate dataTemplate = contentPresenter.ContentTemplate;
            TextBlock textBlock = (dataTemplate.FindName(template, contentPresenter) as TextBlock);
            int outVal = 0;
            int.TryParse(textBlock.Text, out outVal);
            return outVal;
        }

        /// <summary>
        /// Get value from TextBlock contained on DataGrid
        /// </summary>
        /// <param name="dataGrid">DataGrid container</param>
        /// <param name="row">Row index</param>
        /// <param name="column">Column index</param>
        /// <returns>Integer value from TextBlock</returns>
        internal static int getIntegerDG(DataGrid dataGrid, int row, int column)
        {
            int outVal = 0;
            Boolean convertedValueFlag = int.TryParse(((TextBlock)dataGrid.Columns[column].GetCellContent(dataGrid.Items[row])).Text, out outVal);
            if (convertedValueFlag)
                return outVal;
            else
            {
                var textBlock = dataGrid.Columns[column].GetCellContent(dataGrid.Items[row]) as TextBlock;
                var text = textBlock.Text;
                decimal parse = 0;
                Decimal.TryParse(text, out parse);
                return Decimal.ToInt32(parse);
            }
        }

        /// <summary>
        /// Get value from CheckBox contained on DataGrid
        /// </summary>
        /// <param name="dataGrid">DataGrid container</param>
        /// <param name="row">Row index</param>
        /// <param name="column">Column index</param>
        /// <returns>Boolean value from TextBlock</returns>
        internal static Boolean getBoolDG(DataGrid dataGrid, int row, int column)
        {
            return ((CheckBox)dataGrid.Columns[column].GetCellContent(dataGrid.Items[row])).IsChecked.Value;
        }
        /// <summary>
        /// Get Value From Inner DataGrid TextBox
        /// </summary>
        /// <param name="datagrid">Data Grid for input.</param>
        /// <param name="row">Row number for input.</param>
        /// <param name="column">Column number for input.</param>
        /// <returns>Decimal of data grid cell contents.</returns>
        internal static TimeSpan getTimeSpanDG(DataGrid datagrid, int row, int column)
        {
            TimeSpan outVal;
            TimeSpan.TryParse(((TextBlock)datagrid.Columns[column].GetCellContent(datagrid.Items[row])).Text, out outVal);
            return outVal;
        }
        /// <summary>
        /// Get value from DatePicker contained on DataGrid
        /// </summary>
        /// <param name="dataGrid">DataGrid container</param>
        /// <param name="row">Row index</param>
        /// <param name="column">Column index</param>
        /// <param name="name">DatePicker's identifier</param>
        /// <returns>DateTime value from TextBlock</returns>
        internal static DateTime getDateTimeDG(DataGrid dataGrid, int row, int column, String name)
        {
            ContentPresenter contentPresenter = ((ContentPresenter)dataGrid.Columns[column].GetCellContent(dataGrid.Items[row]));
            DataTemplate dataTemplate = contentPresenter.ContentTemplate;
            TextBlock datePicker = (dataTemplate.FindName(name, contentPresenter) as TextBlock);
            return DateTime.Parse(datePicker.Text);
        }

        #endregion
        #region EntityFramework Data

        /// <summary>
        /// Obtain Dataset from entities dynamically
        /// </summary>
        /// <returns>Dataset or null if not found</returns>
        internal static Object getDataSetEntities()
        {
            dynamic dynObjDataSet = null;
            try
            {
                Type connType = dataGrid.getConn().GetType();
                PropertyInfo[] connProps = connType.GetProperties();

                foreach (PropertyInfo connPropInfo in connProps)
                {
                    if (connPropInfo.Name.Equals(dataGrid.getIndex()))
                    {
                        dynObjDataSet = connPropInfo.GetValue(dataGrid.getConn(), new object[] { });
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
                dataGrid.displayText(message.ToString(), false);
            }
            finally
            {
                GC.Collect();
            }

            return dynObjDataSet;
        }
        /// <summary>
        /// Obtain Dataset from entities dynamically
        /// </summary>
        /// <param name="dbSet">Name of dbSet to search</param>
        /// <returns>Dataset or null if not found</returns>
        internal static Object getDataSetEntities(String dbSet)
        {
            dynamic dynObjDataSet = null;
            try
            {
                Type connType = dataGrid.getConn().GetType();
                PropertyInfo[] connProps = connType.GetProperties();

                foreach (PropertyInfo connPropInfo in connProps)
                {
                    if (connPropInfo.Name.Equals(dbSet))
                    {
                        dynObjDataSet = connPropInfo.GetValue(dataGrid.getConn(), new object[] { });
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
                dataGrid.displayText(message.ToString(), false);
            }
            finally
            {
                GC.Collect();
            }

            return dynObjDataSet;
        }
        /// <summary>
        /// Handle actions for new or edited Entity Object (add or update)
        /// </summary>
        /// <param name="values">Values to update.</param>
        /// <param name="properties">Names of properties.</param>
        /// <param name="propertiesForeign">Names of foreign properties.</param>
        /// <param name="e">Event of sender object.</param>
        /// <param name="flagNew">Flag used when new item is added to database.</param>
        /// <param name="dynamicObject">Dynamic object for input.</param>
        /// <param name="objectEntity">EntityFramework's object for input.</param>
        /// <param name="invalids">Array of invalid positions.</param>
        internal static void HandleAddUpdate(object[] values, string[] properties, string[] propertiesForeign, DataGridRowEditEndingEventArgs e, bool flagNew, dynamic dynamicObject, dynamic objectEntity, Boolean flagLastValueEdit, params int[] invalids)
        {
            try
            {
                //LastRow = this.currentDataGrid.Items[e.Row.GetIndex()];
                if (!flagLastValueEdit)
                    setLastValue(values, invalids);
                setForeignPropertiesValues(flagNew, ref objectEntity, dynamicObject, propertiesForeign);
                setValuesToProperties(ref objectEntity, properties, values);
                validateNewRowForeignProperty(flagNew, objectEntity, e, propertiesForeign);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException exc)
            {
                Console.WriteLine(exc.ToString());
                retrieveChanges();
                System.Text.StringBuilder message = new System.Text.StringBuilder();
                message.Append("Error: You are setting a duplicated value");
                dataGrid.displayText(message.ToString(), false);
                e.Cancel = true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbevException)
            {
                retrieveChanges();
                System.Text.StringBuilder message = new System.Text.StringBuilder();
                message.Append(dbevException.Message);
                var errorMessages = dbevException.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat("There were errors in the input values: ", fullErrorMessage);
                dataGrid.displayText(exceptionMessage.ToString(), false);
                e.Cancel = true;
            }
            catch (Exception exc)
            {
                retrieveChanges();
                System.Text.StringBuilder message = new System.Text.StringBuilder();
                message.Append(exc.Message);

                if (exc.InnerException != null)
                {
                    message.Append("\n" + exc.InnerException.Message);
                }

                dataGrid.displayText(message.ToString(), false);
                e.Cancel = true;
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// Assign the last value entered in the current DataGrid
        /// </summary>
        /// <param name="values">Values for input.</param>
        /// <param name="invalids">Array of invalid values.</param>
        internal static void setLastValue(object[] values, params int[] invalids)
        {
            if (dataGrid.getLastValueEdited() != null)
            {
                int invalidPositions = 0;

                for (int counter = 0; counter < invalids.Length; counter++)
                {
                    if (invalids[counter] < dataGrid.getLastIndex())
                        invalidPositions++;
                }

                int position = dataGrid.getLastIndex() - invalidPositions;
                String value = dataGrid.getLastValueEdited().ToString();

                if (values[position] is String)
                {
                    values[position] = value;
                    return;
                }

                int intValue = 0;
                decimal decVal = 0;
                Boolean convert = int.TryParse(value + "", NumberStyles.Number, CultureInfo.InvariantCulture, out intValue);

                if (convert)
                {
                    values[position] = intValue;
                    return;
                }

                convert = Decimal.TryParse(value + "", NumberStyles.Number, CultureInfo.InvariantCulture, out decVal);

                if (convert)
                {
                    values[position] = decVal;
                    return;
                }

                values[position] = dataGrid.getLastValueEdited();
            }
        }

        /// <summary>
        /// Set foreign values to object
        /// </summary>
        /// <param name="flagNew">Flag that indicates if a new object is added</param>
        /// <param name="entity">EntityFramework's object.</param>
        /// <param name="dynamicObject">Object to assign values</param>
        /// <param name="propertiesForeign">Name of foreign relationship's properties</param>
        internal static void setForeignPropertiesValues(Boolean flagNew, ref dynamic entity, dynamic dynamicObject, String[] propertiesForeign)
        {
            if (!flagNew)
            {
                entity = dynamicObject;
            }
            else
            {
                for (int counter = 0; counter < propertiesForeign.Length; counter++)
                {
                    PropertyInfo propertyInfo = entity.GetType().GetProperty(propertiesForeign[counter]);
                    object value = dynamicObject.GetType().GetProperty(propertiesForeign[counter]).GetValue(dynamicObject, null);
                    Type type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                    object safeValue = (value == null) ? null : Convert.ChangeType(value, type);
                    propertyInfo.SetValue(entity, safeValue, null);
                }
            }
        }
        /// <summary>
        /// Set value to a property with reflection
        /// </summary>
        /// <param name="entity">Entity to set property</param>
        /// <param name="property">Property of the entity to set new value</param>
        /// <param name="value">Value to set to property</param>
        internal static void setValueToProperty(object entity, String property, object value)
        {
            PropertyInfo propertyInfo = entity.GetType().GetProperty(property);
            propertyInfo.SetValue(entity, value, null);
        }

        /// <summary>
        /// Retrieve specific object from a collection
        /// </summary>
        /// <param name="id">Value to compare and retrieve object</param>
        /// <param name="dbSet">Name of DbSet to search for value</param>
        /// <returns></returns>
        internal static dynamic getFirstorDefaultEntity(object id, String dbSet)
        {
            if (id is int)
                return getCollection(dbSet).FirstOrDefault(x => x.ID == (int)id);
            else
                return getCollection(dbSet).FirstOrDefault(x => x.ID == (String)id);
        }
        /// <summary>
        /// Get collection from entities connection according to a string
        /// </summary>
        /// <param name="dbSet">Name of DbSet</param>
        /// <returns></returns>
        private static IEnumerable<dynamic> getCollection(String dbSet)
        {
            return (IEnumerable<dynamic>)DataGridHelper.getDataSetEntities(dbSet);
        }
        /// <summary>
        /// Assign values to properties of an specific object
        /// </summary>
        /// <param name="obj">Object to assign values.</param>
        /// <param name="properties">Name of object's properties.</param>
        /// <param name="values">Values for object's properties.</param>
        internal static void setValuesToProperties(ref Object obj, String[] properties, Object[] values)
        {
            for (int counter = 0; counter < properties.Length; counter++)
            {
                PropertyInfo propertyInfo = obj.GetType().GetProperty(properties[counter]);
                object value = values[counter];
                Type type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                object safeValue = (value == null) ? null : Convert.ChangeType(value, type);
                if (safeValue != null)
                {
                    if (safeValue.GetType() == typeof(string))
                    {
                        String str = safeValue as String;

                        if (str.Equals(String.Empty))
                        {
                            safeValue = null;
                        }
                    }
                }

                propertyInfo.SetValue(obj, safeValue, null);
            }
        }

        /// <summary>
        /// Validate foreign properties in the objects
        /// </summary>
        /// <param name="flagNew">Flag that indicates if a new object is added</param>
        /// <param name="objectEntity">EntityFramework's object</param>
        /// <param name="e">Event of sender object.</param>
        /// <param name="propertiesForeign">Names of all the foreign properties</param>
        internal static void validateNewRowForeignProperty(Boolean flagNew, dynamic objectEntity, DataGridRowEditEndingEventArgs e, String[] propertiesForeign)
        {
            for (int counter = 0; counter < propertiesForeign.Length; counter++)
            {
                object val = objectEntity.GetType().GetProperty(propertiesForeign[counter]).GetValue(objectEntity, null);

                if (val is Int32)
                {
                    int intVal = Convert.ToInt32(val);

                    if (intVal == 0)
                    {
                        var property = objectEntity.GetType().GetProperty(propertiesForeign[counter]);
                        var custom = property.GetCustomAttributes(false);
                        var dataAnnotation = custom[0];
                        String dataName = dataAnnotation.Name;
                        System.Text.StringBuilder message = new System.Text.StringBuilder();
                        message.Append("Error: Your are Missing the Value for : " + dataName);
                        dataGrid.displayText(message.ToString(), false);
                        e.Cancel = true;
                        return;
                    }
                }
            }

            if (flagNew)
            {
                dynamic value = getDataSetEntities();
                value.Add(objectEntity);
            }

            //dataGrid.getConn().SaveChanges();
        }

        /// <summary>
        /// Update the state of EntityFramework changed states
        /// </summary>
        internal static void retrieveChanges()
        {
            //var changedEntries = dataGrid.getConn().ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

            //foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
            //{
            //    entry.State = EntityState.Detached;
            //}

            //foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
            //{
            //    entry.State = EntityState.Unchanged;
            //}
        }

        /// <summary>
        /// Update the state of EntityFramework changed states
        /// </summary>
        /// <param name="conn">EntitiFramework connection</param>
        internal static void retrieveChanges(ref WODVPEntities conn)
        {
            //var changedEntries = conn.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

            //foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
            //{
            //    entry.State = EntityState.Detached;
            //}

            //foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
            //{
            //    entry.State = EntityState.Unchanged;
            //}
        }

        #endregion
        #region Handle Data Grid
        /// <summary>
        /// AutoGeneratingColumn handler for DataGrid
        /// </summary>
        /// <param name="sender">DataGrid object.</param>
        /// <param name="e">Event of sender object.</param>
        internal static void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                PropertyDescriptor pd = (PropertyDescriptor)e.PropertyDescriptor;
                DisplayAttribute da = (DisplayAttribute)pd.Attributes[typeof(DisplayAttribute)];

                if (da.AutoGenerateField)
                {
                    e.Column.Header = da.Name;

                    if (da.Description == "ComboBox")
                    {
                        // Create counter new template column.
                        DataGridTemplateColumn templateColumn = new DataGridTemplateColumn();
                        templateColumn.Header = da.Name;
                        templateColumn.CellTemplate = (DataTemplate)dataGrid.getResources()[e.PropertyName + "CellTemplate"];
                        templateColumn.CellEditingTemplate = (DataTemplate)dataGrid.getResources()[e.PropertyName + "CellEditingTemplate"];
                        templateColumn.SortMemberPath = e.PropertyName;
                        e.Column = templateColumn;
                        if (e.PropertyName.Equals("HourWageType_ID"))
                        {
                            templateColumn.SortMemberPath = "HourWageType.Code";
                        }
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
                dataGrid.displayText(message.ToString(), false);
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// PreviewKeyDown handler for DataGrid
        /// </summary>
        /// <param name="senderDataGrid">DataGrid object to process event</param>
        internal static void deleteItem(DataGrid senderDataGrid)
        {
            if (senderDataGrid != null)
            {
                dynamic dinamo = senderDataGrid.SelectedItem;
                if (dinamo != null)
                {
                    Type type = CollectionView.NewItemPlaceholder.GetType();
                    if (dinamo.GetType() != type)
                    {
                        if (new Dialogs().msgQuestionCantBeUndone("Confirm Deleting?"))
                        {
                            bool flagNew = (dinamo.ID is string) ? dinamo.ID == "" : dinamo.ID == 0;

                            if (!flagNew)
                            {
                                dynamic value = getDataSetEntities();

                                if (value != null)
                                {
                                    try
                                    {
                                        value.Remove(dinamo);
                                        //dataGrid.getConn().SaveChanges();
                                        dataGrid.fillDataGrid();
                                        dataGrid.displayText("Data Deleted");
                                    }
                                    catch
                                    {
                                        retrieveChanges();
                                        dataGrid.displayText("The Current Row Cannot Be Deleted", false);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    dataGrid.displayText("Error: There is no item selected", false);
                }
            }
            else
            {
                dataGrid.displayText("Error: There is no item selected", false);
            }
        }

        /// <summary>
        /// Change single click behavior
        /// </summary>
        /// <param name="sender">Element implementing the single click</param>
        /// <param name="e">Event fired when clicked</param>
        internal static void DataGridPreviewMouseLeftButtonDownEvent(object sender, System.Windows.RoutedEventArgs e)
        {
            var mbe = e as MouseButtonEventArgs;
            DependencyObject obj = null;

            if (mbe != null)
            {
                obj = mbe.OriginalSource as DependencyObject;
                while (obj != null && !(obj is DataGridCell))
                {
                    obj = VisualTreeHelper.GetParent(obj);
                }
            }

            DataGridCell cell = null;
            DataGrid dataGrid = null;

            if (obj != null)
            {
                cell = obj as DataGridCell;
            }

            if (cell != null && !cell.IsEditing && !cell.IsReadOnly)
            {
                if (!cell.IsFocused)
                {
                    cell.Focus();
                }

                dataGrid = FindVisualParent<DataGrid>(cell);

                if (dataGrid != null)
                {
                    if (dataGrid.SelectionUnit
                        != DataGridSelectionUnit.FullRow)
                    {
                        if (!cell.IsSelected)
                        {
                            cell.IsSelected = true;
                        }
                    }
                    else
                    {
                        var row = FindVisualParent<DataGridRow>(cell);

                        if (row != null && !row.IsSelected)
                        {
                            row.IsSelected = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Find parent from specified element
        /// </summary>
        /// <typeparam name="T">Type of data</typeparam>
        /// <param name="element">Element to search visual parent</param>
        /// <returns></returns>
        static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;

            while (parent != null)
            {
                T correctlyTyped = parent as T;

                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }

                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }

            return null;
        }

        #endregion
    }
}