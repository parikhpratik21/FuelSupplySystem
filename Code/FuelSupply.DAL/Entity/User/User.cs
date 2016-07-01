using FuelSupply.DAL.Entity.FuelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.DAL.Entity.UserEntity
{
    public class User
    {
        public User()
        {
            this.FuelHistories = new HashSet<FuelHistory>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<int> UserType { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ContactNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<FuelHistory> FuelHistories { get; set; }
        public virtual UserType UserType1 { get; set; }
    }
}
