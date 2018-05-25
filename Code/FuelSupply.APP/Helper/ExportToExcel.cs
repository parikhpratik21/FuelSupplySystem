using FuelSupply.DAL.Provider.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Excel = Microsoft.Office.Interop.Excel;

namespace FuelSupply.APP.Helper
{
    public class ExportToExcel<T, U>
        where T : class
        where U : List<T>
    {
        public List<T> dataToPrint;
        // Excel object references.
        private Excel.Application _excelApp = null;
        private Excel.Workbooks _books = null;
        private Excel._Workbook _book = null;
        private Excel.Sheets _sheets = null;
        private Excel._Worksheet _sheet = null;
        private Excel.Range _range = null;
        private Excel.Font _font = null;
        // Optional argument variable
        private object _optionalValue = Missing.Value;

        /// <summary>
        /// Generate report and sub functions
        /// </summary>
        public void GenerateReport()
        {
            try
            {
                if (dataToPrint != null)
                {
                    if (dataToPrint.Count != 0)
                    {
                        Mouse.SetCursor(Cursors.Wait);
                        CreateExcelRef();
                        FillSheet();
                        OpenReport();
                        Mouse.SetCursor(Cursors.Arrow);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.logExceptionMessage("ExportToExcel", "ReleaseObject", ex);                   
            }
            finally
            {
                ReleaseObject(_sheet);
                ReleaseObject(_sheets);
                ReleaseObject(_book);
                ReleaseObject(_books);
                ReleaseObject(_excelApp);
            }
        }
        /// <summary>
        /// Make MS Excel application visible
        /// </summary>
        private void OpenReport()
        {
            _excelApp.Visible = true;
        }
        /// <summary>
        /// Populate the Excel sheet
        /// </summary>
        private void FillSheet()
        {
            object[] header = CreateHeader();
            WriteData(header);
        }
        /// <summary>
        /// Write data into the Excel sheet
        /// </summary>
        /// <param name="header"></param>
        private void WriteData(object[] header)
        {
           // object[,] objData = new object[dataToPrint.Count, header.Length];

            List<string> oRowData;
            for (int j = 0; j < dataToPrint.Count; j++)
            {
                var item = dataToPrint[j];
                oRowData = new List<string>();
                for (int i = 0; i < header.Length; i++)
                {
                    var y = typeof(T).InvokeMember(header[i].ToString(), BindingFlags.GetProperty, null, item, null);
                    //objData[j, i] = (y == null) ? "" : y.ToString();
                    oRowData.Add((y == null) ? "" : y.ToString());
                }

                if (item.GetType() == typeof(FuelSupply.APP.ExportEntity.CreditHistoryExport) && ((FuelSupply.APP.ExportEntity.CreditHistoryExport)(object)item).IsAdjustmentCreditHistory == true)
                {
                    AddExcelRows("A" + (j + 2).ToString(), 1, header.Length, oRowData.ToArray(), j + 1,true);
                }
                else
                {
                    AddExcelRows("A" + (j + 2).ToString(), 1, header.Length, oRowData.ToArray(), j + 1, false);
                }
            }
            
            AutoFitColumns("A1", dataToPrint.Count + 1, header.Length);
        }
        /// <summary>
        /// Method to make columns auto fit according to data
        /// </summary>
        /// <param name="startRange"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        private void AutoFitColumns(string startRange, int rowCount, int colCount)
        {
            _range = _sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);
            _range.Columns.AutoFit();
        }
        /// <summary>
        /// Create header from the properties
        /// </summary>
        /// <returns></returns>
        private object[] CreateHeader()
        {
            PropertyInfo[] headerInfo = typeof(T).GetProperties();

            // Create an array for the headers and add it to the
            // worksheet starting at cell A1.
            List<object> objHeaders = new List<object>();

            List<object> objDisplayHeaders = new List<object>();
            for (int index = 0; index < headerInfo.Length; index++)
            {                
                object[] attrs = headerInfo[index].GetCustomAttributes(true);
                if (attrs != null && attrs.Any())
                {
                    string sColumnHeaderString = ((DescriptionAttribute)attrs[0]).Description;
                    objDisplayHeaders.Add(sColumnHeaderString);

                    objHeaders.Add(headerInfo[index].Name);
                }
            }

            var headerToAdd = objHeaders.ToArray();
            AddExcelRows("A1", 1, headerToAdd.Length, objDisplayHeaders.ToArray(), -1,false);
            SetHeaderStyle();

            return headerToAdd;
        }
        /// <summary>
        /// Set Header style as bold
        /// </summary>
        private void SetHeaderStyle()
        {
            _font = _range.Font;
            _font.Bold = true;           
            _range.Interior.Color = System.Drawing.Color.FromArgb(0, 0, 223, 223);
        }
        /// <summary>
        /// Method to add an excel rows
        /// </summary>
        /// <param name="startRange"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        /// <param name="values"></param>
        private void AddExcelRows(string startRange, int rowCount, int colCount, object values, int rowIndex, bool IsAdjustmentColumn)
        {            
            _range = _sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);

            if (rowIndex % 2 == 0)
            {
                _range.Interior.Color = System.Drawing.Color.FromArgb(0, 224, 255, 255);
            }
            else
            {
                _range.Interior.Color = System.Drawing.Color.White;
            }

            _range.set_Value(_optionalValue, values);

            if(IsAdjustmentColumn == true)
            {
                _sheet.Cells[rowIndex + 1, 5].Interior.Color = System.Drawing.Color.FromArgb(0, 255, 255, 0);               
            }
        }
        /// <summary>
        /// Create Excel applicaiton parameters instances
        /// </summary>
        private void CreateExcelRef()
        {
            _excelApp = new Excel.Application();
            _books = (Excel.Workbooks)_excelApp.Workbooks;
            _book = (Excel._Workbook)(_books.Add(_optionalValue));
            _sheets = (Excel.Sheets)_book.Worksheets;
            _sheet = (Excel._Worksheet)(_sheets.get_Item(1));
        }
        /// <summary>
        /// Release unused COM objects
        /// </summary>
        /// <param name="obj"></param>
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                LogManager.logExceptionMessage("ExportToExcel", "ReleaseObject", ex);                
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
