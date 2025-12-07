using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class SupplierImage
    {
        public int Id { get; set; }
        public string SupplierNo { get; set; } = null!;
        public string? FilePath { get; set; }

        public virtual Supplier SupplierNoNavigation { get; set; } = null!;
    }
}
