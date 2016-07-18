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
    public class AddCreditViewModel : INotifyPropertyChanged
    {
        #region "Declaration"
        private Customer _SelectedCustomer;
        private decimal _Credit;
        private List<PaymentType> _PaymentTypeList;
        private MainWindow oMainWindow;
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

        public Decimal Credit
        {
            get
            {
                return _Credit;
            }
            set
            {
                _Credit = value;
                OnPropertyChanged("Credit");
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
        public AddCreditViewModel(Window pOwnerWindow)
        {
            _PaymentTypeList = CustomerManager.GetAllPaymentTypes();

            oMainWindow = (MainWindow)pOwnerWindow;
        }

        public bool AddCredit()
        {
            if (Credit <= 0)
            {
                MessageManager.ShowErrorMessage("Please enter valid amount", oMainWindow);
            }            
            else
            {
                bool result = CustomerManager.IncreaseCredit(_SelectedCustomer.Id, Credit,(int)_SelectedCustomer.PaymentType);
                if (result == false)
                {
                    MessageManager.ShowErrorMessage("Error while adding the credit, Please try again", oMainWindow);
                }
                else
                    return true;
            }
            return false;
        }
        #endregion
    }
}
