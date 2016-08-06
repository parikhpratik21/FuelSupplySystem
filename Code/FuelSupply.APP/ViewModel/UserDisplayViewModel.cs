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
    public class UserDisplayViewModel : INotifyPropertyChanged
    {
        #region "Declaration"
        private List<User> _DisplayedUserList;
        private List<User> _OriginalUserList;
        private User _selectedUser;
        private MainWindow oMainWindow;
        private ICommand _searchCommand;
        private string _SearchTerms;
        #endregion

        public UserDisplayViewModel(Window pOwnerWindow)
        {
            _OriginalUserList = UserManager.GetAllUser();
            oMainWindow = (MainWindow)pOwnerWindow;
            _DisplayedUserList = _OriginalUserList;

            SearchCommand = new RelayCommand(SearchText);
        }

        #region "Property"
        public string SearchTerms
        {
            get
            {
                return _SearchTerms;
            }
            set
            {
                _SearchTerms = value;
                OnPropertyChanged("SearchTerms");
                if(value != null && value.Length == 0 && _OriginalUserList != null && UserList != null && UserList.Count != _OriginalUserList.Count)
                {
                    UserList = _OriginalUserList;
                    OnPropertyChanged("UserList");
                }
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
        public List<User> UserList
        {
            get {
                return _DisplayedUserList; 
            }
            set
            {
                _DisplayedUserList = value;
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

        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand;
            }
            set
            {
                _searchCommand = value;
                OnPropertyChanged("SearchCommand");                
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
        public void DeleteUser()
        {
            bool result = MessageManager.ShowConfirmationMessage("Are you sure you want to delete selected user?", oMainWindow);
            if(result == true)
            {
                bool deleteResult = UserManager.DeleteUser(_selectedUser.Id);
                if(deleteResult == false)
                {
                    MessageManager.ShowErrorMessage("Error while deleting user, Please try again.", oMainWindow);
                }
                else
                {
                    _OriginalUserList = UserManager.GetAllUser();
                    UserList = _OriginalUserList;
                }
            }
        }    

        private void SearchText()
        {
            string searchString = SearchTerms.ToLower().Trim();

            UserList = _OriginalUserList.Where(x => x.Name.ToLower().Contains(searchString) == true || x.UserName.ToLower().Contains(searchString) == true).ToList();
            OnPropertyChanged("UserList");
        }
        #endregion
    }
}
