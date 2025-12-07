using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class PurchaseOrderDetailModel
    {
        public int id { get; set; }
        public string Podno { get; set; } = null!;
        public string ItemDetailCode { get; set; } = null!;
        public int? QtyOrder { get; set; }
        public int? QtyReceived { get; set; }
        public decimal? Uprice { get; set; }
        public decimal? Amount { get; set; }
        public string? RrrecStatus { get; set; }
        public string? RecStatus { get; set; }

        public ItemDetailModel ItemDetailCodeNavigation { get; set; } = null!;
        public PurchaseOrderMasterModel PodnoNavigation { get; set; } = null!;
    }
}
