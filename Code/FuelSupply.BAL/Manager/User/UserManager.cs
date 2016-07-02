using FuelSupply.DAL.Entity.UserEntity;
using FuelSupply.DAL.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.BAL.Manager
{
    public class UserManager
    {
        #region "Declaration"

        #endregion

        #region "Methods"
        public static List<User> GetAllUser()
        {
            return UserProvider.GetAllUser();
        }

        public static User GetUserById(int pUserId)
        {
            return UserProvider.GetUserById(pUserId);
        }

        public static List<User> GetUserListByType(int pType)
        {
            return UserProvider.GetUserListByType(pType);
        }

        public static User Login(string pUserName, string pPassword, ref string pErrorMsg)
        {
            return UserProvider.Login(pUserName,pPassword);           
        }

        public static bool ChangePassword(int pUserId, string pNewPassword)
        {
            return UserProvider.ChangePassword(pUserId, pNewPassword);
        }

        public static bool AddUser(User pUser)
        {
            return UserProvider.AddUser(pUser);
        }

        public static bool UpdateUser(User pUser)
        {
            return UserProvider.UpdateUser(pUser);
        }

        public static bool DeleteUser(int pUserId)
        {
            return UserProvider.DeleteUser(pUserId);
        }


        #endregion
    }
}
