using FuelSupply.BAL.Manager;
using FuelSupply.BAL.Manager.Common;
using FuelSupply.DAL.Entity.CustomerEntity;
using FuelSupply.DAL.Entity.FuelEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FuelSupply.APP.ViewModel
{
    public class AddFuelViewModel : INotifyPropertyChanged
    {
        #region "Declaration"
        private Customer _SelectedCustomer;
        private List<FuelType> _FuelTypeList;
        private Decimal _FuelTaken;
        private Decimal _FuelAmount;
        private List<Customer> _KeyCustomerList;
        private MainWindow oMainWindow;
        private int _FuelType;

        private delegate bool FingerPrintScan(string pFingerPrint);
        #endregion

        #region "Property"
        public Customer SelectedCustomer
        {
            get
            {
                return _SelectedCustomer;
            }
            set
            {
                _SelectedCustomer = value;
                OnPropertyChanged("SelectedCustomer");
            }
        }

        public decimal? AvailableAmount
        {
            get
            {
                if(_SelectedCustomer.CustomerType == (int)FuelSupply.DAL.Entity.Comman.Constants.eCustomerType.KeyCustomer)
                {
                    return SelectedCustomer.AvailablePay;
                }
                else
                {
                    if (SelectedCustomer.Customer2 != null)
                        return SelectedCustomer.Customer2.AvailablePay;
                    else
                        return 0;
                }
            }
        }
        public List<FuelType> FuelTypeList
        {
            get
            {
                return _FuelTypeList;
            }
            set
            {
                _FuelTypeList = value;
                OnPropertyChanged("FuelTypeList");
            }
        }

        public Decimal FuelTaken
        {
            get
            {
                return _FuelTaken;
            }
            set
            {
                _FuelTaken = value;
                OnPropertyChanged("FuelTaken");
            }
        }

        public Decimal FuelAmount
        {
            get
            {
                return _FuelAmount;
            }
            set
            {
                _FuelAmount = value;
                OnPropertyChanged("FuelAmount");
            }
        }

        public List<Customer> KeyCustomerList
        {
            get
            {
                return _KeyCustomerList;
            }
            set
            {
                _KeyCustomerList = value;
                OnPropertyChanged("KeyCustomerList");
            }
        }

        public int FuelType
        {
            get
            {
                return _FuelType;
            }
            set
            {
                _FuelType = value;
                OnPropertyChanged("FuelType");
            }
        }

        public bool IsKeyCustomerListEnable
        {
            get
            {
                if (_KeyCustomerList == null || _KeyCustomerList.Count == 0)
                    return false;
                else
                {
                    if (_KeyCustomerList.Where(x => x.Id == _SelectedCustomer.KeyCustomerId).Count() > 0)
                        return true;
                    else
                        return false;
                }
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
        public AddFuelViewModel(Window pOwnerWindow)
        {
            _FuelTypeList = FuelManager.GetAllFuelTypeList();
            _KeyCustomerList = CustomerManager.GetAllKeyCustomer();

            _FuelType = _FuelTypeList.FirstOrDefault().Id;

            oMainWindow = (MainWindow)pOwnerWindow;            
        }

        public bool AddFuel(ref string pErrorString)
        {
            if (FuelAmount <= 0)
            {
                pErrorString = "Please enter valid fuel amount.";                
            }
            else if (FuelTaken <= 0)
            {
                pErrorString = "Please enter valid fuel taken.";                
            }
            else
            {
                //First check whether we can deduct money or not
                bool bCheckAvailibility = CustomerManager.CheckDeductionAvailibility(_SelectedCustomer.Id, FuelAmount);
                if (bCheckAvailibility == false)
                {
                    pErrorString = "Available amount is low, Fuel can not be added. Please add credit.";                        
                }
                else
                {                                       
                    CustomerDisplayViewModel.ofisFingerPrintSensor.SetFPEngineVersion(CustomerDisplayViewModel.FingerPrintSensorVersion);

                    CustomerDisplayViewModel.ofisFingerPrintSensor.InitSensor();

                    if (CustomerDisplayViewModel.ofisFingerPrintSensor.GetVerTemplate() == true)
                    {
                        if (Properties.Settings.Default.IsBeepEnable == true)
                            CustomerDisplayViewModel.ofisFingerPrintSensor.PlayBeep(true);

                        CustomerDisplayViewModel.ofisFingerPrintSensor.PlayGreenLight(true);

                        string CurrentFingerPrint = CustomerDisplayViewModel.ofisFingerPrintSensor.VerifyTemplate;

                        if (Properties.Settings.Default.IsBeepEnable == true)
                            CustomerDisplayViewModel.ofisFingerPrintSensor.PlayBeep(false);
                        CustomerDisplayViewModel.ofisFingerPrintSensor.PlayGreenLight(false);

                        bool matchResult = MatchFingerPrint(CurrentFingerPrint); 
                        if(matchResult == false)
                        {
                            pErrorString = "Fingerprint doesn't match, Please try again.";                                                            
                            return false;
                        }

                        int iCustomerId = 0;
                        if (_SelectedCustomer.CustomerType == (int)FuelSupply.DAL.Entity.Comman.Constants.eCustomerType.KeyCustomer)
                            iCustomerId = _SelectedCustomer.Id;
                        else
                        {
                            if (_SelectedCustomer.Customer2 != null)
                                iCustomerId = _SelectedCustomer.Customer2.Id;
                        }
                        bool bDeductAmount = CustomerManager.DeductAmount(iCustomerId, FuelAmount);
                        if (bDeductAmount == false)
                        {
                            pErrorString = "Error while adding the fuel, Please try again.";  
                            return false;
                        }

                        FuelHistory oFuelHistory = new FuelHistory();
                        oFuelHistory.CustomerId = _SelectedCustomer.Id;
                        oFuelHistory.CustomerName = _SelectedCustomer.Name;
                        oFuelHistory.FuelAmount = FuelAmount;
                        oFuelHistory.FuelStationId = SharedData.CurrentFuelStation.Id;
                        oFuelHistory.FuelType = FuelType;
                        oFuelHistory.FuelVolume = FuelTaken;
                        oFuelHistory.KeyCustomerId = _SelectedCustomer.KeyCustomerId;
                        oFuelHistory.Id = 0;
                        if (_SelectedCustomer.KeyCustomerId != null && _SelectedCustomer.KeyCustomerId > 0)
                        {
                            oFuelHistory.KeyCustomerName = _SelectedCustomer.Customer2.Name;
                        }
                        oFuelHistory.Time = DateTime.Now;
                        oFuelHistory.UserId = SharedData.LoggedUser.Id;
                        oFuelHistory.UserName = SharedData.LoggedUser.Name;

                        bool result = FuelManager.AddFuelHistory(oFuelHistory);
                        if (result == false)
                        {
                            pErrorString = "Error while adding the fuel, Please try again.";                                
                        }
                        else
                            return true;                        
                    }
                    else
                    {
                        CustomerDisplayViewModel.ofisFingerPrintSensor.PlayRedLight(true);

                        CustomerDisplayViewModel.ofisFingerPrintSensor.PlayRedLight(false);
                    }                    
                }
            }
            return false;
        }       

        private bool MatchFingerPrint(string pFingerPrint)
        {
            if (oMainWindow.Dispatcher.CheckAccess())
            {
                if (_SelectedCustomer.CustomerFingerPrints != null && _SelectedCustomer.CustomerFingerPrints.Count > 0)
                {
                    foreach (CustomerFingerPrint oFingerPrint in _SelectedCustomer.CustomerFingerPrints)
                    {
                        if (CustomerDisplayViewModel.ofisFingerPrintSensor.MatchFinger(oFingerPrint.FingerPrint, pFingerPrint) == true)
                        {
                            return true;
                        }
                    }
                }                               
            }
            else
                oMainWindow.Dispatcher.Invoke(new FingerPrintScan(MatchFingerPrint), new object[] { pFingerPrint });

            return false;
        }

        #endregion
    }
}
