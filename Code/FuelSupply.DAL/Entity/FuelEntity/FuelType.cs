using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.DAL.Entity.FuelEntity
{
    public class FuelType
    {
        public FuelType()
        {
            this.FuelHistories = new HashSet<FuelHistory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FuelHistory> FuelHistories { get; set; }
    }
}
