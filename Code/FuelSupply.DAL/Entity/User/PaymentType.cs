using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuelSupply.DAL.Entity.CustomerEntity;

namespace FuelSupply.DAL.Entity.UserEntity
{
    public class PaymentType
    {
        public PaymentType()
        {
            this.Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
