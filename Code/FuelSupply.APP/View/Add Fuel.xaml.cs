using FuelSupply.APP.ViewModel;
using FuelSupply.BAL.Manager.Common;
using FuelSupply.DAL.Entity.CustomerEntity;
using MahApps.Metro.Controls;
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

namespace FuelSupply.APP.View
{
    /// <summary>
    /// Interaction logic for Add_Fuel.xaml
    /// </summary>
    public partial class Add_Fuel : MetroWindow
    {
         #region "Declaration"
        private AddFuelViewModel oViewModel;
        private MainWindow oMainWindow;

        public delegate void DisplayErrorMessage(string sErrorMsg);
        public event DisplayErrorMessage showErrorMessage; 
        #endregion
        public Add_Fuel(Customer pSelectedCustomer, Window pOwnerWindow, AddFuelViewModel pViewModel)
        {
            InitializeComponent();
            oViewModel = pViewModel;
            oViewModel.SelectedCustomer = pSelectedCustomer;
            this.DataContext = oViewModel;

            oMainWindow = (MainWindow)pOwnerWindow;
        }     

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string sErrorString = string.Empty;
            bool result = oViewModel.AddFuel(ref sErrorString);
            if (result == true)
            {
                this.Close();
                oMainWindow.btnCustomer_Click(null, null);
            }
            else
            {
                ShowErrorMessage(sErrorString);
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
