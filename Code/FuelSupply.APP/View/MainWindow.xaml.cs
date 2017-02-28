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
using System.Threading;
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
        private delegate void StartLoader(object LoadingParam);
        private delegate void StartProcessDelegate(string pIntializeText);

        LoginWindow oLoginWindow;
        LoginViewModel oLogInViewModel;

        public struct LoadingParams
        {
            public string ContentText;
            public double xPos;
            public double yPos;
        };
        #endregion
        public MainWindow()
        {
            InitializeComponent();

            mainModel = new MainViewModel(this);
            this.DataContext = mainModel;        
        }       

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //btnUser_Click(null, null);
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
            mainModel.OnPropertyChanged("IsAdminUser");

            HomeScreen oHomeScreen = new HomeScreen();
            SetUserContentandData("Home", oHomeScreen);
            //btnProfile_Click(null, null);
            this.Focus();
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

        public void startProcess(string pIntializeText)
        {
            if (this.Dispatcher.CheckAccess())
            {
                double windowTop = this.Top;
                double windowLeft = this.Left;

                if (this.WindowState == System.Windows.WindowState.Maximized)
                {
                    windowTop = 0;
                    windowLeft = 0;
                }
                double loadingWindowTop = (windowTop + (this.ActualHeight / 2));
                double loadingWindowLeft = (windowLeft + (this.ActualWidth / 2));

                MainGrid.IsEnabled = false;
                LoadingParams oLoadingParams;
                if (objLoadingForm != null)
                {
                    EnabledGrid();
                    stopProcess();
                    ////OwnerForm.IsEnabled = true;
                }
                objLoadingForm = new LoadingWindow(this);

                oLoadingParams.ContentText = pIntializeText;
                oLoadingParams.xPos = loadingWindowLeft;
                oLoadingParams.yPos = loadingWindowTop;

                System.Threading.Thread oLoaderThread = new Thread(new ParameterizedThreadStart(startLoader));
                oLoaderThread.IsBackground = true;
                oLoaderThread.SetApartmentState(ApartmentState.STA);
                oLoaderThread.Priority = ThreadPriority.Highest;
                oLoaderThread.Start(oLoadingParams);
            }
            else
            {
                MainGrid.Dispatcher.Invoke(new StartProcessDelegate(startProcess), pIntializeText);
            }
        }

        #region "Method: startLoader(1)"
        /// <summary>
        /// Starts the loader.
        /// </summary>
        private static void startLoader(object pLoadingParams)
        {
            if (objLoadingForm == null)
                return;

            if (objLoadingForm.Dispatcher.CheckAccess())
            {
                LoadingParams oLodingParam = (LoadingParams)pLoadingParams;
                objLoadingForm.lblLoadingtext.Content = oLodingParam.ContentText;
                objLoadingForm.Owner = objLoadingForm.OwnerForm;

                double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
                double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

                oLodingParam.xPos = oLodingParam.xPos - (objLoadingForm.Width / 2);
                oLodingParam.yPos = oLodingParam.yPos - (objLoadingForm.Height / 2);

                if ((screenWidth < (oLodingParam.xPos + objLoadingForm.Width)) || (screenHeight < (oLodingParam.yPos + objLoadingForm.Height)))
                {
                    objLoadingForm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }
                else
                {
                    objLoadingForm.Left = oLodingParam.xPos;
                    objLoadingForm.Top = oLodingParam.yPos;
                }

                objLoadingForm.Activate();
                objLoadingForm.Show();

            }
            else
            {
                objLoadingForm.Dispatcher.Invoke(new StartLoader(startLoader), pLoadingParams);
            }
        }
        #endregion

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

            SetUserContentandData("Profile", oProfile);
        }

        public async void btnUser_Click(object sender, RoutedEventArgs e)
        {
            UserDisplayViewModel oViewModel = null;
            startProcess("Loading...");

            await Task.Run(() =>
            {
                System.Threading.Thread.Sleep(10);

                oViewModel = new UserDisplayViewModel(this);
            });

            stopProcess();
            EnabledGrid();

            UserDisplay oUserDisplay = new UserDisplay(this, oViewModel);

            SetUserContentandData("User", oUserDisplay);
        }

        public async void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerDisplayViewModel oViewModel = null;
            startProcess("Loading...");

            await Task.Run(() =>
            {
                System.Threading.Thread.Sleep(10);

                oViewModel = new CustomerDisplayViewModel(this);
            });

            stopProcess();
            EnabledGrid();

            CustomerDisplay oCustomer = new CustomerDisplay(this, oViewModel);
         
            SetUserContentandData("Customer", oCustomer);
        }

        public async void btnFuelHistory_Click(object sender, RoutedEventArgs e)
        {
            FuelHistoryViewModel oViewModel = null;
            startProcess("Loading...");

            await Task.Run(() =>
            {
                System.Threading.Thread.Sleep(10);

                oViewModel = new FuelHistoryViewModel(this);
            });

            stopProcess();
            EnabledGrid();

            FuelHistory oHistory = new FuelHistory(this, oViewModel);         

            SetUserContentandData("FuelHistory", oHistory);
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            SharedData.LoggedUser = null;

            HomeScreen oHomeScreen = new HomeScreen();

            SetUserContentandData("Home", oHomeScreen);

            OpenLoginWindows();
            SetUserContentandData("LogOut", null);
        }

        private void MetroWindow_Activated(object sender, EventArgs e)
        {
            if (oLoginWindow != null)
            {
                oLoginWindow.Focus();
                oLoginWindow.txtUseName.Focus();
            }
        }

        private async void btnCreditHistory_Click(object sender, RoutedEventArgs e)
        {
            CreditHistoryViewModel oViewModel = null;
            startProcess("Loading...");

            await Task.Run(() =>
            {
                System.Threading.Thread.Sleep(10);

                oViewModel = new CreditHistoryViewModel(this);
            });

            stopProcess();
            EnabledGrid();

            CreditHistory oHistory = new CreditHistory(this, oViewModel);

            SetUserContentandData("CreditHistory", oHistory);
        }  
        
        private void SetUserContentandData(string pType, object pData)
        {
            switch(pType)
            {
                case "CreditHistory":
                    btnProfile.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnLogOut.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnUser.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnCustomer.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnFuelHistory.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnCreditHistory.Style = (System.Windows.Style)this.FindResource("SelectedButtonStyle");
                    break;
                case "FuelHistory":
                    btnProfile.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnLogOut.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnUser.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnCustomer.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnFuelHistory.Style = (System.Windows.Style)this.FindResource("SelectedButtonStyle");
                    btnCreditHistory.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    break;
                case "LogOut":
                    btnProfile.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnLogOut.Style = (System.Windows.Style)this.FindResource("SelectedButtonStyle");
                    btnUser.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnCustomer.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnFuelHistory.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnCreditHistory.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    break;
                case "Customer":
                    btnProfile.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnLogOut.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnUser.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnCustomer.Style = (System.Windows.Style)this.FindResource("SelectedButtonStyle");
                    btnFuelHistory.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnCreditHistory.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    break;
                case "User":
                    btnProfile.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnLogOut.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnUser.Style = (System.Windows.Style)this.FindResource("SelectedButtonStyle");
                    btnCustomer.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnFuelHistory.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnCreditHistory.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    break;
                case "Profile":
                    btnProfile.Style = (System.Windows.Style)this.FindResource("SelectedButtonStyle");
                    btnLogOut.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnUser.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnCustomer.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnFuelHistory.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnCreditHistory.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    break;
                case "Home":
                    btnProfile.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnLogOut.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnUser.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnCustomer.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnFuelHistory.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    btnCreditHistory.Style = (System.Windows.Style)this.FindResource("ButtonStyle");
                    break;

            }

            if (pData != null)
            {
                if (mainModel.ContentWindow != null)
                {
                    if (mainModel.ContentWindow.DataContext.GetType() == typeof(CustomerDisplayViewModel))
                   {
                       CustomerDisplayViewModel oViewModel = (CustomerDisplayViewModel) mainModel.ContentWindow.DataContext;
                       oViewModel.DeregisterFingerPrinttouchEvent();                       
                   }

                    mainModel.ContentWindow.DataContext = null;
                    mainModel.ContentWindow = null;
                }
                mainModel.ContentWindow = (UserControl)pData;
            }
        }
    }
}
