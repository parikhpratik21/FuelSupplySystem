using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.DAL.Entity.CustomerEntity
{
    public class CustomerFingerPrint
    {
        public int ID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string Policy { get; set; }
        public string FingerPrint { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
