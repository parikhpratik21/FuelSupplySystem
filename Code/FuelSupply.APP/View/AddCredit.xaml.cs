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
        #endregion
        public AddCredit(Customer pSelectedCustomer)
        {
            InitializeComponent();
            oViewModel = new AddCreditViewModel();
            oViewModel.SelectedCustomer = pSelectedCustomer;
            this.DataContext = oViewModel;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

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
