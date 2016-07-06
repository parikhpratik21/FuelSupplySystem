using FuelSupply.APP.ViewModel;
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
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class CustomerDisplay : UserControl
    {
        #region "Declaration"
        private CustomerDisplayViewModel customerDisplayModel;
        #endregion
        public CustomerDisplay(Window pOwnerWindow)
        {
            InitializeComponent();

            customerDisplayModel = new CustomerDisplayViewModel(pOwnerWindow);
            this.DataContext = customerDisplayModel;
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditCustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddCredit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
