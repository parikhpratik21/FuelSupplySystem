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
    /// Interaction logic for AddCredit.xaml
    /// </summary>
    public partial class AddCredit : MetroWindow
    {
        #region "Declaration"
        private AddCreditViewModel oViewModel;
        private MainWindow oMainWindow;
        #endregion
        public AddCredit(Customer pSelectedCustomer, Window pOwnerWindow)
        {
            InitializeComponent();
            oViewModel = new AddCreditViewModel(pOwnerWindow);
            oViewModel.SelectedCustomer = pSelectedCustomer;
            this.DataContext = oViewModel;

            oMainWindow = (MainWindow)pOwnerWindow;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool result = oViewModel.AddCredit();
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

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
