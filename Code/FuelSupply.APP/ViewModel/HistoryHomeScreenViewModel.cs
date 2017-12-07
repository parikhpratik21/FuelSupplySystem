using FuelSupply.APP.ExportEntity;
using FuelSupply.BAL.Manager;
using FuelSupply.BAL.Manager.Common;
using FuelSupply.DAL.Entity.CreditEntity;
using FuelSupply.DAL.Entity.CustomerEntity;
using FuelSupply.DAL.Entity.Fuel;
using FuelSupply.DAL.Entity.UserEntity;
using FuelSupply.DAL.Provider.Common;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FuelSupply.APP.ViewModel
{
    public class HistoryHomeScreenViewModel : INotifyPropertyChanged
    {
        #region "Declaration"
        private MainWindow oMainWindow;
        private CreditHistoryViewModel oCreditViewModel;
        private FuelHistoryViewModel oFuelViewModel;
        private CombineHistoryViewModel oCombineViewModel;

        private UserControl _fuelHistoryContentWindow;
        private UserControl _creditHistoryContentWindow;
        private UserControl _combineHistoryContentWindow;
        #endregion

        #region "Property"

        public UserControl FuelHistoryContentWindow
        {
            get { return _fuelHistoryContentWindow; }
            set
            {
                _fuelHistoryContentWindow = value;
                OnPropertyChanged("FuelHistoryContentWindow");
            }
        }

        public UserControl CreditHistoryContentWindow
        {
            get { return _creditHistoryContentWindow; }
            set
            {
                _creditHistoryContentWindow = value;
                OnPropertyChanged("CreditHistoryContentWindow");
            }
        }

        public UserControl CombineHistoryContentWindow
        {
            get { return _combineHistoryContentWindow; }
            set
            {
                _combineHistoryContentWindow = value;
                OnPropertyChanged("CombineHistoryContentWindow");
            }
        }
        public CreditHistoryViewModel CreditViewModel
        {
            get
            {
                return oCreditViewModel;
            }
            set
            {
                oCreditViewModel = value;
                OnPropertyChanged("CreditViewModel");
            }
        }

        public FuelHistoryViewModel FuelViewModel
        {
            get
            {
                return oFuelViewModel;
            }
            set
            {
                oFuelViewModel = value;
                OnPropertyChanged("FuelViewModel");
            }
        }

        public CombineHistoryViewModel CombineViewModel
        {
            get
            {
                return oCombineViewModel;
            }
            set
            {
                oCombineViewModel = value;
                OnPropertyChanged("CombineViewModel");
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

        public HistoryHomeScreenViewModel(Window pOwnerWindow)
        {          
            oMainWindow = (MainWindow)pOwnerWindow;                      
        }

        #region "Methods"
       
        #endregion
    }
}
