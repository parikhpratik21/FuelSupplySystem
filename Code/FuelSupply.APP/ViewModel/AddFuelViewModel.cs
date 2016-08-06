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

        public bool AddFuel()
        {
            if (FuelAmount <= 0)
            {
                MessageManager.ShowErrorMessage("Please enter valid fuel amount", oMainWindow);
            }
            else if (FuelTaken <= 0)
            {
                MessageManager.ShowErrorMessage("Please enter valid fuel taken", oMainWindow);
            }
            else
            {
                //First check whether we can deduct money or not
                bool bCheckAvailibility = CustomerManager.CheckDeductionAvailibility(_SelectedCustomer.Id, FuelAmount);
                if (bCheckAvailibility == false)
                {
                    MessageManager.ShowErrorMessage("Available amount is low can not add fuel, Please add credit", oMainWindow);
                }
                else
                {
                    bool bDeductAmount = CustomerManager.DeductAmount(_SelectedCustomer.Id, FuelAmount);
                    if (bDeductAmount == false)
                    {
                        MessageManager.ShowErrorMessage("Error while adding the fuel, Please try again", oMainWindow);
                    }
                    else
                    {
                        FuelHistory oFuelHistory = new FuelHistory();
                        oFuelHistory.CustomerId = _SelectedCustomer.Id;
                        oFuelHistory.CustomerName = _SelectedCustomer.Name;
                        oFuelHistory.FuelAmount = FuelAmount;
                        oFuelHistory.FuelStationId = SharedData.CurrentFuelStation.Id;
                        oFuelHistory.FuelType = FuelType;
                        oFuelHistory.FuelVolume = FuelTaken;
                        oFuelHistory.KeyCustomerId = _SelectedCustomer.KeyCustomerId;
                        oFuelHistory.Id = 0;
                        if(_SelectedCustomer.KeyCustomerId != null && _SelectedCustomer.KeyCustomerId >0)
                        {
                            oFuelHistory.KeyCustomerName = _SelectedCustomer.Customer2.Name;
                        }
                        oFuelHistory.Time = DateTime.Now;
                        oFuelHistory.UserId = SharedData.LoggedUser.Id;
                        oFuelHistory.UserName = SharedData.LoggedUser.Name;

                        bool result = FuelManager.AddFuelHistory(oFuelHistory);
                        if (result == false)
                        {
                            MessageManager.ShowErrorMessage("Error while adding the fuel, Please try again", oMainWindow);
                        }
                        else
                            return true;
                    }
                }
            }
            return false;
        }
        #endregion
    }
}
