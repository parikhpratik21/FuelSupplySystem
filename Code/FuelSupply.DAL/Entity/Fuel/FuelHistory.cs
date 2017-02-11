using FuelSupply.DAL.Entity.CustomerEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuelSupply.DAL.Entity.UserEntity;

namespace FuelSupply.DAL.Entity.FuelEntity
{
    public class FuelHistory
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
        public Nullable<int> FuelStationId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> FuelType { get; set; }
        public Nullable<decimal> FuelVolume { get; set; }
        public Nullable<decimal> FuelAmount { get; set; }
        public Nullable<int> KeyCustomerId { get; set; }
        public string KeyCustomerName { get; set; }
        public string CustomerName { get; set; }
        public string UserName { get; set; }
        public Nullable<int> ShiftId { get; set; }
        public string ShiftName { get; set; }
        public string InvoiceNo { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual FuelStation FuelStation { get; set; }
        public virtual FuelType FuelType1 { get; set; }
        public virtual User User { get; set; }
        public virtual Shift Shift { get; set; }
    }   
}
