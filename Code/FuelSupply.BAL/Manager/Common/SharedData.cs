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
        #region "Declaration"
        public static User LoggedUser {get;set;}
        public static FuelStation CurrentFuelStation { get; set; }
        #endregion
    }
}
