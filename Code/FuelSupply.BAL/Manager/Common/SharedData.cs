using FuelSupply.DAL.Entity.FuelEntity;
using FuelSupply.DAL.Entity.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.BAL.Manager.Common
{
    public class SharedData
    {
        #region "Enums"
        public enum UserType
        {            
            Admin = 1,
            Operator = 2
        }

        public enum HisotryByType
        {
            Key_Customer = 1,
            Customer = 2,
            User = 3,
            Fuel_Type = 4
        }
        #endregion

        #region "Declaration"
        public static User LoggedUser {get;set;}
        public static FuelStation CurrentFuelStation { get; set; }
        #endregion
    }
}
