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
        public CustomerDisplay(Window pOwnerWindow, CustomerDisplayViewModel pViewModel)
        {
            InitializeComponent();

            oMainWindow = (MainWindow)pOwnerWindow;
            customerDisplayModel = pViewModel;
            this.DataContext = customerDisplayModel;
            customerDisplayModel.openAddFuelForSelectedCustomer += OpenAddFuelForm;
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            AddEditCustomerViewModel oAddCustomerViewModel = new AddEditCustomerViewModel(oMainWindow);
            oAddCustomerViewModel.SelectedCustomer = new Customer();
            oAddCustomerViewModel.Title = "Add Customer";

            AddEditCustomer oAddEditForm = new AddEditCustomer(oMainWindow, oAddCustomerViewModel);
            oAddEditForm.DataContext = oAddCustomerViewModel;

            oMainWindow.mainModel.ContentWindow = oAddEditForm;

            customerDisplayModel.DeregisterFingerPrinttouchEvent();
        }

        private void btnEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            AddEditCustomerViewModel oAddCustomerViewModel = new AddEditCustomerViewModel(oMainWindow);
            oAddCustomerViewModel.SelectedCustomer = customerDisplayModel.SelectedCustomer;
            oAddCustomerViewModel.Title = "Edit Customer";

            AddEditCustomer oAddEditForm = new AddEditCustomer(oMainWindow, oAddCustomerViewModel);
            oAddEditForm.DataContext = oAddCustomerViewModel;

            oMainWindow.mainModel.ContentWindow = oAddEditForm;

            customerDisplayModel.DeregisterFingerPrinttouchEvent();
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

            customerDisplayModel.OnPropertyChanged("IsAddCreditVisible");
        }

        private void btnAddFuel_Click(object sender, RoutedEventArgs e)
        {
            customerDisplayModel.DeregisterFingerPrinttouchEvent();

            AddFuelViewModel oViewModel = new AddFuelViewModel(oMainWindow);
            Add_Fuel oAddFuel = new Add_Fuel(customerDisplayModel.SelectedCustomer, oMainWindow, oViewModel);
            oAddFuel.Owner = oMainWindow;
            oAddFuel.ShowDialog();

            if (sender != null)
                customerDisplayModel.RegisterFingerPrinttouchEvent();
        }

        private void dgCustomerList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(customerDisplayModel.IsAdminUser == true)
                btnEditCustomer_Click(null, null);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(customerDisplayModel != null && customerDisplayModel.CustomerList != null && customerDisplayModel.CustomerList.Count() > 0)
            {
                dgCustomerList.SelectedIndex = 0;
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Color col = (Color)ColorConverter.ConvertFromString("#a5a5a5");
            txtSearch.Background = new SolidColorBrush(col);
        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            Color col = (Color)ColorConverter.ConvertFromString("#c5c5c5");
            txtSearch.Background = new SolidColorBrush(col);
        }
       
        private void OpenAddFuelForm()
        {
            btnAddFuel_Click(null, null);
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword oChangePassword = new ChangePassword(oMainWindow, customerDisplayModel.SelectedCustomer);
            oChangePassword.Owner = oMainWindow;
            oChangePassword.ShowDialog();
        }
    }
}
