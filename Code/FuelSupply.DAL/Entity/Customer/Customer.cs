using FuelSupply.DAL.Entity.CreditEntity;
using FuelSupply.DAL.Entity.FuelEntity;
using FuelSupply.DAL.Entity.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.DAL.Entity.CustomerEntity
{
    public class Customer
    {
        public Customer()
        {
            this.FuelHistories = new HashSet<FuelHistory>();
            this.Customer1 = new HashSet<Customer>();
            this.CreditHistories = new HashSet<CreditHistory>();
            this.CustomerFingerPrints = new HashSet<CustomerFingerPrint>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public Nullable<int> CustomerType { get; set; }
        public Nullable<int> KeyCustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public Nullable<int> PaymentType { get; set; }
        public Nullable<int> FuelLimitType { get; set; }
        public Nullable<decimal> PaymentLimit { get; set; }
        public Nullable<decimal> AvailablePay { get; set; }
        public string Password { get; set; }

        public virtual CustomerType CustomerType1 { get; set; }
        public virtual FuelLimitType FuelLimitType1 { get; set; }
        public virtual PaymentType PaymentType1 { get; set; }
        public virtual ICollection<FuelHistory> FuelHistories { get; set; }
        public virtual ICollection<Customer> Customer1 { get; set; }
        public virtual Customer Customer2 { get; set; }
        public virtual ICollection<CreditHistory> CreditHistories { get; set; }
        public virtual ICollection<CustomerFingerPrint> CustomerFingerPrints { get; set; }
    }
}
