using FuelSupply.APP.View;
using FuelSupply.APP.ViewModel;
using FuelSupply.BAL.Manager.Common;
using FuelSupply.DAL.Database_Entity;
using FuelSupply.DAL.Entity.UserEntity;
using FuelSupply.DAL.Provider.Common;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FuelSupply.APP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region "Declaration"
        public MainViewModel mainModel;
        private static LoadingWindow objLoadingForm;

        private delegate void ShowMessageDelegate();
        private delegate void StopLoader();
        private delegate void EnabledGridDelegate();

        LoginWindow oLoginWindow;
        LoginViewModel oLogInViewModel;
        #endregion
        public MainWindow()
        {
            InitializeComponent();

            mainModel = new MainViewModel(this);
            this.DataContext = mainModel;        
        }       

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {            
            OpenLoginWindows();
        }

        private void OpenLoginWindows()
        {
            oLogInViewModel = new LoginViewModel(this);
            oLoginWindow = new LoginWindow(this, oLogInViewModel);
            
            oLoginWindow.Owner = this;
            oLoginWindow.Show();          
            MainGrid.IsEnabled = false;
            this.IsEnabled = false;

            oLoginWindow.DataContext = oLogInViewModel;
        }

        public void SuccessLogIn()
        {
            CloseLoginPopup();
            btnProfile_Click(null, null);
        }

        public void CloseLoginPopup()
        {
            if (oLoginWindow != null)
            {
                oLoginWindow.Close();
                oLoginWindow = null;
            }
            EnabledGrid();
        }
        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        public void ShowMessage()
        {
            if (this.Dispatcher.CheckAccess())
            { 
                MessageManager.ShowErrorMessage(Constant.MSG_GLOBAL_EXCEPTION, this);
            }
            else
            {
                this.Dispatcher.Invoke(new ShowMessageDelegate(ShowMessage));
            }
        }

        public void EnabledGrid()
        {
            if (MainGrid.Dispatcher.CheckAccess())
            {
                MainGrid.IsEnabled = true;
                this.IsEnabled = true;
            }
            else
            {
                MainGrid.Dispatcher.Invoke(new EnabledGridDelegate(EnabledGrid));
            }
        }

        public void stopProcess()
        {

            if (objLoadingForm != null)
            {
                if (objLoadingForm.Dispatcher.CheckAccess())
                {
                    objLoadingForm.Activate();
                    objLoadingForm.Close();
                    objLoadingForm = null;
                }
                else
                {
                    objLoadingForm.Dispatcher.Invoke(new StopLoader(stopProcess));
                }
            }
        }

        public void Loading_Timer_Tick(object sender, EventArgs e)
        {
            stopProcess();
            ShowMessage();
            EnabledGrid();

            LogManager.logMessage("MainWindow.xaml.cs", "Loading_Timer_Tick : Loader Close");
        }

        public void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            Profile oProfile = new Profile(this, oLogInViewModel.Loggeduser);
            mainModel.ContentWindow = oProfile;
        }

        public void btnUser_Click(object sender, RoutedEventArgs e)
        {
            UserDisplay oUserDisplay = new UserDisplay(this);
            mainModel.ContentWindow = oUserDisplay;
        }

        public void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerDisplay oCustomer = new CustomerDisplay(this);
            mainModel.ContentWindow = oCustomer;
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            History oHistory  = new History();
            mainModel.ContentWindow = oHistory;
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            OpenLoginWindows();     
        }

        private void MetroWindow_Activated(object sender, EventArgs e)
        {
            if (oLoginWindow != null)
            {
                oLoginWindow.Focus();
                oLoginWindow.txtUseName.Focus();
            }
        }             
    }
}
