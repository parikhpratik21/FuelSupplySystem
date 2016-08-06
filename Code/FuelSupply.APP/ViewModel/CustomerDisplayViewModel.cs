using FuelSupply.APP.Helper;
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
using System.Windows.Input;

namespace FuelSupply.APP.ViewModel
{
    public class CustomerDisplayViewModel : INotifyPropertyChanged
    {
        #region "Declaration"
        private List<Customer> _OriginalCustomerList;
        private List<Customer> _DisplayCustomerList;
        private Customer _selectedCustomer;
        private MainWindow oMainWindow;
        private ICommand _searchCommand;
        private string _SearchTerms;
        #endregion
          
        public CustomerDisplayViewModel(Window pOwnerWindow)
        {
            _OriginalCustomerList = CustomerManager.GetAllCustomers();
            _DisplayCustomerList = _OriginalCustomerList;
            oMainWindow = (MainWindow)pOwnerWindow;

            SearchCommand = new RelayCommand(SearchText);
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
        #endregion
    }
}
