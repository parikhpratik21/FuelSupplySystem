﻿using FuelSupply.APP.ViewModel;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FuelSupply.APP.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        #region "Declaration"
        MainWindow oMainWindows;
        LoginViewModel oViewModel;
        #endregion
        public LoginWindow(Window pOwnerWindow, LoginViewModel pViewModel)
        {
            InitializeComponent();

            oMainWindows = (MainWindow)pOwnerWindow;
            oViewModel = pViewModel;
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            oMainWindows.EnabledGrid();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            oViewModel.Login(txtPassword.Password);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}