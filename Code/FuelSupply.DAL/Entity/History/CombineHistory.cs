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
    public class CombineHistory
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
        public Nullable<int> FuelStationId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> PaymentType { get; set; }
        public Nullable<decimal> CreditAmount { get; set; }
        public Nullable<int> KeyCustomerId { get; set; }
        public Nullable<int> ShiftId { get; set; }
        public string ShiftName { get; set; }
        public string KeyCustomerName { get; set; }        
        public Nullable<decimal> FuelVolume { get; set; }
        public Nullable<decimal> FuelAmount { get; set; }
        public string InvoiceNo { get; set; }
        public string HistoryType { get; set; }
        public string AttendantName { get; set; }
        public string CustomerName { get; set; }
        public Nullable<decimal> CustomerLastBalance { get; set; }
    }

    public class CombineHistoryList : List<CombineHistory> { }
}
