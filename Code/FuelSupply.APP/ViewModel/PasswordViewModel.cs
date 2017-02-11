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
    public class PasswordViewModel : INotifyPropertyChanged
    {
         #region "Declaration"
        private Window oMainWindow;      
        private string _Password;
        private string _CustomerPassword;
        #endregion

        #region "Property"
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
                OnPropertyChanged("Password");
            }
        }

        public string CustomerPassword
        {
            get
            {
                return _CustomerPassword;
            }
            set
            {
                _CustomerPassword = value;
                OnPropertyChanged("CustomerPassword");
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
        public PasswordViewModel(Window pOwner)
        {
            oMainWindow = pOwner;
        }

        public bool GetPassword(string pNewPassword)
        {
            if(pNewPassword != null && pNewPassword.Trim() != string.Empty )
            {
                MessageManager.ShowErrorMessage("Please enter password.", oMainWindow);
            }           
            else
            {
                Password = UserManager.Encrypt(pNewPassword);
                if (Password == CustomerPassword)
                {
                    return true;
                }
                else
                {
                    MessageManager.ShowErrorMessage("Please enter correct password.", oMainWindow);                   
                }
            }
            return false;
        }
        #endregion
    }
}
