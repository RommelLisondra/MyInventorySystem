using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class PurchaseRequisitionDetailModel
    {
        public int id { get; set; }
        public string Prdno { get; set; } = null!;
        public string ItemDetailCode { get; set; } = null!;
        public int? QtyReq { get; set; }
        public int? QtyOrder { get; set; }
        public string? RecStatus { get; set; }

        public ItemDetailModel ItemDetailCodeNavigation { get; set; } = null!;
        public PurchaseRequisitionMasterModel PrdnoNavigation { get; set; } = null!;
    }
}
