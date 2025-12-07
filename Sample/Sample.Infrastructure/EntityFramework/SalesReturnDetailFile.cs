using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class SalesReturnDetailFile
    {
        public int Id { get; set; }
        public string Srmno { get; set; } = null!;
        public string ItemDetailCode { get; set; } = null!;
        public int? Qty { get; set; }
        public decimal? Uprice { get; set; }
        public decimal? Amount { get; set; }
        public string? RecStatus { get; set; }

        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;
        public virtual SalesReturnMasterFile SrmnoNavigation { get; set; } = null!;
    }
}
