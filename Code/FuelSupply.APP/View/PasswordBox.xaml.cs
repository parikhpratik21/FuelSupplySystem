using FuelSupply.APP.ViewModel;
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
    public partial class PasswordBox : MetroWindow
    {
        #region "Declaration"
        private MainWindow oMainWindow;
        private PasswordViewModel oViewModel;
        #endregion
        public PasswordBox(Window pOwner, PasswordViewModel pViewModel)
        {
            InitializeComponent();

            oViewModel = pViewModel;            
            this.DataContext = oViewModel;
        }       

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool result = oViewModel.GetPassword(txtNewPassword.Password);
            if (result == true)
            {
                this.DialogResult = true;
                this.Close();
            }    
            else
            {
                txtNewPassword.Password = string.Empty;
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
