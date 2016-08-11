using FuelSupply.APP.ViewModel;
using FuelSupply.DAL.Entity.UserEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FuelSupply.APP.View
{
    /// <summary>
    /// Interaction logic for AddEditUser.xaml
    /// </summary>
    public partial class AddEditCustomer : UserControl, INotifyPropertyChanged
    {
        #region "Declaration"
        private AddEditCustomerViewModel viewModel;
        private MainWindow oMainWindow;
        #endregion      

        public AddEditCustomer(Window pOwnerWindow, AddEditCustomerViewModel pViewModel)
        {
            InitializeComponent();

            oMainWindow = (MainWindow)pOwnerWindow;
            viewModel = pViewModel;
            this.DataContext = viewModel;
        }

        #region EventHandlers (1)
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        #endregion

        private void btnSaveUser_Click(object sender, RoutedEventArgs e)
        {
            bool result = viewModel.AddEditCustomer();
            if (result == true)
                oMainWindow.btnCustomer_Click(null, null);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            oMainWindow.btnCustomer_Click(null, null);
        }

        private void txtCustomerType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.OnPropertyChanged("IsKeyCustomerListEnable");
        }

        private void btnAddFingerPrint_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
