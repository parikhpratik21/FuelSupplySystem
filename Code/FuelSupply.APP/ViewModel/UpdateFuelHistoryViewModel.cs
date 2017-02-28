using FuelSupply.BAL.Manager;
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
    public class UpdateFuelHistoryViewModel: INotifyPropertyChanged
    {
        #region "Declaration"
        private FuelHistory _SelectedFuelHistory;        
        private Decimal _ActualFuel;
        private Decimal _ActualAmount;        
        private MainWindow oMainWindow;
        public static string CreditConfirmationMessage = "Actual amount is less then paid amount." + Environment.NewLine + "Do you want to credit difference back to the customer account?";     
        #endregion

        #region "Property"
        public FuelHistory SelectedFuelHistory
        {
            get
            {
                return _SelectedFuelHistory;
            }
            set
            {
                _SelectedFuelHistory = value;
                if (_SelectedFuelHistory != null && _SelectedFuelHistory.ActualFuelVolume != null && _SelectedFuelHistory.ActualFuelAmount != null)
                {
                    ActualFuel = _SelectedFuelHistory.ActualFuelVolume.Value;
                    ActualAmount = _SelectedFuelHistory.ActualFuelAmount.Value;
                }
                OnPropertyChanged("SelectedFuelHistory");
            }
        }

        public Decimal ActualFuel
        {
            get
            {
                return _ActualFuel;
            }
            set
            {
                _ActualFuel = value;
                OnPropertyChanged("ActualFuel");
            }
        }

        public Decimal ActualAmount
        {
             get
            {
                return _ActualAmount;
            }
            set
            {
                _ActualAmount = value;
                OnPropertyChanged("ActualAmount");
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
        public UpdateFuelHistoryViewModel(Window pOwnerWindow)
        {           
            oMainWindow = (MainWindow)pOwnerWindow;
        }

        public bool UpdateFuelHistory(ref string pErrorString)
        {
            if(SelectedFuelHistory == null)
            {
                pErrorString = "Please select FuelHistory first.";
                return false;
            }
            else if (ActualAmount <= 0)
            {
                pErrorString = "Please enter fuel amount.";
                return false;
            }
            else if (ActualFuel <= 0)
            {
                pErrorString = "Please enter fuel volume.";
                return false;
            }
            else
            {
                if (ActualAmount != SelectedFuelHistory.ActualFuelAmount && ActualAmount < SelectedFuelHistory.FuelAmount)
                {
                    //ask for credit credit back to customer account
                    pErrorString = CreditConfirmationMessage;
                    return true;
                }
                else
                {
                    var result = FuelManager.UpdateActualFuelDetail(SelectedFuelHistory.Id, ActualFuel,ActualAmount);
                    if(result == false)
                    {
                        pErrorString = "Error while updating fuel history, Please try again.";
                    }
                    return result;
                }
            }            
        }

        public bool UpdateActualAmount(ref string pErrorString)
        {
            var result = FuelManager.UpdateActualFuelDetail(SelectedFuelHistory.Id, ActualFuel, ActualAmount);
            if (result == false)
            {
                pErrorString = "Error while updating fuel history, Please try again.";
            }
            return result;
        }

        public bool DebitCreditandUpdateActualAmount(ref string pErrorString)
        {
            //debit customer limit
            decimal? difference = SelectedFuelHistory.FuelAmount - ActualAmount;
            if(difference != null && difference > 0)
            {
                if (SelectedFuelHistory.CustomerId != null && difference != null)
                {
                    var deductResult = CustomerManager.IncreaseAmount(SelectedFuelHistory.CustomerId.Value, difference.Value);

                    if (deductResult == true)
                    {
                        var result = FuelManager.UpdateActualFuelDetail(SelectedFuelHistory.Id, ActualFuel, ActualAmount);
                        if (result == false)
                        {
                            pErrorString = "Error while updating fuel history, Please try again.";
                        }
                        return result;
                    }
                    else
                    {
                        pErrorString = "Error while updating fuel history, Please try again.";
                        return false;
                    }
                }
                else
                {
                    pErrorString = "Error while updating fuel history, Please try again.";
                    return false;
                }
            }   
            else
            {
                pErrorString = "Actual amount is less then fuel amount, Please try again.";
                return false;
            }
        }

        #endregion
    }
}
