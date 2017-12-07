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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FuelSupply.APP.View
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HistoryHomeScreen : UserControl
    {
        #region "Declaration"
        private HistoryHomeScreenViewModel ViewModel;
        private Window OwnerWindow;
        private static LoadingWindow objLoadingForm;

        private delegate void ShowMessageDelegate();
        private delegate void StopLoader();
        private delegate void EnabledGridDelegate();
        private delegate void StartLoader(object LoadingParam);
        private delegate void StartProcessDelegate(string pIntializeText);

        LoginWindow oLoginWindow;       
        #endregion

        public HistoryHomeScreen(Window pOwnerWindow, HistoryHomeScreenViewModel oViewModel)
        {
            InitializeComponent();
            this.DataContext = oViewModel;
            ViewModel = oViewModel;
            OwnerWindow = pOwnerWindow;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FuelHistory oFuelHistory = new FuelHistory(OwnerWindow, ViewModel.FuelViewModel);
            ViewModel.FuelHistoryContentWindow = oFuelHistory;

            CreditHistory oCreditHistory = new CreditHistory(OwnerWindow, ViewModel.CreditViewModel);
            ViewModel.CreditHistoryContentWindow = oCreditHistory;

            CombineHistory oCombineHistory = new CombineHistory(OwnerWindow, ViewModel.CombineViewModel);
            ViewModel.CombineHistoryContentWindow = oCombineHistory;
        }
    }
}
