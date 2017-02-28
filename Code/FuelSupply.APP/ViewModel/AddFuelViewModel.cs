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
        private string _InvoiceNo;
        private int _FuelType;

        private delegate bool FingerPrintScan(string pFingerPrint);

        public const string FingerPrintNotMatchMessage = "Fingerprint doesn't match, Please try again.";
        #endregion

        #region "Property"
        public string InvoiceNo
        {
            get
            {
                return _InvoiceNo;
            }
            set
            {
                _InvoiceNo = value;
                OnPropertyChanged("InvoiceNo");
            }
        }

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
                if (_SelectedCustomer.CustomerType == (int)FuelSupply.DAL.Entity.Comman.Constants.eCustomerType.KeyCustomer)
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
                if (_FuelTaken != value)
                {
                    _FuelTaken = Math.Round(value, 3);
                    FuelAmount = Math.Round(CurrentFuelRate.Value * _FuelTaken, 3);
                    OnPropertyChanged("FuelTaken");
                }
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
                if (_FuelAmount != value)
                {
                    _FuelAmount = Math.Round(value, 3);
                    FuelTaken = Math.Round(_FuelAmount / CurrentFuelRate.Value, 3);
                    OnPropertyChanged("FuelAmount");
                }
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
                if(_FuelType > 0)
                {
                    FuelAmount = Math.Round(CurrentFuelRate.Value * _FuelTaken, 3);
                }
                OnPropertyChanged("FuelType");
                OnPropertyChanged("CurrentFuelRate");
            }
        }

        public decimal? CurrentFuelRate
        {
            get
            {
                if (_FuelTypeList != null)
                {
                    FuelType oType = _FuelTypeList.Where(data => data.Id == FuelType).FirstOrDefault();
                    if (oType != null)
                        return oType.Rate;
                    else
                        return 0;
                }
                else
                    return 0;
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
            else if(InvoiceNo == null || InvoiceNo == string.Empty || InvoiceNo.Trim().Length == 0)
            {
                pErrorString = "Please enter invoice no.";
            }
            else
            {
                int iCustomerId = 0;
                if (_SelectedCustomer.CustomerType == (int)FuelSupply.DAL.Entity.Comman.Constants.eCustomerType.KeyCustomer)
                    iCustomerId = _SelectedCustomer.Id;
                else
                {
                    if (_SelectedCustomer.Customer2 != null)
                        iCustomerId = _SelectedCustomer.Customer2.Id;
                }

                //First check whether we can deduct money or not
                bool bCheckAvailibility = CustomerManager.CheckDeductionAvailibility(iCustomerId, FuelAmount);
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
                        if (matchResult == false)
                        {
                            pErrorString = FingerPrintNotMatchMessage;                            
                            return false;
                        }


                        return SaveFuelDetail(iCustomerId, ref pErrorString);
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

        public bool AddFuelByPassword(ref string pErrorMessage)
        {
            int iCustomerId = 0;
            if (_SelectedCustomer.CustomerType == (int)FuelSupply.DAL.Entity.Comman.Constants.eCustomerType.KeyCustomer)
                iCustomerId = _SelectedCustomer.Id;
            else
            {
                if (_SelectedCustomer.Customer2 != null)
                    iCustomerId = _SelectedCustomer.Customer2.Id;
            }

            return SaveFuelDetail(iCustomerId, ref pErrorMessage);
        }

        private bool SaveFuelDetail(int pCustomerId, ref string pErrorString)
        {
            bool bDeductAmount = CustomerManager.DeductAmount(pCustomerId, FuelAmount);
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

            if (SharedData.CurrentShift != null)
            {
                oFuelHistory.ShiftId = SharedData.CurrentShift.ShiftId;
                oFuelHistory.ShiftName = SharedData.CurrentShift.ShiftName;
            }
            oFuelHistory.InvoiceNo = InvoiceNo;
            oFuelHistory.ActualFuelAmount = 0;
            oFuelHistory.ActualFuelVolume = 0;

            bool result = FuelManager.AddFuelHistory(oFuelHistory);
            if (result == false)
            {
                pErrorString = "Error while adding the fuel, Please try again.";
                return false;
            }
            else
                return true;
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
