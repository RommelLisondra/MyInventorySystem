using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.DTOs
{
    public class SalesInvoiceDetailDto 
    {
        public int id { get; set; }
        public string Sidno { get; set; } = null!;
        public string ItemDetailCode { get; set; } = null!;
        public int QtyInv { get; set; }
        public int QtyRet { get; set; }
        public int QtyDel { get; set; }
        public decimal? Uprice { get; set; }
        public decimal? Amount { get; set; }
        public string? SrrecStatus { get; set; }
        public string? DrrecStatus { get; set; }
        public string? RecStatus { get; set; }

        public ItemDetailDto ItemDetailCodeNavigation { get; set; } = null!;
        public SalesInvoiceMasterDto SidnoNavigation { get; set; } = null!;
    }
}
