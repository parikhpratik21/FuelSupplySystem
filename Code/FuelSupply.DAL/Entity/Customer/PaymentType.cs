using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuelSupply.DAL.Entity.CustomerEntity;
using FuelSupply.DAL.Entity.CreditEntity;

namespace FuelSupply.DAL.Entity.CustomerEntity
{
    public class PaymentType
    {
        public PaymentType()
        {
            this.Customers = new HashSet<Customer>();
            this.CreditHistories = new HashSet<CreditHistory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<CreditHistory> CreditHistories { get; set; }
    }
}
