﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupply.APP.ExportEntity
{
    public class CreditHistoryExport
    {
        [Description("ID")]
        public int ID { get; set; }

        [Description("Attendant Name")]
        public string UserName { get; set; }

        [Description("Customer Name")]
        public string CustomerName { get; set; }

        [Description("Key Customer")]
        public string KeyCustomer { get; set; }

        [Description("Payment Type")]
        public string PaymentType { get; set; }     

        [Description("Credit Amount")]
        public decimal? CreditAmount { get; set; }

        [Description("Date")]
        public string Date { get; set; }

        [Description("Shift")]
        public string Shift { get; set; }
        
        public bool IsAdjustmentCreditHistory { get; set; }
    }

    public class CreditHistoryExportList : List<CreditHistoryExport> { }
}
