using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LeposWPF.Helpers
{
    /// <summary>
    /// Helper class used to create a excel file
    /// </summary>
    public class ExcelHelper
    {
        #region Declaration
        /// <summary>
        /// 
        /// </summary>
        private DataGrid dataGrid;
        /// <summary>
        /// 
        /// </summary>
        private DateTime startDate;
        /// <summary>
        /// 
        /// </summary>
        private DateTime endDate;
        /// <summary>
        /// 
        /// </summary>
        private String path;
        /// <summary>
        /// 
        /// </summary>
        private Microsoft.Office.Interop.Excel.Application excelApplication = null;
        /// <summary>
        /// 
        /// </summary>
        private Microsoft.Office.Interop.Excel.Workbook excelWorkBook = null;
        /// <summary>
        /// 
        /// </summary>
        private Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet = null;
        #endregion
        #region Constructors
        public ExcelHelper(String path , DataGrid dataGrid, DateTime startDate, DateTime endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.dataGrid = dataGrid;
            this.path = path;
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Create the excel file
        /// </summary>
        /// <returns>Message to display to user</returns>
        internal KeyValuePair<String,Boolean> createExcelFile()
        {
            excelApplication = new Microsoft.Office.Interop.Excel.Application();
            if (excelApplication == null)
            {
                return new KeyValuePair<string, bool>("Error : no se pudo iniciar la aplicación Excel en este equipo",false);
            }
            excelWorkBook = excelApplication.Workbooks.Add();
            excelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkBook.ActiveSheet;
            createHeader();
            insertData();
            String startRange = "A1";
            String endRange = ("H" + dataGrid.Items.Count + 1 + 2);
            Microsoft.Office.Interop.Excel.Range excelCells = (Microsoft.Office.Interop.Excel.Range)excelWorkSheet.get_Range(startRange, endRange);
            excelCells.Columns.AutoFit();
            return finishExcel();
        }
        /// <summary>
        /// Insert headers to excel grid
        /// </summary>
        private void createHeader()
        {
            excelWorkSheet.Cells[1, 1] = "Nota de Venta";
            excelWorkSheet.Cells[1, 2] = "Inicio : " + startDate.ToShortDateString();
            excelWorkSheet.Cells[1, 3] = "Fin: " + endDate.ToShortDateString();
            int column = 1;
            foreach (object header in dataGrid.Columns.Select(cs => cs.Header).ToList())
            {
                excelWorkSheet.Cells[3, column] = header.ToString();
                column++;
            }
        }
        /// <summary>
        /// Insert all information from DataGrid to excel file
        /// </summary>
        private void insertData()
        {
            int row = 4;
            foreach (dynamic item in dataGrid.Items)
            {
                int col = 0;
                excelWorkSheet.Cells[++col][row] = item.Producto;
                excelWorkSheet.Cells[++col][row] = item.Descripción;
                excelWorkSheet.Cells[++col][row] = item.Mayoreo ? "Mayoreo" : "Menudeo";
                excelWorkSheet.Cells[++col][row] = item.Cantidad;
                excelWorkSheet.Cells[++col][row] = item.Precio;
                excelWorkSheet.Cells[++col][row] = item.Descuento;
                excelWorkSheet.Cells[++col][row] = item.Importe;
                excelWorkSheet.Cells[++col][row] = item.IVA;
                row++;
            }
        }
        /// <summary>
        /// Close excel application and dispose all its data
        /// </summary>
        private KeyValuePair<String, bool> finishExcel()
        {
            try
            {
                if (excelApplication.ActiveWorkbook != null)
                    excelApplication.ActiveWorkbook.SaveAs(path,
                                                           Type.Missing,
                                                           Type.Missing,
                                                           Type.Missing,
                                                           Type.Missing,
                                                           Type.Missing,
                                                           Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                                                           Type.Missing,
                                                           Type.Missing,
                                                           Type.Missing,
                                                           Type.Missing,
                                                           Type.Missing);
                if (excelApplication != null)
                {
                    if (excelWorkBook != null)
                    {
                        if (excelWorkSheet != null)
                            Marshal.ReleaseComObject(this.excelWorkSheet);
                        excelWorkBook.Close(false, Type.Missing, Type.Missing);
                        Marshal.ReleaseComObject(this.excelWorkBook);
                    }
                    excelApplication.Quit();
                    Marshal.ReleaseComObject(this.excelApplication);
                    return new KeyValuePair<String, bool>("El archivo ha sido exportado", true);
                }
                else
                {
                    return new KeyValuePair<String, bool>("Error : no se ha podido cerrar la aplicación Excel", false);
                }
            }
            catch (Exception exc)
            {
                return new KeyValuePair<String, bool>("Error : no se ha podido cerrar la aplicación Excel; "+exc.ToString(), false);
            }
        }
        #endregion
    }
}