using FuelSupply.DAL.Database_Entity;
using FuelSupply.DAL.Entity.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.DAL.Provider 
{
    public class UserProvider 
    {
        #region "Declaration"
        private static FuelSupplySystemEntities1 userDbObject;
        #endregion

        #region "Constructor"
        static UserProvider()
        {
            userDbObject = new FuelSupplySystemEntities1();
        }
        #endregion

        #region "Methods"
        public static List<User> GetAllUser()
        {        
            List<User> userList = userDbObject.Users.ToList();
            return userList;
        }

        public static List<UserType> GetAllUserType()
        {
            List<UserType> userTypeList = userDbObject.UserTypes.ToList();
            return userTypeList;
        }

        public static User GetUserById(int pUserId)
        {
            return userDbObject.Users.Where(x=> x.Id == pUserId).FirstOrDefault();
        }

        public static List<User> GetUserListByType(int pType)
        {
            return userDbObject.Users.Where(x => x.UserType == pType).ToList();
        }

        public static User Login(string pUserName, string pPassword)
        {
            return userDbObject.Users.Where(x => x.UserName == pUserName && x.Password == pPassword).FirstOrDefault();
        }

        public static bool ChangePassword(int pUserId, string pNewPassword)
        {
            User oUser = userDbObject.Users.Where(x => x.Id == pUserId).FirstOrDefault();
            if (oUser != null)
            {                
                oUser.Password = pNewPassword;
                userDbObject.SaveChanges();

                return true;
            }

            return false;
        }

        public static bool AddUser(User pUser)
        {
            User oUser = userDbObject.Users.Where(x => x.Code == pUser.Code || x.UserName == pUser.UserName).FirstOrDefault();
            if (oUser == null)
            {
                userDbObject.Users.Add(pUser);
                userDbObject.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public static bool UpdateUser(User pUser)
        {
            User oUser = userDbObject.Users.Where(x => x.Id == pUser.Id).FirstOrDefault();
            if (oUser != null)
            {                
                oUser.Address = pUser.Address;
                oUser.City = pUser.City;
                oUser.Code = pUser.Code;
                oUser.ContactNo = pUser.ContactNo;
                oUser.Country = pUser.Country;
                oUser.Name= pUser.Name;
                oUser.Pincode = pUser.Pincode;
                oUser.State = pUser.State;

                userDbObject.SaveChanges();

                 return true;
            }        
            return false;
        }

        public static bool DeleteUser(int pUserId)
        {
            User oUser = userDbObject.Users.Where(x => x.Id == pUserId).FirstOrDefault();
            if (oUser != null)
            {
                userDbObject.Users.Remove(oUser);
                userDbObject.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion
    }
}
