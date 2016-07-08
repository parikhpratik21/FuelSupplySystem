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
    public class CustomerDisplayViewModel : INotifyPropertyChanged
    {
        #region "Declaration"
        private List<Customer> _CustomerList;
        private Customer _selectedCustomer;
        #endregion
          
        public CustomerDisplayViewModel(Window pOwnerWindow)
        {
            _CustomerList = CustomerManager.GetAllCustomers();
        }

        #region "Property"
        public List<Customer> CustomerList
        {
            get { 
                return _CustomerList; 
            }
            set
            {
                _CustomerList = value;
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
