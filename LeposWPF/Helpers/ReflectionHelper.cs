using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace LeposWPF.Helpers
{
    /// <summary>
    /// Manipulate dynamically and statically properties and values from EntitFramewor's objects
    /// </summary>
    class EF
    {
        #region Properties and values
        /// <summary>
        /// Get object property value using the name of it.
        /// </summary>
        /// <param name="source">Object to get.</param>
        /// <param name="property">Property of object to get.</param>
        public static decimal GetPropertyValue(object source, string property)
        {
            PropertyInfo propertyToGet = null;
            object value = null;

            try
            {
                string[] bits = property.Split('.');

                for (int i = 0; i < bits.Length - 1; i++)
                {
                    PropertyInfo prop = source.GetType().GetProperty(bits[i]);
                    source = prop.GetValue(source, null);
                }

                if (source is IEnumerable)
                {
                    foreach (object o in (source as IEnumerable))
                    {
                        propertyToGet = o.GetType().GetProperty(bits[bits.Length - 1]);
                        value = propertyToGet.GetValue(o, null);
                        break;
                    }
                }
                else
                {
                    propertyToGet = source.GetType().GetProperty(bits[bits.Length - 1]);
                    value = propertyToGet.GetValue(source, null);
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
                
            }
            finally
            {
                GC.Collect();
            }

            return Convert.ToDecimal(value);
        }

        /// <summary>
        /// Set property value accordingly to its type
        /// </summary>
        /// <param name="propertyToSet">Property to set</param>
        /// <param name="source">Source Object</param>
        /// <param name="value">Value to set</param>
        private static void SetByType(PropertyInfo propertyToSet, object source, object value)
        {
            try
            {
                if ((propertyToSet.PropertyType == typeof(Int32))
                    || (propertyToSet.PropertyType == typeof(Int32?)))
                {
                    propertyToSet.SetValue(source, Convert.ToInt32(value), null);
                }
                else if ((propertyToSet.PropertyType == typeof(Decimal))
                    || (propertyToSet.PropertyType == typeof(Decimal?)))
                {
                    propertyToSet.SetValue(source, Convert.ToDecimal(value), null);
                }
                else if ((propertyToSet.PropertyType == typeof(DateTime))
                    || (propertyToSet.PropertyType == typeof(DateTime?)))
                {
                    propertyToSet.SetValue(source, Convert.ToDateTime(value), null);
                }
                else if ((propertyToSet.PropertyType == typeof(TimeSpan))
                    || (propertyToSet.PropertyType == typeof(TimeSpan?)))
                {
                    TimeSpan valueTimeSpan;

                    if (TimeSpan.TryParse(Convert.ToString(value), out valueTimeSpan))
                    {
                        propertyToSet.SetValue(source, valueTimeSpan, null);
                    }
                }
                else if ((propertyToSet.PropertyType == typeof(Boolean))
                    || (propertyToSet.PropertyType == typeof(Boolean?)))
                {
                    propertyToSet.SetValue(source, Convert.ToBoolean(value), null);
                }
                else if (propertyToSet.PropertyType == typeof(String))
                {
                    propertyToSet.SetValue(source, Convert.ToString(value), null);
                }
                else
                {
                    
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
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// Set object property value using the name of it.
        /// </summary>
        /// <param name="source">Object to set.</param>
        /// <param name="property">Property of object to set.</param>
        /// <param name="value">Value to be set.</param>
        public static void SetProperty(object source, string property, object value)
        {
            try
            {
                string[] bits = property.Split('.');

                for (int i = 0; i < bits.Length - 1; i++)
                {
                    PropertyInfo prop = source.GetType().GetProperty(bits[i]);
                    source = prop.GetValue(source, null);
                }

                PropertyInfo propertyToSet = null;

                if (source is IEnumerable)
                {
                    foreach (object o in (source as IEnumerable))
                    {
                        propertyToSet = o.GetType().GetProperty(bits[bits.Length - 1]);
                        SetByType(propertyToSet, o, value);
                        //propertyToSet.SetValue(o, value, null);
                        break;
                    }
                }
                else
                {
                    propertyToSet = source.GetType().GetProperty(bits[bits.Length - 1]);
                    SetByType(propertyToSet, source, value);
                    //propertyToSet.SetValue(source, value, null);
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
            }
            finally
            {
                GC.Collect();
            }
        }

        #endregion
        #region Hurdle Functions

        /// <summary>
        /// Returns the hurdle level accordingly to provide parameters
        /// </summary>
        /// <param name="value">Value from which hurdle is required.</param>
        /// <param name="ranges">Dictionary (List) of ranges that will be used to classify the value.</param>
        /// <returns></returns>
        public static int Between(int value, Dictionary<int, Dictionary<int, int>> ranges)
        {
            int level = 0;

            try
            {
                foreach (KeyValuePair<int, Dictionary<int, int>> pair in ranges)
                {
                    Dictionary<int, int> range = pair.Value;
                    bool valueInRange = false;
                    foreach (var record in range)
                    {
                        if (value >= record.Key && value <= record.Value)
                        {
                            valueInRange = true;
                        }
                    }
                    if (valueInRange)
                    {
                        level = pair.Key;
                        break;
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
            }
            finally
            {
                GC.Collect();
            }

            return level;
        }

        #endregion

    }
}
