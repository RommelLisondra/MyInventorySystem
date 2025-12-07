using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class PurchaseOrderDetailFile
    {
        public int Id { get; set; }
        public string Pono { get; set; } = null!;
        public string ItemDetailCode { get; set; } = null!;
        public int? QtyOrder { get; set; }
        public int? QtyReceived { get; set; }
        public decimal? Uprice { get; set; }
        public decimal? Amount { get; set; }
        public string? RrrecStatus { get; set; }
        public string? RecStatus { get; set; }

        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;
        public virtual PurchaseOrderMasterFile PonoNavigation { get; set; } = null!;
    }
}
