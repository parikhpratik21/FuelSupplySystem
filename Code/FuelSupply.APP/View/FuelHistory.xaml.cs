using FuelSupply.APP.ViewModel;
using FuelSupply.DAL.Entity.Fuel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;
using FuelSupply.APP.Helper;
using System.ComponentModel;
using FuelSupply.APP.ExportEntity;

namespace FuelSupply.APP.View
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class FuelHistory : UserControl
    {
        #region "Declaration"
        public FuelHistoryViewModel viewModel;
        public MainWindow oMainWindow;
        #endregion
        public FuelHistory(Window pOwnerWindow)
        {
            InitializeComponent();
            oMainWindow = (MainWindow)pOwnerWindow;
            viewModel = new FuelHistoryViewModel(pOwnerWindow);
            this.DataContext = viewModel;

            dgHistoryList.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            dgHistoryList.SelectionMode = DataGridSelectionMode.Extended;
        }

        private void btnExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel<FuelHistoryExport, FuelHistoryExportList> oExcelSheet = new ExportToExcel<FuelHistoryExport, FuelHistoryExportList>();
            List<FuelHistoryExport> oFuelHistoryExportList = viewModel.ConvertFuelHistoryToFuelHistoryExportEntity();
            ICollectionView view = CollectionViewSource.GetDefaultView(oFuelHistoryExportList);
            oExcelSheet.dataToPrint = (List<FuelHistoryExport>)view.SourceCollection;
            oExcelSheet.GenerateReport();           
        }

        private void btnExportToWord_Click(object sender, RoutedEventArgs e)
        {         
            SaveFileDialog oSaveFileDialog = new SaveFileDialog();
            oSaveFileDialog.Filter = "Image Files (*.doc, *.docx)|*.doc;*.docx";
            if (oSaveFileDialog.ShowDialog() == true)
            {
                List<FuelHistoryExport> oFuelHistoryExportList = viewModel.ConvertFuelHistoryToFuelHistoryExportEntity();
                viewModel.Export_Data_To_Word(oFuelHistoryExportList,oSaveFileDialog.FileName);                
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cmbHistoryBy.Focus();
            cmbHistoryBy.SelectedIndex = 0;

            dpEndTime.SelectedDate = DateTime.Now;
            dpStartTime.SelectedDate = DateTime.Now;
        }

        private void cmbHistoryBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.SelectedFuelHistoryType = (HistoryType)cmbHistoryBy.SelectedItem;

            viewModel.OnPropertyChanged("HistoryValueList");
            viewModel.OnPropertyChanged("HistoryInfoHeader");

            cbHistoryTypeValue.SelectedIndex = 0;
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
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

            viewModel.GetFuelHistory(pStartTime, pEndTime, (int?)cbHistoryTypeValue.SelectedValue);
        }

        private void btnExportToPDF_Click(object sender, RoutedEventArgs e)
        {            
            SaveFileDialog oSaveFileDialog = new SaveFileDialog();
            oSaveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            if (oSaveFileDialog.ShowDialog() == true)
            {
                List<FuelHistoryExport> oFuelHistoryExportList = viewModel.ConvertFuelHistoryToFuelHistoryExportEntity();
                viewModel.Export_Data_To_PDF(oFuelHistoryExportList, oSaveFileDialog.FileName);
            }
        }

        private void btnExportToCSV_Click(object sender, RoutedEventArgs e)
        {
            dgHistoryList.SelectAllCells();
            ApplicationCommands.Copy.Execute(null, dgHistoryList);
            dgHistoryList.UnselectAllCells();

            String result = (string)Clipboard.GetData(DataFormats.Text);
            Clipboard.Clear();

            SaveFileDialog oSaveFileDialog = new SaveFileDialog();
            oSaveFileDialog.Filter = "CSV Files (*.csv)|*.csv;";
            if (oSaveFileDialog.ShowDialog() == true)
            {
                System.IO.File.WriteAllText(oSaveFileDialog.FileName, result);
            }
        }
    }    


}
