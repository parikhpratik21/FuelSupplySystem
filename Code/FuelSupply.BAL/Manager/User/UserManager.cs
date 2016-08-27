using FuelSupply.DAL.Entity.UserEntity;
using FuelSupply.DAL.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace FuelSupply.BAL.Manager
{
    public class UserManager
    {
        #region "Property"
        private const string EncryptionKey = "PBPKP21222290";
        #endregion

        #region "Methods"
        public static List<User> GetAllUser()
        {
            List<User> oUserList =  UserProvider.GetAllUser();
            foreach(User oUser in oUserList)
            {
                oUser.Password = Decrypt(oUser.Password);
            }
            return oUserList;
        }

        public static List<UserType> GetAllUserType()
        {
            return UserProvider.GetAllUserType();
        }

        public static User GetUserById(int pUserId)
        {
            User oUser = UserProvider.GetUserById(pUserId);
            oUser.Password = Decrypt(oUser.Password);
            return oUser;
        }

        public static List<User> GetUserListByType(int pType)
        {            
            List<User> oUserList = UserProvider.GetUserListByType(pType);
            foreach (User oUser in oUserList)
            {
                oUser.Password = Decrypt(oUser.Password);
            }
            return oUserList;
        }

        public static User Login(string pUserName, string pPassword, ref string pErrorMsg)
        {
            pPassword = Encrypt(pPassword);
            User oUser = UserProvider.Login(pUserName,pPassword);   
            if(oUser == null)
            {
                pErrorMsg="Please enter valid credentials";
            }
            return oUser;
        }

        public static bool ChangePassword(int pUserId, string pNewPassword)
        {
            pNewPassword = Encrypt(pNewPassword);
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
                pUser.Password = Encrypt(pUser.Password);
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

        private static string Encrypt(string clearText)
        {
            return clearText;
            //byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            //using (Aes encryptor = Aes.Create())
            //{
            //    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            //    encryptor.Key = pdb.GetBytes(32);
            //    encryptor.IV = pdb.GetBytes(16);
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
            //        {
            //            cs.Write(clearBytes, 0, clearBytes.Length);
            //            cs.Close();
            //        }
            //        clearText = Convert.ToBase64String(ms.ToArray());
            //    }
            //}
            //return clearText;
        }

        private static string Decrypt(string cipherText)
        {
            return cipherText;
            //string EncryptionKey = "MAKV2SPBNI99212";
            //byte[] cipherBytes = Convert.FromBase64String(cipherText);
            //using (Aes encryptor = Aes.Create())
            //{
            //    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            //    encryptor.Key = pdb.GetBytes(32);
            //    encryptor.IV = pdb.GetBytes(16);
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
            //        {
            //            cs.Write(cipherBytes, 0, cipherBytes.Length);
            //            cs.Close();
            //        }
            //        cipherText = Encoding.Unicode.GetString(ms.ToArray());
            //    }
            //}
            //return cipherText;
        }

        #endregion
    }
}
