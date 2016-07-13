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
    public class CreditHistory
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
        public Nullable<int> FuelStationId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> PaymentType { get; set; }
        public Nullable<decimal> CreditAmount { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual FuelStation FuelStation { get; set; }
        public virtual PaymentType PaymentType1 { get; set; }
        public virtual User User { get; set; }
    }
}
