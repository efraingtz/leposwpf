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
    /// Class that maps a Provider to the database
    /// </summary>
    public partial class Provider
    {
        #region Constructors
        /// <summary>
        /// Initialize current instance
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Provider()
        {
            this.Purchases = new HashSet<Purchase>();
        }
        #endregion
        #region Definition
        /// <summary>
        /// Identifier for the current instance
        /// </summary>
        [Display(AutoGenerateField = false)]
        public long ID { get; set; }
        /// <summary>
        /// Name of the provider
        /// </summary>
        [Display(Name="Nombre",AutoGenerateField = true)]
        public string Name { get; set; }
        /// <summary>
        /// RFC of the provider
        /// </summary>
        [Display(Name = "RFC", AutoGenerateField = true)]
        public string RFC { get; set; }
        /// <summary>
        /// Email of the provider
        /// </summary>
        [Display(Name = "Email", AutoGenerateField = true)]
        public string Email { get; set; }
        /// <summary>
        /// Address of the provider
        /// </summary>
        [Display(Name = "Direcci�n", AutoGenerateField = true)]
        public string Address { get; set; }
        /// <summary>
        /// Phone of the provider
        /// </summary>
        [Display(Name = "Tel�fono", AutoGenerateField = true)]
        public string Phone { get; set; }
        /// <summary>
        /// Date of birth in the system of the provider
        /// </summary>
        [Display(AutoGenerateField = false)]
        public System.DateTime Birth { get; set; }
        /// <summary>
        /// Date of death in the system of the provider
        /// </summary>
        [Display(AutoGenerateField = false)]
        public System.DateTime Death { get; set; }
        /// <summary>
        /// Flag that indicates if the provider is still active
        /// </summary>
        [Display(AutoGenerateField = false)]
        public bool IsAlive { get; set; }
        /// <summary>
        /// List of purchases made to the provider
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Display(AutoGenerateField = false)]
        public virtual ICollection<Purchase> Purchases { get; set; }
        #endregion
    }
}
