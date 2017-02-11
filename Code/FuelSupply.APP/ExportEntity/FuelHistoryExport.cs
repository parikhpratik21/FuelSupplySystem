using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.APP.ExportEntity
{
    public class FuelHistoryExport
    {
        [Description("ID")]
        public int ID { get; set; }

        [Description("Attendant Name")]
        public string UserName { get; set; }

        [Description("Customer Name")]
        public string CustomerName { get; set; }

        [Description("Key Customer")]
        public string KeyCustomer { get; set; }

        [Description("Fuel Type")]
        public string FuelType { get; set; }

        [Description("Fuel Volume")]
        public decimal? FuelVolume { get; set; }

        [Description("Fuel Amount")]
        public decimal? FuelAmount { get; set; }

        [Description("Date")]
        public string Date { get; set; }
    }

    public class FuelHistoryExportList : List<FuelHistoryExport> { }
}
