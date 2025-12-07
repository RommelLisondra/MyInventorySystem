using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class SalesOrderDetailModel 
    {
        public int id { get; set; }
        public string Sodno { get; set; } = null!;
        public string ItemDetailCode { get; set; } = null!;
        public int? QtyOnOrder { get; set; }
        public int? QtyInvoice { get; set; }
        public decimal? Uprice { get; set; }
        public decimal? Amount { get; set; }
        public string? RecStatus { get; set; }

        public ItemDetailModel ItemDetailCodeNavigation { get; set; } = null!;
        public SalesOrderMasterModel SodnoNavigation { get; set; } = null!;
    }
}
