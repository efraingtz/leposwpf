using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace LeposWPF.Helpers
{
    /// <summary>
    /// This class contains pre-define paths used by the application.
    /// </summary>
    class Paths
    {
        #region Get Paths

        /// <summary>
        /// Payroll BIA path.
        /// </summary>
        /// <returns>String containing BIA path.</returns>
        public string getBIA()
        {
            return "\\\\STLABCFIL200\\Payroll_BIA$";
        }

        /// <summary>
        /// Payroll path.
        /// </summary>
        /// <returns>String containing Payroll path.</returns>
        public string getPayroll()
        {
            return "\\\\stlabcfil002\\BSC\\STL-WOD\\Payroll";
        }

        /// <summary>
        /// 7-Zip path used to compress data.
        /// </summary>
        /// <returns>String containing 7-Zip executable path.</returns>
        public string getSevenZipExe()
        {
            return this.getBIA() + "\\7-Zip\\7z.exe";
        }
        #endregion Get Paths
    }
}
