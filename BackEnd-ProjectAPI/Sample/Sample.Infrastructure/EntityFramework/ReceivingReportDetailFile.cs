using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class ReceivingReportDetailFile
    {
        public int Id { get; set; }
        public string Rrno { get; set; } = null!;
        public string ItemDetailCode { get; set; } = null!;
        public int? QtyReceived { get; set; }
        public decimal? Uprice { get; set; }
        public decimal? Amount { get; set; }
        public string? RecStatus { get; set; }
        public int? QtyReturn { get; set; }
        public string? PretrunRecStatus { get; set; }

        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;
        public virtual ReceivingReportMasterFile RrnoNavigation { get; set; } = null!;
    }
}
