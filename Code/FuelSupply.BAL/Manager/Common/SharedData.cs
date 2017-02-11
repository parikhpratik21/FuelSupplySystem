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

        public enum FuelHisotryByType
        {
            Key_Customer = 1,
            Customer = 2,
            User = 3,
            Fuel_Type = 4
        }

        public enum CreditHisotryByType
        {
            Key_Customer = 1,
            Customer = 2,
            User = 3,
            Payment_Type = 4
        }
        #endregion

        #region "Declaration"
        public static User LoggedUser {get;set;}
        public static FuelStation CurrentFuelStation { get; set; }
        public static Shift CurrentShift { get; set; }
        #endregion
    }
}
