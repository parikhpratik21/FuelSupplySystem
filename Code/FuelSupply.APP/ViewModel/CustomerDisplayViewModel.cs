using FuelSupply.APP.Helper;
using FuelSupply.BAL.Manager;
using FuelSupply.BAL.Manager.Common;
using FuelSupply.DAL.Entity.CustomerEntity;
using Ofis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FuelSupply.APP.ViewModel
{
    public class CustomerDisplayViewModel : INotifyPropertyChanged, IDisposable
    {
        #region "Declaration"
        private List<Customer> _OriginalCustomerList;
        private List<Customer> _DisplayCustomerList;
        private Customer _selectedCustomer;
        private MainWindow oMainWindow;
        private ICommand _searchCommand;
        private string _SearchTerms;

        public delegate void OpenAddFuelForm();
        public event OpenAddFuelForm openAddFuelForSelectedCustomer;               

        public static OfisMain ofisFingerPrintSensor;
        public static int FingerPrintSensorVersion = 10;

        public List<CustomerFingerPrint> _AllCustomerFingerPrintList;

        private delegate void FingerPrintScan(string pFingerPrint);
        public delegate void SetupFingerPrintSensor();
        #endregion
          
        public CustomerDisplayViewModel(Window pOwnerWindow)
        {
            _OriginalCustomerList = CustomerManager.GetAllCustomers();
            _DisplayCustomerList = _OriginalCustomerList;
            oMainWindow = (MainWindow)pOwnerWindow;

            SearchCommand = new RelayCommand(SearchText);

            _AllCustomerFingerPrintList = CustomerManager.GetAllCustomerFingerPrint();

            if(ofisFingerPrintSensor == null)
            {
                ofisFingerPrintSensor = new OfisMain();                
            }

            SetUpFingerPrintDevice();
        }

        public void DeregisterFingerPrinttouchEvent()
        {
            ofisFingerPrintSensor.OnFingerTouching -= ofisFingerPrintSensor_OnFingerTouching;
        }

        public void RegisterFingerPrinttouchEvent()
        {
            ofisFingerPrintSensor.SetFPEngineVersion(CustomerDisplayViewModel.FingerPrintSensorVersion);

            ofisFingerPrintSensor.OnFingerTouching += ofisFingerPrintSensor_OnFingerTouching;
        }

        private void SetUpFingerPrintDevice()
        {
            if (oMainWindow.Dispatcher.CheckAccess())
            {
                ofisFingerPrintSensor.IsAutoRegister = true;

                ofisFingerPrintSensor.SetFPEngineVersion(FingerPrintSensorVersion);

                int InitSensorResult = ofisFingerPrintSensor.InitSensor();

                if (InitSensorResult == 1)
                {
                    MessageManager.ShowErrorMessage("Driver not install for fingerprint sensor, Please install driver.", oMainWindow);
                }
                else if (InitSensorResult == 2)
                {
                    MessageManager.ShowErrorMessage("Fingerprint sensor not connected, Please connect fingerprint sensor", oMainWindow);
                }

                ofisFingerPrintSensor.OnFingerTouching += ofisFingerPrintSensor_OnFingerTouching;
            }
            else
                oMainWindow.Dispatcher.Invoke(new SetupFingerPrintSensor(SetUpFingerPrintDevice));   

           
        }

        private void ofisFingerPrintSensor_OnFingerTouching()
        {
            ofisFingerPrintSensor.OnFingerTouching -= ofisFingerPrintSensor_OnFingerTouching;

            if (ofisFingerPrintSensor.GetVerTemplate() == true)
            {
                if (Properties.Settings.Default.IsBeepEnable == true)
                    ofisFingerPrintSensor.PlayBeep(true);
                ofisFingerPrintSensor.PlayGreenLight(true);

                string sFingerPrint = ofisFingerPrintSensor.VerifyTemplate;

                if (Properties.Settings.Default.IsBeepEnable == true)
                    ofisFingerPrintSensor.PlayBeep(false);

                ofisFingerPrintSensor.PlayGreenLight(false);

                OnReceiveFingerPrint(sFingerPrint);               
            } 
            else
            {
                ofisFingerPrintSensor.PlayRedLight(true);

                ofisFingerPrintSensor.PlayRedLight(false);
            }

            ofisFingerPrintSensor.OnFingerTouching += ofisFingerPrintSensor_OnFingerTouching;
        }       

        #region "Property"
        public string LoggedUserName
        {
            get
            {
                if (SharedData.LoggedUser != null)
                {
                    return SharedData.LoggedUser.Name;
                }
                else
                    return string.Empty;
            }
        }
        public List<Customer> CustomerList
        {
            get {
                return _DisplayCustomerList; 
            }
            set
            {
                _DisplayCustomerList = value;
                OnPropertyChanged("CustomerList");
            }
        }

        public Customer SelectedCustomer
        {
            get
            {
                return _selectedCustomer;
            }
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged("SelectedCustomer");
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand;
            }
            set
            {
                _searchCommand = value;
                OnPropertyChanged("SearchCommand");
            }
        }

        public string SearchTerms
        {
            get
            {
                return _SearchTerms;
            }
            set
            {
                _SearchTerms = value;
                OnPropertyChanged("SearchTerms");
                if (value != null && value.Length == 0 && _OriginalCustomerList != null && CustomerList != null && CustomerList.Count != _OriginalCustomerList.Count)
                {
                    CustomerList = _OriginalCustomerList;
                    OnPropertyChanged("CustomerList");
                }
            }
        }

        public bool IsAdminUser
        {
            get
            {
                if (SharedData.LoggedUser != null && SharedData.LoggedUser.UserType != null && SharedData.LoggedUser.UserType == (int)SharedData.UserType.Admin)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        #endregion

        #region EventHandlers (1)
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        #endregion

        #region "Methods"
        public void DeleteCustomer()
        {
            bool result = MessageManager.ShowConfirmationMessage("Are you sure you want to delete selected customer?", oMainWindow);
            if (result == true)
            {
                bool deleteResult = CustomerManager.DeleteCustomer(_selectedCustomer.Id);
                if (deleteResult == false)
                {
                    MessageManager.ShowErrorMessage("Error while deleting customer, Please try again.", oMainWindow);
                }
                else
                {
                    _OriginalCustomerList = CustomerManager.GetAllCustomers();
                    CustomerList = _OriginalCustomerList;
                }
            }
        }

        private void SearchText()
        {
            string searchString = SearchTerms.ToLower().Trim();

            CustomerList = _OriginalCustomerList.Where(x => x.Name.ToLower().Contains(searchString) == true || (x.Customer2 != null && x.Customer2.Name.ToLower().Contains(searchString) == true)).ToList();
            OnPropertyChanged("CustomerList");
        }

        public void OnReceiveFingerPrint(string pFingerPrint)
        {
            if (oMainWindow.Dispatcher.CheckAccess())
            {
                foreach (CustomerFingerPrint oFingerPrint in _AllCustomerFingerPrintList)
                {
                    if (ofisFingerPrintSensor.MatchFinger(oFingerPrint.FingerPrint, pFingerPrint) == true)
                    {
                        Customer oSelectedCustomer = _OriginalCustomerList.Where(x => x.Id == oFingerPrint.CustomerID).FirstOrDefault();
                        if (oSelectedCustomer != null)
                        {
                            _selectedCustomer = oSelectedCustomer;
                            openAddFuelForSelectedCustomer();
                        }

                        return;
                    }
                }

                MessageManager.ShowErrorMessage("Fingerprint doesn't exist, Please register.", oMainWindow);
            }
            else
                oMainWindow.Dispatcher.Invoke(new FingerPrintScan(OnReceiveFingerPrint), new object[] { pFingerPrint });            
        }
        #endregion

        public void Dispose()
        {
            DeregisterFingerPrinttouchEvent();
        }
    }
}
