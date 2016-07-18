using FuelSupply.APP.ViewModel;
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
        #endregion
        public Add_Fuel(Customer pSelectedCustomer, Window pOwnerWindow)
        {
            InitializeComponent();
            oViewModel = new AddFuelViewModel(pOwnerWindow);
            oViewModel.SelectedCustomer = pSelectedCustomer;
            this.DataContext = oViewModel;

            oMainWindow = (MainWindow)pOwnerWindow;
        }     

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool result = oViewModel.AddFuel();
            if (result == true)
            {
                this.Close();
                oMainWindow.btnCustomer_Click(null, null);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
