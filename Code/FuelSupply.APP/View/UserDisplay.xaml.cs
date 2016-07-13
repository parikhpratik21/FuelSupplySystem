using FuelSupply.APP.ViewModel;
using FuelSupply.DAL.Entity.UserEntity;
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
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class UserDisplay : UserControl
    {
        #region "Declaration"
        private UserDisplayViewModel userDisplayViewModel;
        private MainWindow oMainWindow;
        #endregion

        public UserDisplay(Window pOwnerWindow)
        {
            InitializeComponent();

            oMainWindow = (MainWindow)pOwnerWindow;
            userDisplayViewModel = new UserDisplayViewModel(pOwnerWindow);
            this.DataContext = userDisplayViewModel;
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            AddEditUserViewModel oAddUserViewModel = new AddEditUserViewModel(oMainWindow);
            oAddUserViewModel.SelectedUser = new User();
            oAddUserViewModel.Title = "Add User";

            AddEditUserViewModel viewModel = new AddEditUserViewModel(oMainWindow);
            viewModel.SelectedUser = oAddUserViewModel.SelectedUser;
            AddEditUser oAddEditForm = new AddEditUser(oMainWindow, viewModel);
            oAddEditForm.DataContext = oAddUserViewModel;            

            oMainWindow.mainModel.ContentWindow = oAddEditForm;
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            AddEditUserViewModel oAddUserViewModel = new AddEditUserViewModel(oMainWindow);
            oAddUserViewModel.SelectedUser = userDisplayViewModel.SelectedUser;
            oAddUserViewModel.Title = "Edit User";

            AddEditUserViewModel viewModel = new AddEditUserViewModel(oMainWindow);
            viewModel.SelectedUser = oAddUserViewModel.SelectedUser;
            AddEditUser oAddEditForm = new AddEditUser(oMainWindow, viewModel);
            oAddEditForm.DataContext = oAddUserViewModel;            

            oMainWindow.mainModel.ContentWindow = oAddEditForm;
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            userDisplayViewModel.DeleteUser();
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword oChangePassword = new ChangePassword(oMainWindow, userDisplayViewModel.SelectedUser);
            oChangePassword.Owner = oMainWindow;
            oChangePassword.ShowDialog();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userDisplayViewModel.SelectedUser = (User)dgUserList.SelectedItem;
        }
    }
}
