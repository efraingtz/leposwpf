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
    /// Class that maps a company to the database
    /// </summary>
    public partial class Company
    {
        #region Definition
        /// <summary>
        /// Identifier of the current instance
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// Name of the company
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// RFC of the company
        /// </summary>
        public string RFC { get; set; }
        /// <summary>
        /// Description of the company
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Address of the company
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// ZIP of the company
        /// </summary>
        public string ZIP { get; set; }
        /// <summary>
        /// Type of IVA that the company is implementing
        /// </summary>
        public int IVAType { get; set; }
        /// <summary>
        /// Path to the logo image
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// Name of configurated printer
        /// </summary>
        public string Printer { get; set; }
        /// <summary>
        /// Money stored in the cash register
        /// </summary>
        public double Cash { get; set; }
        /// <summary>
        /// Flag that indicates if the system is activated
        /// </summary>
        public bool IsActivated { get; set; }
        /// <summary>
        /// Date of activation of the system
        /// </summary>
        public System.DateTime DateActivated { get; set; }
        /// <summary>
        /// Version of the current software
        /// </summary>
        public string Version { get; set; }
        #endregion
    }
}