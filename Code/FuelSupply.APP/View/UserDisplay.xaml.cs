﻿using FuelSupply.APP.ViewModel;
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
using Microsoft.Win32;
using FuelSupply.BAL.Manager.Common;

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

        public UserDisplay(Window pOwnerWindow, UserDisplayViewModel pViewModel)
        {
            InitializeComponent();

            oMainWindow = (MainWindow)pOwnerWindow;
            userDisplayViewModel = pViewModel;
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

        private void dgUserList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnEditUser_Click(null, null);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (userDisplayViewModel != null && userDisplayViewModel.UserList != null && userDisplayViewModel.UserList.Count() > 0)
            {
                dgUserList.SelectedIndex = 0;
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

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void btnDatabaseBackup_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog oDbBackup = new SaveFileDialog();
            oDbBackup.FileName = "FuelSupplySystem";
            oDbBackup.Filter = "BAK Files (*.bak)|*.bak;";

            if(oDbBackup.ShowDialog() == true)
            {
               bool result = userDisplayViewModel.BackUpResoreDatabase("backup", oDbBackup.FileName);
               if (result == true)
                   MessageManager.ShowInformationMessage("Database backup complete successfully.", oMainWindow);
               else
                   MessageManager.ShowInformationMessage("Error while database backup, Please try backup.", oMainWindow);
            }
        }

        private void btnDatabaseRestore_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog oDbRestore = new OpenFileDialog();
            oDbRestore.FileName = "FuelSupplySystem";
            oDbRestore.Filter = "BAK Files (*.bak)|*.bak;";

            if (oDbRestore.ShowDialog() == true)
            {
                bool result = userDisplayViewModel.BackUpResoreDatabase("restore", oDbRestore.FileName);
                if (result == true)
                {
                    MessageManager.ShowInformationMessage("Database restore complete successfully.", oMainWindow);

                    MessageManager.ShowInformationMessage("Please restart system.", oMainWindow);

                    Environment.Exit(1);
                }
                else
                    MessageManager.ShowInformationMessage("Error while database restore, Please try backup.", oMainWindow);
            }
        }

        private void btnFuelRate_Click(object sender, RoutedEventArgs e)
        {
            FuelRateViewModel oViewModel = new FuelRateViewModel(oMainWindow);
            FuelRate oFuelRate = new FuelRate(oMainWindow, oViewModel);
            oFuelRate.ShowDialog();
        }
    }
}
