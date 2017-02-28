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
    public partial class UpdateFuelHistory : MetroWindow
    {
         #region "Declaration"
        private UpdateFuelHistoryViewModel oViewModel;
        private MainWindow oMainWindow;

        public delegate void DisplayErrorMessage(string sErrorMsg);
        public event DisplayErrorMessage showErrorMessage;

        public delegate bool DisplayConfirmationMessage(string sErrorMsg);
        public event DisplayConfirmationMessage showConfirmationMessage; 
        #endregion
        public UpdateFuelHistory(UpdateFuelHistoryViewModel pViewModel, Window pOwnerWindow)
        {
            InitializeComponent();
            oViewModel = pViewModel;            
            this.DataContext = oViewModel;

            oMainWindow = (MainWindow)pOwnerWindow;
        }     
       
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string sErrorString = string.Empty;
            bool result = oViewModel.UpdateFuelHistory(ref sErrorString);
            if (result == true)
            {
                if (sErrorString == UpdateFuelHistoryViewModel.CreditConfirmationMessage)
                {
                    var confirmationResult = ShowConfirmationMessage(sErrorString);
                    if(confirmationResult == true)
                    {
                        var deductResult = oViewModel.DebitCreditandUpdateActualAmount(ref sErrorString);
                        if(deductResult == false)
                        {
                            ShowErrorMessage(sErrorString);
                            return;
                        }
                    }
                    else
                    {
                        var deductResult = oViewModel.UpdateActualAmount(ref sErrorString);
                        if (deductResult == false)
                        {
                            ShowErrorMessage(sErrorString);
                            return;
                        }
                    }
                }

                this.Close();
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
