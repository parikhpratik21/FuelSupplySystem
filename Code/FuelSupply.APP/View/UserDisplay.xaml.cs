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
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class UserDisplay : UserControl
    {
        #region "Declaration"
        private UserDisplayViewModel userDisplayModel;
        #endregion

        public UserDisplay(Window pOwnerWindow)
        {
            InitializeComponent();

            userDisplayModel = new UserDisplayViewModel(pOwnerWindow);
            this.DataContext = userDisplayModel;
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
