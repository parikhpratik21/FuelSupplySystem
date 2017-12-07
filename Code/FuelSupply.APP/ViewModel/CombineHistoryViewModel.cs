using FuelSupply.APP.ExportEntity;
using FuelSupply.BAL.Manager;
using FuelSupply.BAL.Manager.Common;
using FuelSupply.DAL.Entity.CreditEntity;
using FuelSupply.DAL.Entity.CustomerEntity;
using FuelSupply.DAL.Entity.Fuel;
using FuelSupply.DAL.Entity.FuelEntity;
using FuelSupply.DAL.Entity.UserEntity;
using FuelSupply.DAL.Provider.Common;
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

namespace FuelSupply.APP.ViewModel
{
    public class CombineHistoryViewModel : INotifyPropertyChanged
    {
        #region "Declaration"
        private List<HistoryType> _CreditHistoryTypeList;
        private HistoryType _SelectedCreditHistoryType;
        private MainWindow oMainWindow;
        private List<Customer> _CustomerList;
        private List<Customer> _KeyCustomerList;
        private List<User> _UserList;
        private List<Fetch_FuelHistory_Result> _FuelHistoryList;
        private List<Fetch_CreditHistory_Result> _CreditHistoryList;
        private List<CombineHistory> _CombineHistoryList;
        private const string _creditType = "Credit";
        private const string _fuelType = "Debit";
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

        public string CurrentShiftName
        {
            get
            {
                if (SharedData.CurrentShift != null)
                {
                    return SharedData.CurrentShift.ShiftName;
                }
                else
                    return string.Empty;
            }
        }

        public List<Fetch_CreditHistory_Result> CreditHistoryList
        {
            get
            {
                return _CreditHistoryList;
            }
            set
            {
                _CreditHistoryList = value;
                OnPropertyChanged("CreditHistoryList");
            }
        }

        public List<CombineHistory> CombineHistoryList
        {
            get
            {
                return _CombineHistoryList;
            }
            set
            {
                _CombineHistoryList = value;
                OnPropertyChanged("CombineHistoryList");
            }
        }

        public List<Fetch_FuelHistory_Result> FuelHistoryList
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

        public List<HistoryType> CreditHistoryType
        {
            get
            {
                return _CreditHistoryTypeList;
            }
            set
            {
                _CreditHistoryTypeList = value;
                OnPropertyChanged("CreditHistoryType");
            }
        }

        public HistoryType SelectedCreditHistoryType
        {
            get
            {
                return _SelectedCreditHistoryType;
            }
            set
            {
                _SelectedCreditHistoryType = value;
                //OnPropertyChanged("SelectedFuelHistoryType");
            }
        }

