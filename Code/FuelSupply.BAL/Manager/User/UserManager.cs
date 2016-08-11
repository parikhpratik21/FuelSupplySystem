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

        public static List<UserType> GetAllUserType()
        {
            return UserProvider.GetAllUserType();
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
            User oUser = UserProvider.Login(pUserName,pPassword);   
            if(oUser == null)
            {
                pErrorMsg="Please enter valid credentials";
            }
            return oUser;
        }

        public static bool ChangePassword(int pUserId, string pNewPassword)
        {
            return UserProvider.ChangePassword(pUserId, pNewPassword);
        }

        public static bool AddUser(User pUser, ref string pErrorString)
        {
            if (UserProvider.ValidateUser(pUser) == false)
            {
                pErrorString = "User code and UserName must be unique";
                return false;
            }
            else
            {
                bool result = UserProvider.AddUser(pUser);
                if (result == false)
                {
                    pErrorString = "Error while adding User, Please try again.";
                    return false;
                }
                else
                    return true;
            }               
        }

        public static bool UpdateUser(User pUser,ref string pErrorString)
        {
            if (UserProvider.ValidateUser(pUser) == false)
            {
                pErrorString = "User code and UserName must be unique";
                return false;
            }
            else
            {
                bool result = UserProvider.UpdateUser(pUser);
                if (result == false)
                {
                    pErrorString = "Error while updating User, Please try again.";
                    return false;
                }
                else
                    return true;
            }               
        }

        public static bool DeleteUser(int pUserId)
        {
            return UserProvider.DeleteUser(pUserId);
        }


        #endregion
    }
}
