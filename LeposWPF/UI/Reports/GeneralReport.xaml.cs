using LeposWPF.Helpers;
using LeposWPF.Helpers.Clases;
using MahApps.Metro.Controls;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LeposWPF.UI.Reports
{
    /// <summary>
    /// Interaction logic for GeneralReport.xaml
    /// </summary>
    public partial class GeneralReport : MetroWindow
    {
        #region Declaration
        /// <summary>
        /// 
        /// </summary>
        public DateTime endDate { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime startDate { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public List<GeneralReportProduct> reportData { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public double totalSales { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public double totalPurchases { get; private set; }
        #endregion
        #region Constructors
        public GeneralReport(DateTime start, DateTime end, List<GeneralReportProduct> reportData, double totalSales, double totalPurchases)
        {
            this.totalSales = totalSales;
            this.totalPurchases = totalPurchases;
            this.reportData = reportData;
            this.startDate = start;
            this.endDate = end;
            InitializeComponent();
        }
        #endregion
        #region UI Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.BindingSource rvBindingSource = new System.Windows.Forms.BindingSource();

            var logoPath = CompanyHelper.getLogoPath();
            Uri pathAsUri = new Uri(logoPath);
            var imagePath = pathAsUri.AbsolutePath;
            rvBindingSource.DataSource = reportData;
            reportsReportViewer.LocalReport.EnableExternalImages = true;
            reportsReportViewer.LocalReport.ReportEmbeddedResource = "LeposWPF.UI.Reports.GeneralReport.rdlc";
            reportsReportViewer.LocalReport.DataSources.Add(new ReportDataSource("productsGeneralReport", rvBindingSource));
            reportsReportViewer.LocalReport.SetParameters(new ReportParameter("CompanyName", CompanyHelper.currentCompany.Name));
            reportsReportViewer.LocalReport.SetParameters(new ReportParameter("StartDate", startDate.ToShortDateString()));
            reportsReportViewer.LocalReport.SetParameters(new ReportParameter("EndDate", endDate.ToShortDateString()));
            reportsReportViewer.LocalReport.SetParameters(new ReportParameter("imagePath", imagePath));
            reportsReportViewer.LocalReport.SetParameters(new ReportParameter("totalSales", "Ventas del período : " +totalSales.ToString("C4")));
            reportsReportViewer.LocalReport.SetParameters(new ReportParameter("totalPurchases", "Compras del período : " + totalPurchases.ToString("C4")));
            reportsReportViewer.LocalReport.SetParameters(new ReportParameter("difference", "Saldo : "+(totalSales - totalPurchases).ToString("C4")));
            reportsReportViewer.PageCountMode = PageCountMode.Actual;
            reportsReportViewer.RefreshReport();
        }
        #endregion
    }
}
