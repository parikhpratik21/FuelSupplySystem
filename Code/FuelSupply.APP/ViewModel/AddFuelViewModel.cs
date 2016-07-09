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
    public class AddFuelViewModel : INotifyPropertyChanged
    {
        #region "Declaration"
        private Customer _SelectedCustomer;
        private List<FuelType> _FuelTypeList;
        private Decimal _FuelTaken;
        private Decimal _FuelAmount;
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
        public AddFuelViewModel()
        {
            _FuelTypeList = FuelManager.GetAllFuelTypeList();
        }
        #endregion
    }
}
