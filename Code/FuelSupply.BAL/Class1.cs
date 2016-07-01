using FuelSupply.DAL.Database_Entity;
using FuelSupply.DAL.Entity.UserEntity;
using FuelSupply.DAL.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.BAL
{
    public class Class1
    {
        public static List<User> GetAllUser()
        {
            return UserProvider.GetAllUser();
        }
    }
}
