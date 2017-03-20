using FuelSupply.APP.ExportEntity;
using FuelSupply.APP.Helper;
using FuelSupply.APP.ViewModel;
using FuelSupply.BAL.Manager.Common;
using FuelSupply.DAL.Entity.Fuel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace FuelSupply.APP.View
{
    /// <summary>
    /// Interaction logic for CreditHistory.xaml
    /// </summary>
    public partial class CreditHistory : UserControl
    {
        #region "Declaration"
        public CreditHistoryViewModel viewModel;
        public MainWindow oMainWindow;

        public delegate void DisplayErrorMessage(string sErrorMsg);
        public event DisplayErrorMessage showErrorMessage;
        #endregion

        public CreditHistory(Window pOwnerWindow,CreditHistoryViewModel pViewModel)
        {
            InitializeComponent();

            oMainWindow = (MainWindow)pOwnerWindow;
            viewModel = pViewModel;
            this.DataContext = viewModel;

            dgCreditHistoryList.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            dgCreditHistoryList.SelectionMode = DataGridSelectionMode.Extended;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cmbHistoryBy.Focus();
            cmbHistoryBy.SelectedIndex = 0;

            dpEndTime.SelectedDate = DateTime.Now;
            dpStartTime.SelectedDate = DateTime.Now;
        }

        private void btnExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel<CreditHistoryExport, CreditHistoryExportList> oExcelSheet = new ExportToExcel<CreditHistoryExport, CreditHistoryExportList>();
            List<CreditHistoryExport> oCreditHistoryExportList = viewModel.ConvertCreditHistoryToCreditHistoryExportEntity();
            ICollectionView view = CollectionViewSource.GetDefaultView(oCreditHistoryExportList);
            oExcelSheet.dataToPrint = (List<CreditHistoryExport>)view.SourceCollection;
            oExcelSheet.GenerateReport();    
        }

        private void btnExportToHTML_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog oSaveFileDialog = new SaveFileDialog();
            oSaveFileDialog.Filter = "HTML Files (*.html)|*.html;";
            if (oSaveFileDialog.ShowDialog() == true)
            {
                List<CreditHistoryExport> oCreditHistoryExportList = viewModel.ConvertCreditHistoryToCreditHistoryExportEntity();
                bool result =  viewModel.Export_Data_To_HTML(oCreditHistoryExportList, oSaveFileDialog.FileName);

                if (result == true)
                {
                    System.Diagnostics.Process.Start(oSaveFileDialog.FileName);
                }
                else
                {
                    ShowErrorMessage("Error while exporting in PDF, please contact administrator.");
                }
            }
        }

        public void ShowErrorMessage(string pErrorMsg)
        {
            if (oMainWindow.Dispatcher.CheckAccess())
            {
                if (pErrorMsg != string.Empty)
                    MessageManager.ShowErrorMessage(pErrorMsg, oMainWindow);
            }
            else
                oMainWindow.Dispatcher.Invoke(new DisplayErrorMessage(ShowErrorMessage), new object[] { pErrorMsg });
        }

        private void btnExportToCSV_Click(object sender, RoutedEventArgs e)
        {
            dgCreditHistoryList.SelectAllCells();
            ApplicationCommands.Copy.Execute(null, dgCreditHistoryList);
            dgCreditHistoryList.UnselectAllCells();

            String result = (string)Clipboard.GetData(DataFormats.Text);
            Clipboard.Clear();

            SaveFileDialog oSaveFileDialog = new SaveFileDialog();
            oSaveFileDialog.Filter = "CSV Files (*.csv)|*.csv;";
            if (oSaveFileDialog.ShowDialog() == true)
            {
                System.IO.File.WriteAllText(oSaveFileDialog.FileName, result);

                System.Diagnostics.Process.Start(oSaveFileDialog.FileName);
            }
        }

        private void btnExportToPDF_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog oSaveFileDialog = new SaveFileDialog();
            oSaveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            if (oSaveFileDialog.ShowDialog() == true)
            {
                List<CreditHistoryExport> oFuelHistoryExportList = viewModel.ConvertCreditHistoryToCreditHistoryExportEntity();
                viewModel.Export_Data_To_PDF(oFuelHistoryExportList, oSaveFileDialog.FileName);

                System.Diagnostics.Process.Start(oSaveFileDialog.FileName);
            }
        }

        private void cmbHistoryBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.SelectedCreditHistoryType = (HistoryType)cmbHistoryBy.SelectedItem;

            viewModel.OnPropertyChanged("HistoryValueList");
            viewModel.OnPropertyChanged("HistoryInfoHeader");

            cbHistoryTypeValue.SelectedIndex = 0;
        }

        private async void btnApply_Click(object sender, RoutedEventArgs e)
        {
            DateTime? pStartTime = DateTime.Now;
            DateTime? pEndTime = DateTime.Now;

            if (dpStartTime.SelectedDate != null && dpStartTime.SelectedDate.Value != null)
            {
                pStartTime = new DateTime(dpStartTime.SelectedDate.Value.Year, dpStartTime.SelectedDate.Value.Month,
                    dpStartTime.SelectedDate.Value.Day, 0, 0, 0);
            }

            if (dpEndTime.SelectedDate != null && dpEndTime.SelectedDate.Value != null)
            {
                pEndTime = new DateTime(dpEndTime.SelectedDate.Value.Year, dpEndTime.SelectedDate.Value.Month,
                    dpEndTime.SelectedDate.Value.Day, 23, 59, 59);
            }

            oMainWindow.startProcess("Loading...");
            await viewModel.GetFuelHistory(pStartTime, pEndTime, (int?)cbHistoryTypeValue.SelectedValue);
            oMainWindow.stopProcess();
            oMainWindow.EnabledGrid();
        }
    }
}
