using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class ItemSupplier
    {
        public int Id { get; set; }
        public string ItemDetailCode { get; set; } = null!;
        public string SupplierNo { get; set; } = null!;
        public string? RecStatus { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? LeadTime { get; set; }
        public string? Terms { get; set; }

        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;
        public virtual Supplier SupplierNoNavigation { get; set; } = null!;
    }
}