        public string HistoryInfoHeader
        {
            get
            {
                if (_SelectedCreditHistoryType != null)
                {
                    if (_SelectedCreditHistoryType.Id == (int)SharedData.CreditHisotryByType.Key_Customer)
                    {
                        return "Key Customers: ";
                    }
                    else if (_SelectedCreditHistoryType.Id == (int)SharedData.CreditHisotryByType.Customer)
                    {
                        return "Customers: ";
                    }
                    else
                    {
                        return "Users: ";
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
                if (_SelectedCreditHistoryType != null)
                {
                    if (_SelectedCreditHistoryType.Id == (int)SharedData.CreditHisotryByType.Key_Customer)
                    {
                        return _KeyCustomerList;
                    }
                    else if (_SelectedCreditHistoryType.Id == (int)SharedData.CreditHisotryByType.Customer)
                    {
                        return _CustomerList;
                    }
                    else
                    {
                        return _UserList;
                    }                   
                }
                else
                    return _CustomerList;
            }
        }

        #endregion

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

        public CombineHistoryViewModel(Window pOwnerWindow)
        {
            _CreditHistoryTypeList = new List<DAL.Entity.Fuel.HistoryType>();
            HistoryType oCreditHistoryType = new DAL.Entity.Fuel.HistoryType();
            oCreditHistoryType.Id = 1;
            oCreditHistoryType.Name = "Key Customer";
            _CreditHistoryTypeList.Add(oCreditHistoryType);

            oCreditHistoryType = new DAL.Entity.Fuel.HistoryType();
            oCreditHistoryType.Id = 2;
            oCreditHistoryType.Name = "Customer";

            _CreditHistoryTypeList.Add(oCreditHistoryType);
            oCreditHistoryType = new DAL.Entity.Fuel.HistoryType();
            oCreditHistoryType.Id = 3;
            oCreditHistoryType.Name = "User";
            _CreditHistoryTypeList.Add(oCreditHistoryType);         

            oMainWindow = (MainWindow)pOwnerWindow;

            _CustomerList = CustomerManager.GetAllCustomers();
            _KeyCustomerList = CustomerManager.GetAllKeyCustomer();
            _UserList = UserManager.GetAllUser();

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

            SelectedCreditHistoryType = CreditHistoryType.FirstOrDefault();

        }

        #region "Methods"
        public async Task<bool> GetFuelHistory(DateTime? pStartDate, DateTime? pEndDate, int? pSelectedId)
        {
            if (pStartDate == null)
            {
                MessageManager.ShowErrorMessage("Please select valid start date", oMainWindow);
            }
            else if (pEndDate == null)
            {
                MessageManager.ShowErrorMessage("Please select valid end date", oMainWindow);
            }
            else if (pStartDate > pEndDate)
            {
                MessageManager.ShowErrorMessage("Start date cannot greter than end date", oMainWindow);
            }
            else if (pSelectedId == null || pSelectedId < 0)
            {
                MessageManager.ShowErrorMessage("Please select valid value", oMainWindow);
            }
            else
            {
                if (_SelectedCreditHistoryType != null)
                {
                    await Task.Run(() =>
                    {
                        System.Threading.Thread.Sleep(10);

                        if (_SelectedCreditHistoryType.Id == (int)SharedData.FuelHisotryByType.Key_Customer)
                        {
                            if (pSelectedId == 0)
                            {
                                CreditHistoryList = CreditManager.GetCreditHistoryBetweenDates(pStartDate, pEndDate);
                                FuelHistoryList = FuelManager.GetFuelHistoryBetweenDates(pStartDate, pEndDate);
                            }
                            else
                            {
                                CreditHistoryList = CreditManager.GetCreditHistoryByKeyCustomerId((int)pSelectedId, pStartDate, pEndDate);
                                FuelHistoryList = FuelManager.GetFuelHistoryByKeyCustomerId((int)pSelectedId, pStartDate, pEndDate);
                            }
                        }
                        else if (_SelectedCreditHistoryType.Id == (int)SharedData.FuelHisotryByType.Customer)
                        {
                            if (pSelectedId == 0)
                            {
                                CreditHistoryList = CreditManager.GetCreditHistoryBetweenDates(pStartDate, pEndDate);
                                FuelHistoryList = FuelManager.GetFuelHistoryBetweenDates(pStartDate, pEndDate);
                            }
                            else
                            {
                                CreditHistoryList = CreditManager.GetCreditHistoryByCustomerId((int)pSelectedId, pStartDate, pEndDate);
                                FuelHistoryList = FuelManager.GetFuelHistoryByCustomerId((int)pSelectedId, pStartDate, pEndDate);
                            }
                        }
                        else
                        {
                            if (pSelectedId == 0)
                            {
                                CreditHistoryList = CreditManager.GetCreditHistoryBetweenDates(pStartDate, pEndDate);
                                FuelHistoryList = FuelManager.GetFuelHistoryBetweenDates(pStartDate, pEndDate);
                            }
                            else
                            {
                                CreditHistoryList = CreditManager.FetCreditHistoryByUserId((int)pSelectedId, pStartDate, pEndDate);
                                FuelHistoryList = FuelManager.GetFuelHistoryByUserId((int)pSelectedId, pStartDate, pEndDate);
                            }
                        }

                        ConvertFuelCreditHistoryToCombineHistory();
                    });                    
                }
                else
                {
                    MessageManager.ShowErrorMessage("Please select valid value", oMainWindow);
                }
            }
            return true;
        }

        public List<CombineHistoryExport> ConvertCombineHistoryToCombineHistoryExportEntity()
        {
            List<CombineHistoryExport> oCombineHistoryExportList = new List<CombineHistoryExport>();

            if (CreditHistoryList != null && CreditHistoryList.Count > 0)
            {
                CombineHistoryExport oExportEntity;
                foreach (var oHistory in CombineHistoryList)
                {
                    oExportEntity = new CombineHistoryExport();
                    oExportEntity.ID = oHistory.Id;
                    oExportEntity.CustomerName = oHistory.CustomerName;

                    oExportEntity.Date = DateTime.ParseExact(oHistory.Time.Value.ToString("dd/MM/yyyy hh:mm:ss tt"), "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy hh:mm:ss tt");
                    oExportEntity.CreditAmount = oHistory.CreditAmount;
                    oExportEntity.Shift = oHistory.ShiftName;                                        
                    oExportEntity.FuelAmount = oHistory.FuelAmount.ToString();
                    oExportEntity.InvoiceNo = oHistory.InvoiceNo;
                    oExportEntity.Type = oHistory.HistoryType;
                    oExportEntity.KeyCustomer = oHistory.KeyCustomerName;
                    oExportEntity.UserName = oHistory.AttendantName;

                    oCombineHistoryExportList.Add(oExportEntity);
                }
            }

            return oCombineHistoryExportList;
        }     

        public void Export_Data_To_PDF(List<CombineHistoryExport> pHistoryList, string filename)
        {
            PropertyInfo[] headerInfo = typeof(CombineHistoryExport).GetProperties();

            // Create an array for the headers and add it to the
            // worksheet starting at cell A1.
            List<string> objHeaders = new List<string>();
            for (int index = 0; index < headerInfo.Length; index++)
            {
                object[] attrs = headerInfo[index].GetCustomAttributes(true);
                if (attrs != null && attrs.Any())
                {
                    string sColumnHeaderString = ((DescriptionAttribute)attrs[0]).Description;
                    objHeaders.Add(sColumnHeaderString);
                }
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
            foreach (var oHistory in pHistoryList)
            {
                cellRow = new PdfPCell(new Phrase(oHistory.ID.ToString()));
                cellRow.HorizontalAlignment = 1;
                cellRow.VerticalAlignment = 1;
                if (rowIndex % 2 == 0)
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

                cellRow = new PdfPCell(new Phrase(oHistory.KeyCustomer));
                cellRow.HorizontalAlignment = 1;
                cellRow.VerticalAlignment = 1;
                if (rowIndex % 2 == 0)
                    cellRow.BackgroundColor = new iTextSharp.text.Color(224, 255, 255);
                pdfTable.AddCell(cellRow);

                cellRow = new PdfPCell(new Phrase(oHistory.InvoiceNo));
                cellRow.HorizontalAlignment = 1;
                cellRow.VerticalAlignment = 1;
                if (rowIndex % 2 == 0)
                    cellRow.BackgroundColor = new iTextSharp.text.Color(224, 255, 255);
                pdfTable.AddCell(cellRow);

                cellRow = new PdfPCell(new Phrase(oHistory.FuelAmount));
                cellRow.HorizontalAlignment = 1;
                cellRow.VerticalAlignment = 1;
                if (rowIndex % 2 == 0)
                    cellRow.BackgroundColor = new iTextSharp.text.Color(224, 255, 255);
                pdfTable.AddCell(cellRow);

                cellRow = new PdfPCell(new Phrase(oHistory.Type));
                cellRow.HorizontalAlignment = 1;
                cellRow.VerticalAlignment = 1;
                if (rowIndex % 2 == 0)
                    cellRow.BackgroundColor = new iTextSharp.text.Color(224, 255, 255);
                pdfTable.AddCell(cellRow);

                //cellRow = new PdfPCell(new Phrase(oHistory.PaymentType));
                //cellRow.HorizontalAlignment = 1;
                //cellRow.VerticalAlignment = 1;
                //if (oHistory.IsAdjustmentCreditHistory == true)
                //{
                //    cellRow.BackgroundColor = new iTextSharp.text.Color(255, 255, 0);
                //}
                //else
                //{
                //    if (rowIndex % 2 == 0)
                //        cellRow.BackgroundColor = new iTextSharp.text.Color(224, 255, 255);
                //}
                //pdfTable.AddCell(cellRow);

                cellRow = new PdfPCell(new Phrase(oHistory.CreditAmount.ToString()));
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

                cellRow = new PdfPCell(new Phrase(oHistory.Shift));
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

        public bool Export_Data_To_HTML(List<CombineHistoryExport> pHistoryList, string pFileName)
        {
            bool bResult = false;
            try
            {
                if (pHistoryList != null && pHistoryList.Count > 0)
                {
                    string sHeaderBackColor = @"""#00DFDF""";
                    string sRowBackColor = @"""#E0FFFF""";
                    string sAdjustMentRowbackColor = @"""#FFFF00""";

                    StringBuilder sbHtml = new StringBuilder();
                    //create html & table
                    sbHtml.AppendLine("<html><body><center><" +
                                  "table border='1' cellpadding='10' cellspacing='0'>");
                    sbHtml.AppendLine("<tr bgcolor=" + sHeaderBackColor + ">");

                    PropertyInfo[] headerInfo = typeof(CombineHistoryExport).GetProperties();

                    // Create an array for the headers and add it to the
                    // worksheet starting at cell A1.
                    List<string> objHeaders = new List<string>();
                    for (int index = 0; index < headerInfo.Length; index++)
                    {
                        object[] attrs = headerInfo[index].GetCustomAttributes(true);
                        if (attrs != null && attrs.Any())
                        {
                            string sColumnHeaderString = ((DescriptionAttribute)attrs[0]).Description;

                            objHeaders.Add(headerInfo[index].Name);

                            //objHeaders.Add(headerInfo[index].Name);
                            sbHtml.AppendLine("<td align='center' valign='middle'><b>" +
                                           sColumnHeaderString + "</b></td>");
                        }
                    }

                    //create table body                   

                    int RowCount = pHistoryList.Count();
                    int ColumnCount = headerInfo.Count();
                    Object[,] DataArray = new object[RowCount, ColumnCount];

                    for (int j = 0; j < pHistoryList.Count; j++)
                    {
                        if ((j + 1) % 2 == 0)
                        {
                            sbHtml.AppendLine("<tr bgcolor=" + sRowBackColor + ">");
                        }
                        else
                        {
                            sbHtml.AppendLine("<tr>");
                        }


                        var item = pHistoryList[j];
                        for (int i = 0; i < objHeaders.Count; i++)
                        {
                            var data = typeof(CombineHistoryExport).InvokeMember(objHeaders[i].ToString(), BindingFlags.GetProperty, null, item, null);

                            string strData = string.Empty;
                            if(data != null)
                            {
                                strData = data.ToString();
                            }

                            //if (objHeaders[i].ToString() == "PaymentType")
                            //{
                            //    if (item.IsAdjustmentCreditHistory == true)
                            //    {
                            //        sbHtml.AppendLine("<td align='center' bgcolor= " + sAdjustMentRowbackColor + " valign='middle'>" + strData + "</td>");
                            //    }
                            //    else
                            //    {
                            //        sbHtml.AppendLine("<td align='center' valign='middle'>" + strData + "</td>");
                            //    }
                            //}
                            //else
                            //{
                                sbHtml.AppendLine("<td align='center' valign='middle'>" + strData + "</td>");
                            //}
                        }

                        sbHtml.AppendLine("</tr>");
                    }
                    //table footer & end of html file
                    sbHtml.AppendLine("</table></center></body></html>");

                    string sFileData = sbHtml.ToString();

                    System.IO.File.WriteAllText(pFileName, sFileData);
                }

                bResult = true;
            }
            catch (Exception ex)
            {
                bResult = false;
                LogManager.logExceptionMessage("CreditHistoryViewModel", "Export_Data_To_HTML", ex);
            }

            return bResult;
        }

        private void ConvertFuelCreditHistoryToCombineHistory()
        {
            if(CombineHistoryList == null)
            {
                CombineHistoryList = new List<CombineHistory>();
            }

            CombineHistoryList.Clear();

            CombineHistory history;
            int index = 1;
            foreach(var fuelHistory in FuelHistoryList)
            {
                history = new CombineHistory();
                history.CustomerId = fuelHistory.CustomerId;
                history.FuelAmount = fuelHistory.FuelAmount;
                history.FuelVolume = fuelHistory.FuelVolume;
                history.Id = index;
                history.InvoiceNo = fuelHistory.InvoiceNo;
                history.KeyCustomerId = fuelHistory.KeyCustomerId;
                history.KeyCustomerName = fuelHistory.KeyCustomerName;                
                history.ShiftName = fuelHistory.ShiftName;
                history.Time = fuelHistory.Time;
                history.AttendantName = fuelHistory.AttendantName;
                history.HistoryType = _fuelType;
                history.CustomerName = fuelHistory.CustomerName;                
                history.CustomerLastBalance = fuelHistory.CustomerLastBalance;
                CombineHistoryList.Add(history);
            }

            foreach(var creditHistory in CreditHistoryList)
            {
                history = new CombineHistory();
                history.CustomerId = creditHistory.CustomerId;                
                history.Id = index;                
                history.KeyCustomerId = creditHistory.KeyCustomerId;
                history.KeyCustomerName = creditHistory.KeyCustomerName;
                history.ShiftName = creditHistory.ShiftName;
                history.Time = creditHistory.Time;
                history.AttendantName = creditHistory.AttendantName;
                history.HistoryType = _creditType;
                history.CustomerName = creditHistory.CustomerName;
                history.CustomerLastBalance = creditHistory.CustomerLastBalance;
                CombineHistoryList.Add(history);
            }

            CombineHistoryList = CombineHistoryList.OrderBy(data => data.Time).ToList();   
         
            foreach(var combineHistory in CombineHistoryList)
            {
                combineHistory.Id = index;
                index++;
            }
        }

        #endregion
    }
}
