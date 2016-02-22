using LeposWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeposWPF.Helpers.Clases
{
    /// <summary>
    /// Helper class used for logic business methods related to companies
    /// </summary>
    public class CompanyHelper
    {
        #region Declaration
        /// <summary>
        /// Connection to the entity framework model
        /// </summary>
        private static LeposWPFModel model = new LeposWPFModel();
        #endregion
        /// <summary>
        /// Company stored in the system
        /// </summary>
        public static Company currentCompany
        {
            get;
            set;
        }
        /// <summary>
        /// Validate there's a company in the system
        /// </summary>
        /// <returns>Whether or not the user and password validates</returns>
        internal static Boolean companyExists()
        {
            var searchCompany = model.Companies.FirstOrDefault();
            currentCompany = searchCompany;
            return currentCompany != null;
        }

        /// <summary>
        /// Update data for current company
        /// </summary>
        internal static void updateCompany()
        {
            model.SaveChanges();
        }
        /// <summary>
        /// Return path to local logo
        /// </summary>
        /// <returns>Local logo's path</returns>
        internal static String getLogoPath()
        {
            return System.IO.Directory.GetCurrentDirectory() + "/" + currentCompany.Logo;
        }
        /// <summary>
        /// Get string type of IVA
        /// </summary>
        /// <param name="type">Type of IVA</param>
        /// <returns>String type of IVA</returns>
        internal static String getIVAString(int type)
        {
            switch (type)
            {
                case 0:
                    return "No tomar IVA para cálculos";
                case 1:
                    return "Los productos ya incluyen IVA";
                default:
                    return "Agregar IVA a cada cálculo";
            }
        }
    }
}
