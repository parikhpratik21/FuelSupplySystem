using FuelSupply.BAL.Manager;
using FuelSupply.BAL.Manager.Common;
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
        private MainWindow oMainWindow;
        #endregion
          
        public CustomerDisplayViewModel(Window pOwnerWindow)
        {
            _CustomerList = CustomerManager.GetAllCustomers();
            oMainWindow = (MainWindow)pOwnerWindow;
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
                    CustomerList = CustomerManager.GetAllCustomers();
                }
            }
        }
        #endregion
    }
}
