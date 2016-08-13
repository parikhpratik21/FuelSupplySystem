using FuelSupply.APP.ExportEntity;
using FuelSupply.BAL.Manager;
using FuelSupply.BAL.Manager.Common;
using FuelSupply.DAL.Entity.CreditEntity;
using FuelSupply.DAL.Entity.CustomerEntity;
using FuelSupply.DAL.Entity.Fuel;
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
    public class CreditHistoryViewModel : INotifyPropertyChanged
    {
        #region "Declaration"
        private List<HistoryType> _CreditHistoryTypeList;
        private HistoryType _SelectedCreditHistoryType;
        private MainWindow oMainWindow;
        private List<Customer> _CustomerList;
        private List<Customer> _KeyCustomerList;
        private List<User> _UserList;
        private List<PaymentType> _PaymentTypeList;

        private List<CreditHistory> _CreditHistoryList;
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
        public List<CreditHistory> CreditHistoryList
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
                    else if (_SelectedCreditHistoryType.Id == (int)SharedData.CreditHisotryByType.User)
                    {
                        return "Users: ";
                    }
                    else
                    {
                        return "Payment Type: ";
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
                    else if (_SelectedCreditHistoryType.Id == (int)SharedData.CreditHisotryByType.User)
                    {
                        return _UserList;
                    }
                    else
                    {
                        return _PaymentTypeList;
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

        public CreditHistoryViewModel(Window pOwnerWindow)
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

            oCreditHistoryType = new DAL.Entity.Fuel.HistoryType();
            oCreditHistoryType.Id = 4;
            oCreditHistoryType.Name = "Payment Type";
            _CreditHistoryTypeList.Add(oCreditHistoryType);       

            oMainWindow = (MainWindow)pOwnerWindow;

            _CustomerList = CustomerManager.GetAllCustomers();
            _KeyCustomerList = CustomerManager.GetAllKeyCustomer();
            _UserList = UserManager.GetAllUser();
            _PaymentTypeList = CustomerManager.GetAllPaymentTypes();

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

            PaymentType oAllpaymentType = new PaymentType();
            oAllpaymentType.Id = 0;
            oAllpaymentType.Name = "---All---";
            _PaymentTypeList.Insert(0, oAllpaymentType);

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
                            }
                            else
                            {
                                CreditHistoryList = CreditManager.GetCreditHistoryByKeyCustomerId((int)pSelectedId, pStartDate, pEndDate);
                            }
                        }
                        else if (_SelectedCreditHistoryType.Id == (int)SharedData.FuelHisotryByType.Customer)
                        {
                            if (pSelectedId == 0)
                            {
                                CreditHistoryList = CreditManager.GetCreditHistoryBetweenDates(pStartDate, pEndDate);
                            }
                            else
                            {
                                CreditHistoryList = CreditManager.GetCreditHistoryByCustomerId((int)pSelectedId, pStartDate, pEndDate);
                            }
                        }
                        else if (_SelectedCreditHistoryType.Id == (int)SharedData.FuelHisotryByType.User)
                        {
                            if (pSelectedId == 0)
                            {
                                CreditHistoryList = CreditManager.GetCreditHistoryBetweenDates(pStartDate, pEndDate);
                            }
                            else
                            {
                                CreditHistoryList = CreditManager.FetCreditHistoryByUserId((int)pSelectedId, pStartDate, pEndDate);
                            }
                        }
                        else
                        {
                            if (pSelectedId == 0)
                            {
                                CreditHistoryList = CreditManager.GetCreditHistoryBetweenDates(pStartDate, pEndDate);
                            }
                            else
                            {
                                CreditHistoryList = CreditManager.GetCreditHistoryByPaymentId((int)pSelectedId, pStartDate, pEndDate);
                            }
                        }
                    });                    
                }
                else
                {
                    MessageManager.ShowErrorMessage("Please select valid value", oMainWindow);
                }
            }
            return true;
        }

        public List<CreditHistoryExport> ConvertCreditHistoryToCreditHistoryExportEntity()
        {
            List<CreditHistoryExport> oCreditHistoryExportList = new List<CreditHistoryExport>();

            if (CreditHistoryList != null && CreditHistoryList.Count > 0)
            {
                CreditHistoryExport oExportEntity;
                foreach (CreditHistory oHistory in CreditHistoryList)
                {
                    oExportEntity = new CreditHistoryExport();
                    oExportEntity.ID = oHistory.Id;
                    if (oHistory.Customer != null)
                        oExportEntity.CustomerName = oHistory.Customer.Name;
                    else
                        oExportEntity.CustomerName = string.Empty;

                    oExportEntity.Date = DateTime.ParseExact(oHistory.Time.Value.ToString("dd/MM/yyyy hh:mm:ss tt"), "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy hh:mm:ss tt");
                    oExportEntity.CreditAmount = oHistory.CreditAmount;

                    if (oHistory.PaymentType1 != null)
                        oExportEntity.PaymentType = oHistory.PaymentType1.Name;
                    else
                        oExportEntity.PaymentType = "Cash";

                    if (oHistory.Customer != null && oHistory.Customer.Customer2 != null)
                        oExportEntity.KeyCustomer = oHistory.Customer.Customer2.Name;
                    else
                        oExportEntity.KeyCustomer = string.Empty;

                    if (oHistory.User != null)
                        oExportEntity.UserName = oHistory.User.Name;
                    else
                        oExportEntity.UserName = string.Empty;

                    oCreditHistoryExportList.Add(oExportEntity);
                }
            }

            return oCreditHistoryExportList;
        }     

        public void Export_Data_To_PDF(List<CreditHistoryExport> pHistoryList, string filename)
        {
            PropertyInfo[] headerInfo = typeof(CreditHistoryExport).GetProperties();

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
            foreach (CreditHistoryExport oHistory in pHistoryList)
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

                cellRow = new PdfPCell(new Phrase(oHistory.PaymentType));
                cellRow.HorizontalAlignment = 1;
                cellRow.VerticalAlignment = 1;
                if (rowIndex % 2 == 0)
                    cellRow.BackgroundColor = new iTextSharp.text.Color(224, 255, 255);
                pdfTable.AddCell(cellRow);

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

        public bool Export_Data_To_HTML(List<CreditHistoryExport> pHistoryList, string pFileName)
        {
            bool bResult = false;
            try
            {
                if (pHistoryList != null && pHistoryList.Count > 0)
                {
                    string sHeaderBackColor = @"""#00DFDF""";
                    string sRowBackColor = @"""#E0FFFF""";

                    StringBuilder sbHtml = new StringBuilder();
                    //create html & table
                    sbHtml.AppendLine("<html><body><center><" +
                                  "table border='1' cellpadding='10' cellspacing='0'>");
                    sbHtml.AppendLine("<tr bgcolor=" + sHeaderBackColor + ">");

                    PropertyInfo[] headerInfo = typeof(CreditHistoryExport).GetProperties();

                    // Create an array for the headers and add it to the
                    // worksheet starting at cell A1.
                    List<string> objHeaders = new List<string>();
                    for (int index = 0; index < headerInfo.Length; index++)
                    {
                        objHeaders.Add(headerInfo[index].Name);
                        sbHtml.AppendLine("<td align='center' valign='middle'><b>" +
                                       headerInfo[index].Name + "</b></td>");
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
                            var data = typeof(CreditHistoryExport).InvokeMember(objHeaders[i].ToString(), BindingFlags.GetProperty, null, item, null);

                            sbHtml.AppendLine("<td align='center' valign='middle'>" + data.ToString() + "</td>");
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

        #endregion
    }
}
