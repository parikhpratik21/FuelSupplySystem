﻿using FuelSupply.BAL.Manager;
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
        #endregion

        #region "Property"
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
        #endregion
        public AddEditUserViewModel(Window pOwnerWindow)
        {
            _UserTypeList = UserManager.GetAllUserType();
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
    }
}