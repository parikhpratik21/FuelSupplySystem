using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.APP.ExportEntity
{
    public class CombineHistoryExport
    {
        [Description("ID")]
        public int ID { get; set; }

        [Description("Attendant Name")]
        public string UserName { get; set; }

        [Description("Customer Name")]
        public string CustomerName { get; set; }

        [Description("Key Customer")]
        public string KeyCustomer { get; set; }

        [Description("Fuel Amount")]
        public string FuelAmount { get; set; }

        [Description("Invoice No")]
        public string InvoiceNo { get; set; }     

        [Description("Credit Amount")]
        public decimal? CreditAmount { get; set; }

        [Description("Date")]
        public string Date { get; set; }

        [Description("Shift")]
        public string Shift { get; set; }

        [Description("Type")]
        public string Type { get; set; }       
    }

    public class CombineHistoryExportList : List<CombineHistoryExport> { }
}
