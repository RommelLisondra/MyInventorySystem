using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class PurchaseRequisitionDetailFile
    {
        public int Id { get; set; }
        public string Prno { get; set; } = null!;
        public string ItemDetailCode { get; set; } = null!;
        public int? QtyRequested { get; set; }
        public string? Uom { get; set; }
        public string? RecStatus { get; set; }
        public int? QtyOrder { get; set; }

        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;
        public virtual PurchaseRequisitionMasterFile PrnoNavigation { get; set; } = null!;
    }
}
