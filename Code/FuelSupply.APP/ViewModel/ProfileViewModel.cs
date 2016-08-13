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
    public class ProfileViewModel : INotifyPropertyChanged
    {
        #region "Declaration"
        private User _SelectedUser;
        private List<UserType> _UserTypeList;
        MainWindow oMainWindows;
        #endregion

        #region "Property"
        public User SelectedUser
        {
            get
            { return _SelectedUser; }
            set
            {
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

        public bool IsProfileEnable
        {
            get
            {
                if (SharedData.LoggedUser != null && SharedData.LoggedUser.UserType == (int?)SharedData.UserType.Admin)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        #endregion
        public ProfileViewModel(Window pOwnerForm)
        {
            _UserTypeList = UserManager.GetAllUserType();
            oMainWindows = (MainWindow)pOwnerForm;
        }

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

        public bool UpdateUser()
        {
            string sErrorMsg = string.Empty;
            bool result = UserManager.UpdateUser(_SelectedUser, ref sErrorMsg);
            if (result == false)
            {
                MessageManager.ShowErrorMessage(sErrorMsg, oMainWindows);
                return false;
            }
            else
            {
                MessageManager.ShowInformationMessage("Profile updated successfully",oMainWindows);
                return true;
            }
        }
    }
}
