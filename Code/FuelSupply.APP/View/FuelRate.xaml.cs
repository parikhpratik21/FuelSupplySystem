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
    public partial class FuelRate : MetroWindow
    {
        #region "Declaration"
        private MainWindow oMainWindow;
        private FuelRateViewModel oViewModel;
        #endregion
        public FuelRate(Window pOwner, FuelRateViewModel pViewModel)
        {
            InitializeComponent();

            oViewModel = pViewModel;            
            this.DataContext = oViewModel;
        }            

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool result = oViewModel.SaveFuelTypeData();
            if (result == true)
            {
                this.DialogResult = true;
                this.Close();
            }                
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
     
    }
}
