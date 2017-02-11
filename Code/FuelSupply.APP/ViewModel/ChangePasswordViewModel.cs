using FuelSupply.BAL.Manager;
using FuelSupply.BAL.Manager.Common;
using FuelSupply.DAL.Entity.CustomerEntity;
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
    public class ChangePasswordViewModel : INotifyPropertyChanged
    {
         #region "Declaration"
        private User _SelectedUser;
        private Customer _SelectedCustomer;
        MainWindow oMainWindow;      
        #endregion

        #region "Property"

        public string Code
        {
            get
            {
                if(SelectedUser != null)
                {
                    return SelectedUser.Code;
                }
                else
                {
                    return SelectedCustomer.Code;
                }
            }
        }

        public string Name
        {
            get
            {
                if (SelectedUser != null)
                {
                    return SelectedUser.Name;
                }
                else
                {
                    return SelectedCustomer.Name;
                }
            }
        }

        public User SelectedUser
        {
            get
            {
                return _SelectedUser;
            }
            set
            {
                _SelectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }

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
        public ChangePasswordViewModel(Window pOwner)
        {
            oMainWindow = (MainWindow)pOwner;
        }

        public bool ChangePassword(string pNewPassword, string pConfirmPassword)
        {
            if(pNewPassword.Length <6)
            {
                MessageManager.ShowErrorMessage("Password must be minimum 6 character long",oMainWindow);
            }
            else if(pNewPassword != pConfirmPassword)
            {
                MessageManager.ShowErrorMessage("New password and confirm password must be same",oMainWindow);
            }
            else
            {
                bool result = true;
                if (_SelectedUser != null)
                {
                    UserManager.ChangePassword(_SelectedUser.Id, pNewPassword);
                }
                else
                {
                    CustomerManager.ChangePassword(_SelectedCustomer.Id, pNewPassword);
                }
                if (result == false)
                {
                    MessageManager.ShowErrorMessage("Error while changing the password, Please try again", oMainWindow);
                }
                else
                    return true;
            }
            return false;
        }
        #endregion
    }
}
