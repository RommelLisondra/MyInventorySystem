using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{ 
    public class OfficialReceiptDetailModel
    {
        public int id { get; set; }
        public string Ordno { get; set; } = null!;
        public string Simno { get; set; } = null!;
        public decimal? AmountPaid { get; set; }
        public decimal? AmountDue { get; set; }
        public string? RecStatus { get; set; }

        public OfficialReceiptMasterModel OrdnoNavigation { get; set; } = null!;
        public SalesInvoiceMasterModel SalesInvoiceMasterFileNavigation { get; set; } = null!;
    }
}
