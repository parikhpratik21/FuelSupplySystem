using FuelSupply.DAL.Database_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.DAL.Provider
{
    public class FuelProvider
    {
         #region "Declaration"
        private static FuelSupplySystemEntities1 fuelDbObject;
        #endregion

        #region "Constructor"
        static FuelProvider()
        {
            fuelDbObject = new FuelSupplySystemEntities1();
        }
        #endregion

        #region "Methods"

        #endregion
    }
}
