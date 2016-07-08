using FuelSupply.BAL.Manager;
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
    public class UserDisplayViewModel : INotifyPropertyChanged
    {
        #region "Declaration"
        private List<User> _UserList;
        private User _selectedUser;
        #endregion

        public UserDisplayViewModel(Window pOwnerWindow)
        {
            _UserList = UserManager.GetAllUser();
        }

        #region "Property"
        public List<User> UserList
        {
            get { 
                return _UserList; 
            }
            set
            {
                _UserList = value;
                OnPropertyChanged("UserList");
            }
        }

        public User SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                _selectedUser = value;
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
    }
}
