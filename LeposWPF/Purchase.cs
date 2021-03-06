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
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    /// <summary>
    /// Initialize current instance
    /// </summary>
    public partial class Purchase
    {
        #region Constructors
        /// <summary>
        /// Initialize current instance
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Purchase()
        {
            this.ProductPrices = new HashSet<ProductPrice>();
            this.PurchasePayments = new HashSet<PurchasePayment>();
            this.PurchaseProducts = new HashSet<PurchaseProduct>();
        }
        #endregion
        #region Declaration
        /// <summary>
        /// Identifier of current instance
        /// </summary>
        [Display(AutoGenerateField = true, Name ="ID")]
        public long ID { get; set; }
        /// <summary>
        /// Foreign ID of provider
        /// </summary>
        [Display(Name = "Proveedor", AutoGenerateField = true, Description = "Template")]
        public long Provider_ID { get; set; }
        /// <summary>
        /// Foreign ID of user
        /// </summary>
        [Display(Name = "Cajero", AutoGenerateField = true)]
        public string User_ID { get; set; }
        /// <summary>
        /// Flag that indicates if the sell was on credit
        /// </summary>
        [Display(Name = "Compra a Cr�dito", AutoGenerateField = true)]
        public bool IsCredit { get; set; }
        /// <summary>
        /// Days to liquidate the credit sell
        /// </summary>
        [Display(Name = "D�as Cr�dito", AutoGenerateField = false)]
        public int CreditDays { get; set; }
        /// <summary>
        /// Folio provided
        /// </summary>
        [Display(Name = "Folio", AutoGenerateField = true)]
        public string Folio { get; set; }
        /// <summary>
        /// Date of the purchase
        /// </summary>
        [Display(Name = "Fecha", AutoGenerateField = true, Description ="Template")]
        public System.DateTime Date { get; set; }
        /// <summary>
        /// Date of the purchase
        /// </summary>
        [Display(Name = "Fecha de Pago", AutoGenerateField = true, Description = "Validate")]
        public DateTime PayDate
        {
            get
            {
                return DateTime.Parse(Date.ToShortDateString()).AddDays(CreditDays);
            }
        }
        /// <summary>
        /// Total value
        /// </summary>
        [Display(AutoGenerateField = true, Name ="Costo Total", Description = "Template")]
        public double Total { get; set; }
        /// <summary>
        /// Payment done to the sale
        /// </summary>
        [Display(AutoGenerateField = true, Description = "Validate",Name ="Abonos")]
        public Double Payments
        {
            get
            {
                double payments = 0;
                PurchasePayments.ToList().ForEach(a=> payments+= a.Payment);
                return payments;
            }
        }
        /// <summary>
        /// Total value
        /// </summary>
        [Display(AutoGenerateField = true, Description = "Validate", Name = "Abonar")]
        public String Pay { get; set; }
        /// <summary>
        /// Total value
        /// </summary>
        [Display(AutoGenerateField = true, Description = "Template", Name = "Ver compra")]
        public String View { get; set; }
        /// <summary>
        /// List of ProductPrice of current products
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Display(AutoGenerateField = false)]
        public virtual ICollection<ProductPrice> ProductPrices { get; set; }
        /// <summary>
        /// Map foreign instance of provider
        /// </summary>
        [Display(AutoGenerateField = false)]
        public virtual Provider Provider { get; set; }
        /// <summary>
        /// Map foreign instance of user
        /// </summary>
        [Display(AutoGenerateField = false)]
        public virtual User User { get; set; }
        /// <summary>
        /// List of PurchasePayment done to current purchase
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Display(AutoGenerateField = false)]
        public virtual ICollection<PurchasePayment> PurchasePayments { get; set; }
        /// <summary>
        /// List of products purchased
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Display(AutoGenerateField = false)]
        public virtual ICollection<PurchaseProduct> PurchaseProducts { get; set; }
        #endregion
    }
}
