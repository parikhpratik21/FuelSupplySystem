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
    public class AddEditUserViewModel : INotifyPropertyChanged
    {
        #region "Declaration"
        private User _SelectedUser;
        private string _Title;
        private List<UserType> _UserTypeList;
        private MainWindow oMainWindows;
        #endregion

        #region "Property"
        public string LoggedUserName
        {
            get
            {
                if (SharedData.LoggedUser != null)
                {
                    return SharedData.LoggedUser.Name;
                }
                else
                    return string.Empty;
            }
        }
        public User SelectedUser
        {
            get {
                return _SelectedUser; 
            }
            set{
                _SelectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }

        public List<UserType> UserTypeList
        {
            get
            {
                return _UserTypeList;
            }
            set
            {
                _UserTypeList = value;
                OnPropertyChanged("UserTypeList");
            }
        }

        public string Title
        {
            get
            { return _Title; }
            set
            {
                _Title = value;
                OnPropertyChanged("Title");
            }
        }

        public bool IsUserNameEnable
        {
            get
            {
                if(_SelectedUser == null || _SelectedUser.Id <= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public int PasswordRowHeight
        {
            get
            {
                if (IsUserNameEnable == true)
                    return 40;
                else
                    return 0;
            }
        }
        public Visibility IsPasswordVisible
        {
            get
            {
                if (IsUserNameEnable == true)
                    return Visibility.Visible;
                else
                    return Visibility.Hidden;
            }
        }
        #endregion

        #region "Methods"
        public AddEditUserViewModel(Window pOwnerWindow)
        {
            _UserTypeList = UserManager.GetAllUserType();
            oMainWindows = (MainWindow)pOwnerWindow;
        }

        public bool AddEditUser(string pPassword)
        {
            if (_SelectedUser.Id <= 0)
                return AddUser(pPassword);
            else
                return UpdateUser();
        }
        private bool AddUser(string pPassword)
        {
            _SelectedUser.Password = pPassword;
            string sErrorMsg = string.Empty;
            bool result = UserManager.AddUser(_SelectedUser, ref sErrorMsg);
            if (result == false)
            {
                MessageManager.ShowErrorMessage(sErrorMsg, oMainWindows);
                return false;
            }
            else
                return true;
        }

        private bool UpdateUser()
        {
            string sErrorMsg = string.Empty;
            bool result = UserManager.UpdateUser(_SelectedUser,ref sErrorMsg);
            if (result == false)
            {
                MessageManager.ShowErrorMessage(sErrorMsg, oMainWindows);
                return false;
            }
            else
                return true;
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
    }
}
