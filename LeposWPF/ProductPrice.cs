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
    /// <summary>
    /// Class that maps a ProductPrice to the database
    /// </summary>
    public partial class ProductPrice
    {
        #region Definition
        /// <summary>
        /// Identifier of the current instance
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// Foreign ID of the product
        /// </summary>
        public string Product_ID { get; set; }
        /// <summary>
        /// Foreign ID of user that made the tansaction
        /// </summary>
        public string User_ID { get; set; }
        /// <summary>
        /// Foreign ID of purchase
        /// </summary>
        public Nullable<long> Purchase_ID { get; set; }
        /// <summary>
        /// Price of the product updated
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Date of the transaction
        /// </summary>
        public System.DateTime Date { get; set; }
        
        /// <summary>
        /// Mapped instance of product
        /// </summary>
        public virtual Product Product { get; set; }
        /// <summary>
        /// Mapped instace of purchase
        /// </summary>
        public virtual Purchase Purchase { get; set; }
        /// <summary>
        /// Mapped instace of user
        /// </summary>
        public virtual User User { get; set; }
        #endregion
    }
}
