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
using FuelSupply.APP.ViewModel;

namespace FuelSupply.APP.View
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : UserControl
    {
        #region "Declaration"
        private ProfileViewModel profileModel;       
        #endregion

        public Profile(Window pOwnerWindow)
        {
            InitializeComponent();

            profileModel = new ProfileViewModel(pOwnerWindow);
            this.DataContext = profileModel;
        }
    }
}
