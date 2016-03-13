using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeposWPF.Helpers
{
    /// <summary>
    /// Helper for import excel window
    /// </summary>
    public class ImportExcelHelper
    {
        #region Definition
        /// <summary>
        /// Row number
        /// </summary>
        [Display(AutoGenerateField = false)]
        public int Row { get; set; }
        /// <summary>
        /// Column number
        /// </summary>
        [Display(AutoGenerateField = false)]
        public int Column { get; set; }

        /// <summary>
        /// Identifier of the current object
        /// </summary>
        [Display(Name = "Renglón Excel", AutoGenerateField = true)]
        public string Path
        {
            get
            {
                return Row.ToString();
            }
        }
        /// <summary>
        /// Identifier of the current object
        /// </summary>
        [Display(Name = "Código de Barras", AutoGenerateField = true)]
        public string ID { get; set; }
        /// <summary>
        /// Price of the product
        /// </summary>
        [Display(Name = "Precio", AutoGenerateField = true)]
        public string Price { get; set; }
        /// <summary>
        /// Wholesale price of the product
        /// </summary>
        [Display(Name = "Precio Mayoreo", AutoGenerateField = true)]
        public string WholeSalePrice { get; set; }
        /// <summary>
        /// Cost of the product
        /// </summary>
        [Display(Name = "Costo", AutoGenerateField = true)]
        public string Cost { get; set; }
        /// <summary>
        /// Actual quantity in stock of the product
        /// </summary>
        [Display(Name = "Cantidad Actual", AutoGenerateField = true)]
        public string Quantity { get; set; }
        /// <summary>
        /// Minimum quantity that needs to be on stock for the product
        /// </summary>
        [Display(Name = "Mínimo en Inventario", AutoGenerateField = true)]
        public string MinimumQuanity { get; set; }
        /// <summary>
        /// Description of the product
        /// </summary>
        [Display(Name = "Descripción", AutoGenerateField = true)]
        public string Description { get; set; }

        /// <summary>
        /// Validate price
        /// </summary>
        /// <returns>True / false</returns>
        public Boolean priceFlag()
        {
            double parse = 0;
            return Double.TryParse(Price,out parse);
        }
        /// <summary>
        /// Validate wholesale price
        /// </summary>
        /// <returns>True / false</returns>
        public Boolean wholeSalePriceFlag()
        {
            double parse = 0;
            return Double.TryParse(WholeSalePrice, out parse);
        }
        /// <summary>
        /// Validate cost
        /// </summary>
        /// <returns>True / false</returns>
        public Boolean costFlag()
        {
            double parse = 0;
            return Double.TryParse(Cost, out parse);
        }
        /// <summary>
        /// Validate quantity
        /// </summary>
        /// <returns>True / false</returns>
        public Boolean quantityFlag()
        {
            double parse = 0;
            return Double.TryParse(Quantity, out parse);
        }
        /// <summary>
        /// Validate minimum quantity
        /// </summary>
        /// <returns>True / false</returns>
        public Boolean minimumQuantityFlag()
        {
            double parse = 0;
            return Double.TryParse(MinimumQuanity, out parse);
        }
        /// <summary>
        /// Validate description
        /// </summary>
        /// <returns>True / false</returns>
        public Boolean descriptionFlag()
        {
            return Description != string.Empty;
        }
        /// <summary>
        /// Validate whole record
        /// </summary>
        /// <returns>True / false</returns>
        public Boolean validate()
        {
            return descriptionFlag() &&
                   minimumQuantityFlag() &&
                   quantityFlag() &&
                   costFlag() &&
                   wholeSalePriceFlag() &&
                   priceFlag();
        }

        #endregion
    }
}
