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

        public delegate bool DisplayConfirmationMessage(string sErrorMsg);
        public event DisplayConfirmationMessage showConfirmationMessage; 
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
                if (sErrorString == AddFuelViewModel.FingerPrintNotMatchMessage)
                {
                    bool confirmationResult = ShowConfirmationMessage("Fingerprint doesn't match, Do you want to continue with password?");
                    if(confirmationResult == true)
                    {
                        //open password box and ask for password
                        PasswordViewModel oPasswordViewModel = new PasswordViewModel(this);
                        oPasswordViewModel.CustomerPassword = oViewModel.SelectedCustomer.Password;
                        PasswordBox oPassword = new PasswordBox(this, oPasswordViewModel);
                        oPasswordViewModel.MainWindow = oPassword;
                        bool? passwordResult = oPassword.ShowDialog();
                        if(passwordResult == true)
                        {
                            string sErrorMsg = string.Empty;
                            oViewModel.AddFuelByPassword(ref sErrorMsg);
                            if(sErrorMsg != string.Empty)
                            {
                                ShowErrorMessage(sErrorString); 
                            }
                            else
                            {
                                this.Close();
                                oMainWindow.btnCustomer_Click(null, null);
                            }

                        }                       
                    }
                }
                else
                {
                    ShowErrorMessage(sErrorString);  
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

        public bool ShowConfirmationMessage(string pErrorMsg)
        {
            if (oMainWindow.Dispatcher.CheckAccess())
            {
                if (pErrorMsg != string.Empty)
                    return MessageManager.ShowConfirmationMessage(pErrorMsg, oMainWindow);
                else
                    return false;
            }
            else
                return (bool)oMainWindow.Dispatcher.Invoke(new DisplayConfirmationMessage(ShowConfirmationMessage), new object[] { pErrorMsg });

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
