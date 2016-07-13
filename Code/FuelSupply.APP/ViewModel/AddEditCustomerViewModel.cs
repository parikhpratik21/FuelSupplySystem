using FuelSupply.BAL.Manager;
using FuelSupply.BAL.Manager.Common;
using FuelSupply.DAL.Entity.Comman;
using FuelSupply.DAL.Entity.CustomerEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FuelSupply.APP.ViewModel
{
    public class AddEditCustomerViewModel : INotifyPropertyChanged
    {
        #region "Declaration"
        private Customer _SelectedCustomer;
        private string _Title;
        private List<PaymentType> _PaymentTypeList;
        private List<CustomerType> _CustomerTypeList;
        private List<Customer> _KeyCustomerList;
        private MainWindow oMainWindow;
        #endregion

        #region "Property"
        public Customer SelectedCustomer
        {
            get {
                return _SelectedCustomer; 
            }
            set{
                _SelectedCustomer = value;
                OnPropertyChanged("SelectedCustomer");
            }
        }

        public List<CustomerType> CustomerTypeList
        {
            get
            {
                return _CustomerTypeList;
            }
            set
            {
                _CustomerTypeList = value;
                OnPropertyChanged("CustomerTypeList");
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
                    if (_SelectedCustomer.KeyCustomerId == null || _SelectedCustomer.KeyCustomerId <= 0)
                    {
                        if(_SelectedCustomer.CustomerType == null)
                            return true;
                        else if(_SelectedCustomer.CustomerType == (int)Constants.eCustomerType.Driver)
                        {
                            return true;
                        }
                        else
                            return false;
                    }                        
                    else
                    {
                        if (_KeyCustomerList.Where(x => x.Id == _SelectedCustomer.KeyCustomerId).Count() > 0)
                        {
                             if(_SelectedCustomer.CustomerType == null)
                                return true;
                            else if(_SelectedCustomer.CustomerType == (int)Constants.eCustomerType.Driver)
                            {
                                return true;
                            }
                            else
                                return false;
                        }                            
                        else
                            return false;
                    }
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

        public List<PaymentType> PaymentTypeList
        {
            get
            {
                return _PaymentTypeList;
            }
            set
            {
                _PaymentTypeList = value;
                OnPropertyChanged("PaymentTypeList");
            }
        }

        public string Title
        {
            get
            { return _Title; }
            set
            {
                _Title = value;
                OnPropertyChanged("Title");
            }
        }
        #endregion

        #region "Methods"
        public AddEditCustomerViewModel(Window pOwnerWindow)
        {
            _CustomerTypeList = CustomerManager.GetAllCustomerTypes();
            _PaymentTypeList = CustomerManager.GetAllPaymentTypes();
            _KeyCustomerList = CustomerManager.GetAllKeyCustomer();

            oMainWindow = (MainWindow)pOwnerWindow;
        }

        public bool AddEditCustomer()
        {
            bool result = ValidateCustomer();
            if (result == false)
                return false;

            if (_SelectedCustomer.Id <= 0)
                return AddCustomer();
            else
                return UpdateCustomer();
        }
        private bool ValidateCustomer()
        {
            if (_SelectedCustomer.Code == null || _SelectedCustomer.Code.Length <= 0)
                MessageManager.ShowErrorMessage("Please select valid customer code", oMainWindow);
            else if (_SelectedCustomer.Name == null || _SelectedCustomer.Name.Length <= 0)
                MessageManager.ShowErrorMessage("Please select valid customer name", oMainWindow);
            else if (_SelectedCustomer.CustomerType == null || _SelectedCustomer.CustomerType == 0)
                MessageManager.ShowErrorMessage("Please select valid customer type", oMainWindow);
            else if (_SelectedCustomer.CustomerType == (int)Constants.eCustomerType.Driver && (_SelectedCustomer.KeyCustomerId == null || _SelectedCustomer.KeyCustomerId <= 0))
                MessageManager.ShowErrorMessage("Please select valid key customer", oMainWindow);
            else if (_SelectedCustomer.PaymentType == null || _SelectedCustomer.PaymentType <= 0)
                MessageManager.ShowErrorMessage("Please select valid key payment type", oMainWindow);
            else if (_SelectedCustomer.PaymentLimit == null || _SelectedCustomer.PaymentLimit <= 0)
                MessageManager.ShowErrorMessage("Please select valid key payment limit", oMainWindow);
            else
                return true;

            return false;
        }
        private bool AddCustomer()
        {
            _SelectedCustomer.AvailablePay = _SelectedCustomer.PaymentLimit;
            bool result = CustomerManager.AddCustomer(_SelectedCustomer);
            if (result == false)
            {
                MessageManager.ShowErrorMessage("Error while adding customer, Please try again.", oMainWindow);
                return false;
            }
            else
                return true;
        }

        private bool UpdateCustomer()
        {
            bool result = CustomerManager.UpdateCustomer(_SelectedCustomer);
            if (result == false)
            {
                MessageManager.ShowErrorMessage("Error while updating customer, Please try again.", oMainWindow);
                return false;
            }
            else
                return true;
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
    }
}
