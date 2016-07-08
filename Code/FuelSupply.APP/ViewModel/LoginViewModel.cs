using FuelSupply.APP.Helper;
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
using System.Windows.Input;

namespace FuelSupply.APP.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region "Declaration"
        private MainWindow oWindow;
        private string _UserName;        
        private ICommand _LogInButtonCommand;
        private User _LogedUser;
        #endregion

        #region "Property"
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                OnPropertyChanged("UserName");
            }
        }     

        public User Loggeduser
        {
            get { return _LogedUser; }
            set { _LogedUser = value; OnPropertyChanged("Loggeduser"); }
        }
        #region LogInButtonCommand
        /// <summary>
        /// Command to handle Hopping command
        /// </summary>       
        public ICommand LogInButtonCommand
        {
            get { return _LogInButtonCommand; }
            set { _LogInButtonCommand = value; }
        }
        #endregion
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
        public LoginViewModel(Window pOwnerWindow)
        {
            oWindow = (MainWindow)pOwnerWindow;

            //_LogInButtonCommand = new RelayCommand(Login);
        }

        public void Login(string pPassword)
        {
            string pErrorMsg = string.Empty;
            if (_UserName == null)
            {
                pErrorMsg = "Please enter user name.";
                MessageManager.ShowErrorMessage(pErrorMsg, oWindow);
            }
            else if (pPassword == null)
            {
                pErrorMsg = "Please enter password.";
                MessageManager.ShowErrorMessage(pErrorMsg, oWindow);
            }
            else
            {
                User oUser = UserManager.Login(_UserName, pPassword, ref pErrorMsg);
                if (oUser == null)
                {
                    MessageManager.ShowErrorMessage(pErrorMsg, oWindow);
                }
                else
                { 
                    _LogedUser =  oUser;
                    oWindow.SuccessLogIn();
                }
            }            
        }
        #endregion
    }
}
