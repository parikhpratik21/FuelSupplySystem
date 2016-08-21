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
    public partial class AddFingerPrint : MetroWindow
    {
         #region "Declaration"
        private AddFingerPrintViewModel oViewModel;
        private MainWindow oMainWindow; 
        #endregion
        public AddFingerPrint(Window pOwnerWindow, AddFingerPrintViewModel pViewmodel)
        {
            InitializeComponent();
            oViewModel = pViewmodel;           
            this.DataContext = oViewModel;

            this.Owner = pOwnerWindow;
            oMainWindow = (MainWindow)pOwnerWindow;
        }     

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string pErrorString = string.Empty;
            bool result = oViewModel.AddFingerPrint(ref pErrorString);
            if (pErrorString != string.Empty)
            {
                if (result == false)
                    MessageManager.ShowErrorMessage(pErrorString, this);
                else
                    MessageManager.ShowInformationMessage(pErrorString, this);                
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();          
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            string pErrorString = string.Empty;
            bool result = oViewModel.ClearSelectedTypeFingerPrint(ref pErrorString);
            if (pErrorString != string.Empty)
            {
                if (result == false)
                    MessageManager.ShowErrorMessage(pErrorString, this);
                else
                    MessageManager.ShowInformationMessage(pErrorString, this);
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            cmbFuelType.SelectedIndex = 0;
        }
    }
}
