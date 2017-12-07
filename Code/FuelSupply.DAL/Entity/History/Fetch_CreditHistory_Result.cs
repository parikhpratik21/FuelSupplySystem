using FuelSupply.DAL.Database_Entity;
using FuelSupply.DAL.Entity.CustomerEntity;
using FuelSupply.DAL.Entity.FuelEntity;
using FuelSupply.DAL.Entity.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.DAL.Entity.CreditEntity
{
    public class Fetch_CreditHistory_Result
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public string PaymentType { get; set; }
        public Nullable<decimal> CreditAmount { get; set; }
        public string ShiftName { get; set; }
        public string AttendantName { get; set; }
        public string KeyCustomerName { get; set; }
        public Nullable<int> KeyCustomerId { get; set; }
        public Nullable<bool> IsAdjustmentCredit { get; set; }
        public Nullable<decimal> CustomerLastBalance { get; set; }
    }
}
