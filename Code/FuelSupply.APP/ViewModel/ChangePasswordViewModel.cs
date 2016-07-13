using FuelSupply.BAL.Manager;
using FuelSupply.BAL.Manager.Common;
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
        MainWindow oMainWindow;      
        #endregion

        #region "Property"
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
                bool result = UserManager.ChangePassword(_SelectedUser.Id,pNewPassword);
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
