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
    /// Class that maps a client to the database
    /// </summary>
    public partial class Client
    {
        #region Constructors
        /// <summary>
        /// Initialize current object
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            this.Sales = new HashSet<Sale>();
            this.Birth = DateTime.Now;
            this.IsAlive = true;
        }
        #endregion
        #region Definition
        /// <summary>
        /// Identifier for the current instance
        /// </summary>
        [Display(Name = "ID", AutoGenerateField = false)]
        public long ID { get; set; }
        /// <summary>
        /// Name of the client
        /// </summary>
        [Display(Name = "Nombre", AutoGenerateField = true)]
        public string Name { get; set; }
        /// <summary>
        /// RFC of the client
        /// </summary>
        [Display(Name = "RFC", AutoGenerateField = true)]
        public string RFC { get; set; }
        /// <summary>
        /// Address of the client
        /// </summary>
        [Display(Name = "Direcci�n", AutoGenerateField = true)]
        public string Address { get; set; }
        /// <summary>
        /// Phone of the client
        /// </summary>
        [Display(Name = "Tel�fono", AutoGenerateField = true)]
        public string Phone { get; set; }
        /// <summary>
        /// Email of the client
        /// </summary>
        [Display(Name = "Email", AutoGenerateField = true)]
        public string Email { get; set; }
        /// <summary>
        /// Date of birth of the client in the system
        /// </summary>
        [Display(AutoGenerateField = false)]
        public System.DateTime Birth { get; set; }
        /// <summary>
        /// Date of death of the client in the system 
        /// </summary>
        [Display(AutoGenerateField = false)]
        public System.DateTime Death { get; set; }
        /// <summary>
        /// Flag that indicates if the client is still active
        /// </summary>
        [Display(AutoGenerateField = false)]
        public bool IsAlive { get; set; }
        /// <summary>
        /// Flag that indicates if the client is still active
        /// </summary>
        [Display(AutoGenerateField = true,Name ="Estado de cuenta",Description ="Template")]
        public String AccountStatus { get; set; }

        /// <summary>
        /// List of sales done to the client
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Display(AutoGenerateField = false)]
        public virtual ICollection<Sale> Sales { get; set; }
        #endregion
    }
}
