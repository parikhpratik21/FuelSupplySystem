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
        private List<User> _UserList;
        public UserDisplayViewModel(Window pOwnerWindow)
        {
            _UserList = UserManager.GetAllUser();
        }

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
