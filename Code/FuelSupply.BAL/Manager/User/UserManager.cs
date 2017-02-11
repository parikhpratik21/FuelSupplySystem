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
        //private const string EncryptionKey = "PBPKP21222290";
        static readonly string PasswordHash = "dbp@akl7";
        static readonly string SaltKey = "kpp_1@#$";
        static readonly string VIKey = "PBPKPP21@#222290";
        #endregion

        #region "Methods"
        public static List<User> GetAllUser()
        {
            List<User> oUserList =  UserProvider.GetAllUser();
            //foreach(User oUser in oUserList)
            //{
            //    oUser.Password = Decrypt(oUser.Password);
            //}
            return oUserList;
        }

        public static List<UserType> GetAllUserType()
        {
            return UserProvider.GetAllUserType();
        }

        public static List<Shift> GetAllTypeList()
        {
            return UserProvider.GetAllShift();
        }

        public static User GetUserById(int pUserId)
        {
            User oUser = UserProvider.GetUserById(pUserId);
            //oUser.Password = Decrypt(oUser.Password);
            return oUser;
        }

        public static List<User> GetUserListByType(int pType)
        {            
            List<User> oUserList = UserProvider.GetUserListByType(pType);
            //foreach (User oUser in oUserList)
            //{
            //    oUser.Password = Decrypt(oUser.Password);
            //}
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
                pUser.Password = Encrypt(pUser.Password);
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

        public static bool DeleteUser(int pUserId, bool pDeleteHistory)
        {           
            if (pDeleteHistory == true)
            {
                //delete cfuel and credit history
                CreditManager.DeleteCreditHistoryByUserId(pUserId);
                FuelManager.DeleteFuelHistoryByUserId(pUserId);
            }

            bool result = UserProvider.DeleteUser(pUserId);
            return result;
        }

        public static string Encrypt(string pEncryptText)
        {            
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(pEncryptText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        private static string Decrypt(string pDecryptText)
        {            
            byte[] cipherTextBytes = Convert.FromBase64String(pDecryptText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        #endregion
    }
}
