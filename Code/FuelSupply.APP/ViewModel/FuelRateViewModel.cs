using FuelSupply.BAL.Manager;
using FuelSupply.BAL.Manager.Common;
using FuelSupply.DAL.Entity.CustomerEntity;
using FuelSupply.DAL.Entity.FuelEntity;
using FuelSupply.DAL.Entity.UserEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FuelSupply.APP.ViewModel
{
    public class FuelRateViewModel : INotifyPropertyChanged
    {
         #region "Declaration"
        private Window oMainWindow;      
        private List<FuelType> _FuelTypeList;
        private FuelType _SelectedFuelType;
        private decimal _FuelRate;
        #endregion

        #region "Property"
        public decimal FuelRate
        {
            get
            {
                return _FuelRate;
            }
            set
            {
                _FuelRate = value;
                OnPropertyChanged("FuelRate");
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

        public FuelType SelectedFuelType
        {
            get
            {
                return _SelectedFuelType;
            }
            set
            {
                _SelectedFuelType = value;
                if(_SelectedFuelType != null)
                {
                    FuelRate = (decimal)_SelectedFuelType.Rate;
                }
                OnPropertyChanged("SelectedFuelType");
            }
        }

        public Window MainWindow
        {
            get
            {
                return oMainWindow;
            }
            set
            {
                oMainWindow = value;
                OnPropertyChanged("MainWindow");
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
        public FuelRateViewModel(Window pOwner)
        {
            oMainWindow = pOwner;

            FuelTypeList = FuelManager.GetAllFuelTypeList();

            if (FuelTypeList != null && FuelTypeList.Any())
            {
                SelectedFuelType = FuelTypeList.FirstOrDefault();
            }
        }

        public bool SaveFuelTypeData()
        {
            if(SelectedFuelType == null )
            {
                MessageManager.ShowErrorMessage("Please select fuel type first.", oMainWindow);
            }   
            
            else if(FuelRate <= 0)
            {
                MessageManager.ShowErrorMessage("Please enter fuel rate.", oMainWindow);
            }
            else
            {
                bool result = FuelManager.UpdateFuelRate(SelectedFuelType.Id,FuelRate);
                return result;
            }
            return false;
        }
        #endregion
    }
}
