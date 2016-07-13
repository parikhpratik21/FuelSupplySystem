using FuelSupply.BAL.Manager;
using FuelSupply.DAL.Entity.CustomerEntity;
using FuelSupply.DAL.Entity.FuelEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.APP.ViewModel
{
    public class AddCreditViewModel : INotifyPropertyChanged
    {
        #region "Declaration"
        private Customer _SelectedCustomer;
        private decimal _Credit;
        private List<PaymentType> _PaymentTypeList;
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
        public AddCreditViewModel()
        {
            _PaymentTypeList = CustomerManager.GetAllPaymentTypes();
        }
        #endregion
    }
}
