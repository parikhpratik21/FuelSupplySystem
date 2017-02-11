using FuelSupply.DAL.Entity.CreditEntity;
using FuelSupply.DAL.Entity.FuelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.DAL.Entity.UserEntity
{
    public class Shift
    {
        public Shift()
        {
            this.CreditHistories = new HashSet<CreditHistory>();
            this.FuelHistories = new HashSet<FuelHistory>();
        }

        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
        public Nullable<System.DateTime> ShiftStartTime { get; set; }
        public Nullable<System.DateTime> ShiftEndTime { get; set; }

        public virtual ICollection<CreditHistory> CreditHistories { get; set; }
        public virtual ICollection<FuelHistory> FuelHistories { get; set; }
    }
}
