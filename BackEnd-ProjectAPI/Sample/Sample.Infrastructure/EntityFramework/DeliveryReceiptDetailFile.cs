using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class DeliveryReceiptDetailFile
    {
        public int Id { get; set; }
        public string Drdno { get; set; } = null!;
        public string ItemDetailCode { get; set; } = null!;
        public int? QtyDel { get; set; }
        public decimal? Uprice { get; set; }
        public decimal? Amount { get; set; }
        public string? RecStatus { get; set; }

        public virtual DeliveryReceiptMasterFile DrdnoNavigation { get; set; } = null!;
        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;
    }
}
