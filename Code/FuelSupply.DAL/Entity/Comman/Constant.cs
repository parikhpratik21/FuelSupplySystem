using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.DAL.Entity.Comman
{
    public class Constant
    {
        #region "Enum"
        public enum eCustomerType
        {
            KeyCustomer = 1,
            Driver = 2,
        }
            
        public enum eUserType
        {
            Admin = 1,
            Operator = 2,
        }

        public enum ePaymentType
        {
            Credit = 1,
            Cash = 2,
        }

        public enum eFuelLimitType
        {
            PerDay = 1,
            PerMonth = 2,
            PerYear = 3,
        }

        #endregion
    }
}
