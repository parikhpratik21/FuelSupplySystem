using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.DAL.Entity.CustomerEntity
{
    public class FingerPrintType
    {
        public FingerPrintType()
        {
            this.CustomerFingerPrints = new HashSet<CustomerFingerPrint>();
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CustomerFingerPrint> CustomerFingerPrints { get; set; }
    }
}
