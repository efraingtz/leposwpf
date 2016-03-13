using LeposWPF.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
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
        /// <summary>
        /// Validate if an imported file is correct
        /// </summary>
        /// <param name="pathDataBase">Path to new database</param>
        /// <returns>True / False</returns>
        internal static bool validateImportDataBase(String pathDataBase)
        {
            SQLiteConnection sqlLiteConnection = new SQLiteConnection("Data Source="+pathDataBase+ ";password=un2CeJr8VSmnxKPq"); ;
            try
            {
                String[] tables = new String[] { "Client","Company",
                                                 "Product","ProductPrice",
                                                 "Provider","Purchase",
                                                 "PurchasePayment","PurchaseProduct",
                                                 "Sale","SalePayment",
                                                 "SaleProduct","User",
                };
                sqlLiteConnection.Open();
                foreach (var table in tables)
                {
                    SQLiteCommand sqlCommand;
                    sqlCommand = sqlLiteConnection.CreateCommand();
                    sqlCommand.CommandText = "select count(*) from " +table;
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (SQLiteException sqLiteException)
            {
                Console.WriteLine(sqLiteException);
                return false;
            }
            finally
            {
                sqlLiteConnection.Close();
            }
            return true;
        }
    }
}
