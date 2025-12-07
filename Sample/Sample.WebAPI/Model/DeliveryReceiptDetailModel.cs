using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class DeliveryReceiptDetailModel 
    {
        public int id { get; set; }
        public string Drdno { get; set; } = null!;
        public string ItemDetailCode { get; set; } = null!;
        public int? QtyDel { get; set; }
        public decimal? Uprice { get; set; }
        public decimal? Amount { get; set; }
        public string? RecStatus { get; set; }

        public DeliveryReceiptMasterModel DrdnoNavigation { get; set; } = null!;
        public ItemDetailModel ItemDetailCodeNavigation { get; set; } = null!;
    }
}
