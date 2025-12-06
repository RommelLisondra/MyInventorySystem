using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class SalesOrderDetailFile
    {
        public int Id { get; set; }
        public string Somno { get; set; } = null!;
        public string ItemDetailCode { get; set; } = null!;
        public int? QtyOnOrder { get; set; }
        public int? QtyInvoice { get; set; }
        public decimal? Uprice { get; set; }
        public decimal? Amount { get; set; }
        public string? RecStatus { get; set; }

        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;
        public virtual SalesOrderMasterFile SomnoNavigation { get; set; } = null!;
    }
}
