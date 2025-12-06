using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class StockCountDetail
    {
        public int Id { get; set; }
        public string Scmno { get; set; } = null!;
        public string ItemDetailCode { get; set; } = null!;
        public int? CountedQty { get; set; }
        public string? RecStatus { get; set; }

        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;
        public virtual StockCountMaster ScmnoNavigation { get; set; } = null!;
    }
}
