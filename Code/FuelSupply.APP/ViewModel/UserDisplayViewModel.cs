using FuelSupply.APP.Helper;
using FuelSupply.BAL.Manager;
using FuelSupply.BAL.Manager.Common;
using FuelSupply.DAL.Entity.UserEntity;
using FuelSupply.DAL.Provider.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
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

        public bool BackUpResoreDatabase(string pType, string pFileName)
        {
            SqlConnection oSqlConnection = null;
            try
            {
                string sSqlConnectionString = "data source=.\\SQLEXPRESS;initial catalog=FuelSupplySystem;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
                sSqlConnectionString = sSqlConnectionString.Replace("FuelSupplySystem", "master");

                string pSqlQuery = string.Empty;
                if (pType == "backup")
                {
                    pSqlQuery = "backup database FuelSupplySystem to disk='" + pFileName + "'";
                }
                else if (pType == "restore")
                {
                    pSqlQuery = "RESTORE DATABASE FuelSupplySystem FROM disk='" + pFileName + "'";

                    KillAllConnection(sSqlConnectionString);

                    //System.Security.Principal.SecurityIdentifier sid = new System.Security.Principal.SecurityIdentifier(System.Security.Principal.WellKnownSidType.NetworkServiceSid, null);
                    //System.Security.Principal.NTAccount acct = (System.Security.Principal.NTAccount)sid.Translate(typeof(System.Security.Principal.NTAccount));
                    //string strNetworkAccount = acct.ToString();

                    //System.IO.FileInfo fileInfoObj = default(System.IO.FileInfo);
                    //System.Security.AccessControl.FileSecurity fileSecurityObj = default(System.Security.AccessControl.FileSecurity);
                    //fileInfoObj = new System.IO.FileInfo(pFileName);
                    //fileSecurityObj = fileInfoObj.GetAccessControl();
                    //fileSecurityObj.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(strNetworkAccount, System.Security.AccessControl.FileSystemRights.ReadAndExecute & System.Security.AccessControl.FileSystemRights.Read, System.Security.AccessControl.AccessControlType.Allow));
                    //fileInfoObj.SetAccessControl(fileSecurityObj);
                }
                
                oSqlConnection = new SqlConnection(sSqlConnectionString);
                oSqlConnection.Open();

                SqlCommand oBackupRestoreCommand = new SqlCommand(pSqlQuery, oSqlConnection);
                oBackupRestoreCommand.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                LogManager.logExceptionMessage("UserDisplayViewModel","BackUpResoreDatabase", ex);
                return false;
            }
            finally
            {
                try
                {
                    if (oSqlConnection != null && oSqlConnection.State == System.Data.ConnectionState.Open)
                    {
                        oSqlConnection.Close();
                        oSqlConnection.Dispose();
                    }
                    oSqlConnection = null;
                }
                catch(Exception ex1)
                {
                    LogManager.logExceptionMessage("UserDisplayViewModel", "BackUpResoreDatabase_Connection_Close", ex1);
                }
            }

            return true;
        }

        private void KillAllConnection(string pSqlConnectionString)
        {
            SqlConnection oSqlConnection = null;
            try
            {
                oSqlConnection = new SqlConnection(pSqlConnectionString);
                oSqlConnection.Open();

                SqlCommand sqlCommand = default(SqlCommand);

                sqlCommand = new SqlCommand();
                sqlCommand.Connection = oSqlConnection;

                sqlCommand.CommandText =  "SET NOCOUNT ON " + 
                    "DECLARE @DBName varchar(50) " + 
                    "DECLARE @spidstr varchar(8000) " + 
                    "DECLARE @ConnKilled smallint " + 
                    "SET @ConnKilled=0 " +
                    "SET @spidstr = '' " + "Set @DBName = 'FuelSupplySystem' " + 
                    "IF db_id(@DBName) < 4 " + "BEGIN " + "PRINT 'Connections to system databases cannot be killed' " + 
                    "RETURN " + "END " + "SELECT @spidstr=coalesce(@spidstr,',' )+'kill '+convert(varchar, spid)+ '; ' " + 
                    "FROM master..sysprocesses WHERE dbid=db_id(@DBName) " + "IF LEN(@spidstr) > 0 " + "BEGIN " + 
                    "EXEC(@spidstr) " + "SELECT @ConnKilled = COUNT(1) " + 
                    "FROM master..sysprocesses WHERE dbid=db_id(@DBName) " + "END";

                sqlCommand.ExecuteNonQuery();            
            }
            catch (Exception ex)
            {
                LogManager.logExceptionMessage("UserDisplayViewModel", "KillAllConnection", ex);
            }
            finally
            {
                try
                {
                    if (oSqlConnection != null && oSqlConnection.State == System.Data.ConnectionState.Open)
                    {
                        oSqlConnection.Close();
                        oSqlConnection.Dispose();
                    }
                    oSqlConnection = null;
                }
                catch (Exception ex1)
                {
                    LogManager.logExceptionMessage("UserDisplayViewModel", "BackUpResoreDatabase_Connection_Close", ex1);
                }
            }
        }
        #endregion
    }
}
