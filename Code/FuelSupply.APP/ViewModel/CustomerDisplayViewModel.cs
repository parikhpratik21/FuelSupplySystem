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
          private List<Customer> _CustomerList;
          public CustomerDisplayViewModel(Window pOwnerWindow)
        {
            _CustomerList = CustomerManager.GetAllCustomer();
        }

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
