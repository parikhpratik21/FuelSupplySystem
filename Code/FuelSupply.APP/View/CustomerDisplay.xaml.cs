using FuelSupply.APP.ViewModel;
using FuelSupply.DAL.Entity.CustomerEntity;
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
        private MainWindow oMainWindow;
        #endregion
        public CustomerDisplay(Window pOwnerWindow)
        {
            InitializeComponent();

            oMainWindow = (MainWindow)pOwnerWindow;
            customerDisplayModel = new CustomerDisplayViewModel(pOwnerWindow);
            this.DataContext = customerDisplayModel;
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            AddEditCustomerViewModel oAddCustomerViewModel = new AddEditCustomerViewModel(oMainWindow);
            oAddCustomerViewModel.SelectedCustomer = new Customer();
            oAddCustomerViewModel.Title = "Add Customer";

            AddEditCustomer oAddEditForm = new AddEditCustomer(oMainWindow, oAddCustomerViewModel);
            oAddEditForm.DataContext = oAddCustomerViewModel;

            oMainWindow.mainModel.ContentWindow = oAddEditForm;
        }

        private void btnEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            AddEditCustomerViewModel oAddCustomerViewModel = new AddEditCustomerViewModel(oMainWindow);
            oAddCustomerViewModel.SelectedCustomer = customerDisplayModel.SelectedCustomer;
            oAddCustomerViewModel.Title = "Edit Customer";

            AddEditCustomer oAddEditForm = new AddEditCustomer(oMainWindow, oAddCustomerViewModel);
            oAddEditForm.DataContext = oAddCustomerViewModel;

            oMainWindow.mainModel.ContentWindow = oAddEditForm;
        }

        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            customerDisplayModel.DeleteCustomer();
        }

        private void btnAddCredit_Click(object sender, RoutedEventArgs e)
        {
            AddCredit oAddCredit = new AddCredit(customerDisplayModel.SelectedCustomer, oMainWindow);
            oAddCredit.Owner = oMainWindow;
            oAddCredit.ShowDialog();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            customerDisplayModel.SelectedCustomer = (Customer)dgCustomerList.SelectedItem;
        }

        private void btnAddFuel_Click(object sender, RoutedEventArgs e)
        {
            Add_Fuel oAddFuel = new Add_Fuel(customerDisplayModel.SelectedCustomer,oMainWindow);
            oAddFuel.Owner = oMainWindow;
            oAddFuel.ShowDialog();
        }

        private void dgCustomerList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnEditCustomer_Click(null, null);
        }
    }
}
