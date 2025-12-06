using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class PurchaseReturnDetailFile
    {
        public int Id { get; set; }
        public string Prmno { get; set; } = null!;
        public string ItemDetailCode { get; set; } = null!;
        public int? Quantity { get; set; }
        public decimal? Uprice { get; set; }
        public decimal? Amount { get; set; }
        public string? RecStatus { get; set; }

        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;
        public virtual PurchaseReturnMasterFile PrmnoNavigation { get; set; } = null!;
    }
}
