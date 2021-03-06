﻿using FuelSupply.APP.ViewModel;
using FuelSupply.DAL.Entity.CustomerEntity;
using FuelSupply.DAL.Entity.UserEntity;
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
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : MetroWindow
    {
        #region "Declaration"
        private MainWindow oMainWindow;
        private ChangePasswordViewModel oViewModel;
        #endregion
        public ChangePassword(Window pOwner, User pSelectedUser)
        {
            InitializeComponent();

            oViewModel = new ChangePasswordViewModel(pOwner);
            oViewModel.SelectedUser = pSelectedUser;
            this.DataContext = oViewModel;
        }

        public ChangePassword(Window pOwner, Customer pSelectedCustomer)
        {
            InitializeComponent();

            oViewModel = new ChangePasswordViewModel(pOwner);
            oViewModel.SelectedCustomer = pSelectedCustomer;
            this.DataContext = oViewModel;
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool result = oViewModel.ChangePassword(txtNewPassword.Password,txtConfirmPassword.Password);
            if (result == true)
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtNewPassword.Focus();
        }
    }
}
