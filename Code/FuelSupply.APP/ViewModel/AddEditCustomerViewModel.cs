using FuelSupply.BAL.Manager;
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
        public AddEditCustomerViewModel(Window pOwnerWindow)
        {
            _CustomerTypeList = CustomerManager.GetAllCustomerTypes();
            _PaymentTypeList = CustomerManager.GetAllPaymentTypes();
        }

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
