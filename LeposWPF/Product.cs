//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LeposWPF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    /// <summary>
    ///  Class that maps a product to the database
    /// </summary>
    public partial class Product
    {
        #region Constructors
        /// <summary>
        /// Initialize current instace
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.ProductPrices = new HashSet<ProductPrice>();
            this.IsAlive = true;
        }
        #endregion
        #region Definition
        /// <summary>
        /// Identifier of the current object
        /// </summary>
        [Display(Name = "C�digo de Barras", AutoGenerateField = true)]
        public string ID { get; set; }
        /// <summary>
        /// Price of the product
        /// </summary>
        [Display(Name = "Precio", AutoGenerateField = true)]
        public double Price { get; set; }
        /// <summary>
        /// Wholesale price of the product
        /// </summary>
        [Display(Name = "Precio Mayoreo", AutoGenerateField = true)]
        public double WholeSalePrice { get; set; }
        /// <summary>
        /// Cost of the product
        /// </summary>
        [Display(Name = "Costo", AutoGenerateField = true)]
        public double Cost { get; set; }
        /// <summary>
        /// Actual quantity in stock of the product
        /// </summary>
        [Display(Name="Cantidad Actual", AutoGenerateField = true)]
        public double Quantity { get; set; }
        /// <summary>
        /// Minimum quantity that needs to be on stock for the product
        /// </summary>
        [Display(Name = "M�nimo en Inventario", AutoGenerateField = true)]
        public double MinimumQuanity { get; set; }
        /// <summary>
        /// Description of the product
        /// </summary>
        [Display(Name="Descripci�n", AutoGenerateField = true)]
        public string Description { get; set; }
        /// <summary>
        /// Date of birth in the system of the product
        /// </summary>
        [Display(AutoGenerateField = false)]
        public System.DateTime Birth { get; set; }
        /// <summary>
        /// Date of death in the system of the product
        /// </summary>
        [Display(AutoGenerateField = false)]
        public System.DateTime Death { get; set; }
        /// <summary>
        /// Flag that indicates if the product is still active in the system
        /// </summary>
        [Display(AutoGenerateField = false)]
        public bool IsAlive { get; set; }
        /// <summary>
        /// List of times this product has had its values updated
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Display(AutoGenerateField = false)]
        public virtual ICollection<ProductPrice> ProductPrices { get; set; }
        /// <summary>
        /// List of times this product has been bougth
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Display(AutoGenerateField = false)]
        public virtual ICollection<PurchaseProduct> PurchaseProducts { get; set; }
        /// <summary>
        /// List of times this product has been sold
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Display(AutoGenerateField = false)]
        public virtual ICollection<SaleProduct> SaleProducts { get; set; }
        #endregion
    }
}
