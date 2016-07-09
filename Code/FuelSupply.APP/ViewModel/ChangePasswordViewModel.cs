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
            
        }
        #endregion
    }
}
