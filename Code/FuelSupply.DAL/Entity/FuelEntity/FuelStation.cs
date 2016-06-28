using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.DAL.Entity.FuelEntity
{
    public class FuelStation
    {
        public FuelStation()
        {
            this.FuelHistories = new HashSet<FuelHistory>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public virtual ICollection<FuelHistory> FuelHistories { get; set; }
    }
}
