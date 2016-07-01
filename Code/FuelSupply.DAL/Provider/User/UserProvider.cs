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
        private static FuelSupplySystemEntities userDbObject;
        #endregion

        #region "Constructor"
        static UserProvider()
        {
           
        }
        #endregion

        #region "Methods"
        public static List<User> GetAllUser()
        {
            var objDbContext = new FuelSupplySystemEntities();
            List<User> userList = objDbContext.Users.ToList();
           // MessageBox.Show(userList.Count.ToString());

            return userList;
        }

        public static User GetUserById(int pUserId)
        {
            return userDbObject.Users.Where(x=> x.Id == pUserId).FirstOrDefault();
        }
        #endregion
    }
}
