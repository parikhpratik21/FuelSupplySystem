using FuelSupply.DAL.Entity.CustomerEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuelSupply.DAL.Entity.UserEntity;

namespace FuelSupply.DAL.Entity.FuelEntity
{
    public class Fetch_FuelHistory_Result
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public string FuelType { get; set; }
        public string ShiftName { get; set; }
        public string AttendantName { get; set; }
        public string KeyCustomerName { get; set; }
        public Nullable<int> KeyCustomerId { get; set; }
        public Nullable<decimal> FuelAmount { get; set; }
        public Nullable<decimal> FuelVolume { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<decimal> ActualFuelAmount { get; set; }
        public Nullable<decimal> ActualFuelVolume { get; set; }
    }   
}
