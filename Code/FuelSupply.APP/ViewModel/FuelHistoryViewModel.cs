using FuelSupply.APP.ExportEntity;
using FuelSupply.BAL.Manager;
using FuelSupply.BAL.Manager.Common;
using FuelSupply.DAL.Entity.CustomerEntity;
using FuelSupply.DAL.Entity.Fuel;
using FuelSupply.DAL.Entity.FuelEntity;
using FuelSupply.DAL.Entity.UserEntity;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace FuelSupply.APP.ViewModel
{
    public class FuelHistoryViewModel : INotifyPropertyChanged
    {
        #region "Declaration"
        private List<HistoryType> _FuelHistoryTypeList;
        private HistoryType _SelectedFuelHistoryType;
        private MainWindow oMainWindow;
        private List<Customer> _CustomerList;
        private List<Customer> _KeyCustomerList;
        private List<User> _UserList;
        private List<FuelType> _FuelTypeList;

        private List<FuelHistory> _FuelHistoryList;
        #endregion

        #region "Property"
        public string LoggedUserName
        {
            get
            {
                if (SharedData.LoggedUser != null)
                {
                    return SharedData.LoggedUser.Name;
                }
                else
                    return string.Empty;
            }
        }
        public List<FuelHistory> FuelHistoryList
        {
            get
            {
                return _FuelHistoryList;
            }
            set
            {
                _FuelHistoryList = value;
                OnPropertyChanged("FuelHistoryList");
            }
        }
        public List<HistoryType> FuelHistoryType
        {
            get
            {
                return _FuelHistoryTypeList;
            }
            set
            {
                _FuelHistoryTypeList = value;
                OnPropertyChanged("FuelHistoryType");
            }
        }

        public HistoryType SelectedFuelHistoryType
        {
            get
            {
                return _SelectedFuelHistoryType;
            }
            set
            {
                _SelectedFuelHistoryType = value;
                //OnPropertyChanged("SelectedFuelHistoryType");
            }
        }

        public string HistoryInfoHeader
        {
            get
            {
                if (_SelectedFuelHistoryType != null)
                {
                    if(_SelectedFuelHistoryType.Id == (int)SharedData.FuelHisotryByType.Key_Customer)
                    {
                        return "Key Customers: ";
                    }
                    else if (_SelectedFuelHistoryType.Id == (int)SharedData.FuelHisotryByType.Customer)
                    {
                        return "Customers: ";
                    }
                    else if(_SelectedFuelHistoryType.Id == (int)SharedData.FuelHisotryByType.User)
                    {
                        return "Users: ";
                    }
                    else
                    {
                        return "Fuel Type: ";
                    }
                }
                else
                    return "Customer :";
            }
        }

        public object HistoryValueList
        {
            get
            {
                if (_SelectedFuelHistoryType != null)
                {
                    if (_SelectedFuelHistoryType.Id == (int)SharedData.FuelHisotryByType.Key_Customer)
                    {
                        return _KeyCustomerList;
                    }
                    else if (_SelectedFuelHistoryType.Id == (int)SharedData.FuelHisotryByType.Customer)
                    {
                        return _CustomerList;
                    }
                    else if (_SelectedFuelHistoryType.Id == (int)SharedData.FuelHisotryByType.User)
                    {
                        return _UserList;
                    }
                    else
                    {
                        return _FuelTypeList;
                    }
                }
                else
                    return _CustomerList;
            }
        }

        #endregion

        public FuelHistoryViewModel(Window pOwnerWindow)
        {
            _FuelHistoryTypeList = new List<DAL.Entity.Fuel.HistoryType>();
            HistoryType oFuelHistoryType = new DAL.Entity.Fuel.HistoryType();
            oFuelHistoryType.Id = 1;
            oFuelHistoryType.Name = "Key Customer";
            _FuelHistoryTypeList.Add(oFuelHistoryType);

            oFuelHistoryType = new DAL.Entity.Fuel.HistoryType();
            oFuelHistoryType.Id = 2;
            oFuelHistoryType.Name = "Customer";
            _FuelHistoryTypeList.Add(oFuelHistoryType);

            oFuelHistoryType = new DAL.Entity.Fuel.HistoryType();
            oFuelHistoryType.Id = 3;
            oFuelHistoryType.Name = "User";
            _FuelHistoryTypeList.Add(oFuelHistoryType);

            oFuelHistoryType = new DAL.Entity.Fuel.HistoryType();
            oFuelHistoryType.Id = 4;
            oFuelHistoryType.Name = "Fuel Type";
            _FuelHistoryTypeList.Add(oFuelHistoryType);

            oMainWindow = (MainWindow)pOwnerWindow;

            _CustomerList = CustomerManager.GetAllCustomers();
            _KeyCustomerList = CustomerManager.GetAllKeyCustomer();
            _UserList = UserManager.GetAllUser();
            _FuelTypeList = FuelManager.GetAllFuelTypeList();

            if (_CustomerList == null)
            {
                _CustomerList = new List<Customer>();
            }
            if (_KeyCustomerList == null)
            {
                _KeyCustomerList = new List<Customer>();
            }

            Customer oAllCustomer = new Customer();
            oAllCustomer.Id = 0;
            oAllCustomer.Name = "---All---";
            _CustomerList.Insert(0, oAllCustomer);

            Customer oAllKeyCustomer = new Customer();
            oAllKeyCustomer.Id = 0;
            oAllKeyCustomer.Name = "---All---";
            _KeyCustomerList.Insert(0, oAllKeyCustomer);

            User oAllUser = new User();
            oAllUser.Id = 0;
            oAllUser.Name = "---All---";
            _UserList.Insert(0, oAllUser);

            FuelType oAllFuelType = new FuelType();
            oAllFuelType.Id = 0;
            oAllFuelType.Name = "---All---";
            _FuelTypeList.Insert(0, oAllFuelType);

            SelectedFuelHistoryType = FuelHistoryType.FirstOrDefault();

        }

        #region EventHandlers (1)
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        #endregion

        #region "Methods"
        public void GetFuelHistory(DateTime? pStartDate, DateTime? pEndDate, int? pSelectedId)
        {
            if(pStartDate == null)
            {
                MessageManager.ShowErrorMessage("Please select valid start date", oMainWindow);                
            }
            else if(pEndDate == null)
            {
                MessageManager.ShowErrorMessage("Please select valid end date", oMainWindow);
            }
            else if(pStartDate > pEndDate)
            {
                MessageManager.ShowErrorMessage("Start date cannot greter than end date", oMainWindow);
            }
            else if(pSelectedId == null || pSelectedId < 0)
            {
                MessageManager.ShowErrorMessage("Please select valid value", oMainWindow);
            }
            else
            {
                if (_SelectedFuelHistoryType != null)
                {
                    if (_SelectedFuelHistoryType.Id == (int)SharedData.FuelHisotryByType.Key_Customer)
                    {
                        if(pSelectedId == 0)
                        {
                            FuelHistoryList = FuelManager.GetFuelHistoryBetweenDates(pStartDate, pEndDate);
                        }
                        else
                        {
                          FuelHistoryList = FuelManager.GetFuelHistoryByKeyCustomerId((int)pSelectedId,pStartDate,pEndDate);
                        }                        
                    }
                    else if (_SelectedFuelHistoryType.Id == (int)SharedData.FuelHisotryByType.Customer)
                    {
                        if (pSelectedId == 0)
                        {
                            FuelHistoryList = FuelManager.GetFuelHistoryBetweenDates(pStartDate, pEndDate);
                        }
                        else
                        {
                            FuelHistoryList = FuelManager.GetFuelHistoryByCustomerId((int)pSelectedId, pStartDate, pEndDate);
                        }  
                    }
                    else if (_SelectedFuelHistoryType.Id == (int)SharedData.FuelHisotryByType.User)
                    {
                        if (pSelectedId == 0)
                        {
                            FuelHistoryList = FuelManager.GetFuelHistoryBetweenDates(pStartDate, pEndDate);
                        }
                        else
                        {
                            FuelHistoryList = FuelManager.GetFuelHistoryByUserId((int)pSelectedId, pStartDate, pEndDate);
                        }  
                    }
                    else
                    {
                        if (pSelectedId == 0)
                        {
                            FuelHistoryList = FuelManager.GetFuelHistoryBetweenDates(pStartDate, pEndDate);
                        }
                        else
                        {
                            FuelHistoryList = FuelManager.GetFuelHistoryByFuelTypeId((int)pSelectedId, pStartDate, pEndDate);
                        }  
                    }
                }
                else
                {
                    MessageManager.ShowErrorMessage("Please select valid value", oMainWindow);
                }
            }

        }

        public List<FuelHistoryExport> ConvertFuelHistoryToFuelHistoryExportEntity()
        {
            List<FuelHistoryExport> oFuelHistoryExportList = new List<FuelHistoryExport>();

            if (FuelHistoryList != null && FuelHistoryList.Count > 0)
            {
                FuelHistoryExport oExportEntity;
                foreach (FuelHistory oHistory in FuelHistoryList)
                {
                    oExportEntity = new FuelHistoryExport();
                    oExportEntity.ID = oHistory.Id;
                    if (oHistory.Customer != null)
                        oExportEntity.CustomerName = oHistory.Customer.Name;
                    else
                        oExportEntity.CustomerName = oHistory.CustomerName;

                    oExportEntity.Date = DateTime.ParseExact(oHistory.Time.Value.ToString("dd/MM/yyyy hh:mm:ss tt"), "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy hh:mm:ss tt"); 
                    oExportEntity.FuelAmount = oHistory.FuelAmount;

                    if (oHistory.FuelType1 != null)
                        oExportEntity.FuelType = oHistory.FuelType1.Name;
                    else
                        oExportEntity.FuelType = "Petrol";

                    oExportEntity.FuelVolume = oHistory.FuelVolume;

                    if (oHistory.Customer != null && oHistory.Customer.Customer2 != null)
                        oExportEntity.KeyCustomerName = oHistory.Customer.Customer2.Name;
                    else
                        oExportEntity.KeyCustomerName = string.Empty;


                    if (oHistory.User != null)
                        oExportEntity.UserName = oHistory.User.Name;
                    else
                        oExportEntity.UserName = oHistory.UserName;

                    oFuelHistoryExportList.Add(oExportEntity);
                }
            }

            return oFuelHistoryExportList;
        }

        public void Export_Data_To_Word(List<FuelHistoryExport> pHistoryList, string filename)
        {
            if (pHistoryList != null && pHistoryList.Count > 0)
            {
                PropertyInfo[] headerInfo = typeof(FuelHistoryExport).GetProperties();

                // Create an array for the headers and add it to the
                // worksheet starting at cell A1.
                List<string> objHeaders = new List<string>();
                for (int index = 0; index < headerInfo.Length; index++)
                {
                    objHeaders.Add(headerInfo[index].Name);
                }

                int RowCount = pHistoryList.Count();
                int ColumnCount = headerInfo.Count();
                Object[,] DataArray = new object[RowCount, ColumnCount];              

                for (int j = 0; j < pHistoryList.Count; j++)
                {
                    var item = pHistoryList[j];
                    for (int i = 0; i < objHeaders.Count; i++)
                    {
                        var y = typeof(FuelHistoryExport).InvokeMember(objHeaders[i].ToString(), BindingFlags.GetProperty, null, item, null);
                        DataArray[j, i] = (y == null) ? "" : y.ToString();
                    }
                }

                Word.Document oDoc = new Word.Document();
                oDoc.Application.Visible = true;

                //page orintation
                oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;


                dynamic oRange = oDoc.Content.Application.Selection.Range;
                string oTemp = "";
                for (int r = 0; r <= RowCount - 1; r++)
                {
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                        oTemp = oTemp + DataArray[r, c] + "\t";

                    }
                }

                //table format
                oRange.Text = oTemp;

                object Separator = Word.WdTableFieldSeparator.wdSeparateByTabs;
                object ApplyBorders = true;
                object AutoFit = true;
                object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitContent;

                oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                                      Type.Missing, Type.Missing, ref ApplyBorders,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);

                oRange.Select();

                oDoc.Application.Selection.Tables[1].Select();
                oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.InsertRowsAbove(1);
                oDoc.Application.Selection.Tables[1].Rows[1].Select();

                //header row style
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1;
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Tahoma";
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14;

                //add header row manually
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = objHeaders[c];
                }

                //table style 
                oDoc.Application.Selection.Tables[1].set_Style("Grid Table 4 - Accent 5");
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                //header text
                foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections)
                {
                    Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                    headerRange.Text = "Fuel History";
                    headerRange.Font.Size = 16;
                    headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                }

                //save the file
                oDoc.SaveAs2(filename);

                //NASSIM LOUCHANI
            }
        }

        public void Export_Data_To_PDF(List<FuelHistoryExport> pHistoryList, string filename)
        {
            PropertyInfo[] headerInfo = typeof(FuelHistoryExport).GetProperties();

            // Create an array for the headers and add it to the
            // worksheet starting at cell A1.
            List<string> objHeaders = new List<string>();
            for (int index = 0; index < headerInfo.Length; index++)
            {
                objHeaders.Add(headerInfo[index].Name);
            }

            PdfPTable pdfTable = new PdfPTable(objHeaders.Count);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfTable.DefaultCell.BorderWidth = 1;

            //Adding Header row
            foreach (string column in objHeaders)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column));                                                 
                Font boldFont = new Font(cell.Phrase.Font.BaseFont, 12, Font.BOLD, Color.BLACK);
                cell = new PdfPCell(new Phrase(column, boldFont));                
                cell.BackgroundColor = new iTextSharp.text.Color(0, 223, 223);
                cell.HorizontalAlignment = 1;
                cell.VerticalAlignment = 1;                
                pdfTable.AddCell(cell);
            }

            PdfPCell cellRow;
            int rowIndex = 1;
            //Adding DataRow           
            foreach (FuelHistoryExport oHistory in pHistoryList)
            {
                cellRow = new PdfPCell(new Phrase(oHistory.ID.ToString()));
                cellRow.HorizontalAlignment = 1;
                cellRow.VerticalAlignment = 1;
                if (rowIndex %2 == 0)
                    cellRow.BackgroundColor = new iTextSharp.text.Color(224, 255, 255);
                pdfTable.AddCell(cellRow);

                cellRow = new PdfPCell(new Phrase(oHistory.UserName));
                cellRow.HorizontalAlignment = 1;
                cellRow.VerticalAlignment = 1;
                if (rowIndex % 2 == 0)
                    cellRow.BackgroundColor = new iTextSharp.text.Color(224, 255, 255);
                pdfTable.AddCell(cellRow);

                cellRow = new PdfPCell(new Phrase(oHistory.CustomerName));
                cellRow.HorizontalAlignment = 1;
                cellRow.VerticalAlignment = 1;
                if (rowIndex % 2 == 0)
                    cellRow.BackgroundColor = new iTextSharp.text.Color(224, 255, 255);
                pdfTable.AddCell(cellRow);

                cellRow = new PdfPCell(new Phrase(oHistory.KeyCustomerName));
                cellRow.HorizontalAlignment = 1;
                cellRow.VerticalAlignment = 1;
                if (rowIndex % 2 == 0)
                    cellRow.BackgroundColor = new iTextSharp.text.Color(224, 255, 255);
                pdfTable.AddCell(cellRow);

                cellRow = new PdfPCell(new Phrase(oHistory.FuelType));
                cellRow.HorizontalAlignment = 1;
                cellRow.VerticalAlignment = 1;
                if (rowIndex % 2 == 0)
                    cellRow.BackgroundColor = new iTextSharp.text.Color(224, 255, 255);
                pdfTable.AddCell(cellRow);

                cellRow = new PdfPCell(new Phrase(oHistory.FuelVolume.ToString()));
                cellRow.HorizontalAlignment = 1;
                cellRow.VerticalAlignment = 1;
                if (rowIndex % 2 == 0)
                    cellRow.BackgroundColor = new iTextSharp.text.Color(224, 255, 255);
                pdfTable.AddCell(cellRow);

                cellRow = new PdfPCell(new Phrase(oHistory.FuelAmount.ToString()));
                cellRow.HorizontalAlignment = 1;
                cellRow.VerticalAlignment = 1;
                if (rowIndex % 2 == 0)
                    cellRow.BackgroundColor = new iTextSharp.text.Color(224, 255, 255);
                pdfTable.AddCell(cellRow);

                cellRow = new PdfPCell(new Phrase(oHistory.Date));
                cellRow.HorizontalAlignment = 1;
                cellRow.VerticalAlignment = 1;
                if (rowIndex % 2 == 0)
                    cellRow.BackgroundColor = new iTextSharp.text.Color(224, 255, 255);
                pdfTable.AddCell(cellRow);

                rowIndex = rowIndex + 1;
            }
            
            //Exporting to PDF

            using (FileStream stream = new FileStream(filename, FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A3, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
                stream.Close();
            }
        }
        #endregion
    }
}
