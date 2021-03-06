﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LeposWPF.Helpers.Clases
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    /// <summary>
    /// Helper class to fill NewSale's DataGrid
    /// </summary>
    public class PurchaseHelper
    {
        #region Definition
        /// <summary>
        /// Foreign ID of the product
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Producto", AutoGenerateField = true)]
        public string Product_ID { get; set; }
        /// <summary>
        /// Price of the product
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Descripción", AutoGenerateField = true)]
        public string Description { get; set; }
        /// <summary>
        /// Quantity purchased
        /// </summary>
        [Display(Name = "Cantidad", AutoGenerateField = true)]
        public double Quantity { get; set; }
        /// <summary>
        /// Quantity purchased
        /// </summary>
        [Display(Name = "Costo", AutoGenerateField = true)]
        public double Cost { get; set; }
        /// <summary>
        /// Quantity purchased
        /// </summary>
        [Display(Name = "Importe", AutoGenerateField = true)]
        [ReadOnly(true)]
        public double Amount { get; set; }
        /// <summary>
        /// Price of the product
        /// </summary>
        [Display(Name = "Precio", AutoGenerateField = true)]
        public double Price { get; set; }
        /// <summary>
        /// Original price of the product
        /// </summary>
        [Display(AutoGenerateField = false)]
        public double OriginalPrice { get; set; }
        /// <summary>
        /// Original cost of the product
        /// </summary>
        [Display(AutoGenerateField = false)]
        public double OriginalCost { get; set; }
        /// <summary>
        /// Quantity purchased
        /// </summary>
        [Display(Name = "Remover", AutoGenerateField = true, Description ="Template")]
        public String Remove { get; set; }
        #endregion
    }
}
