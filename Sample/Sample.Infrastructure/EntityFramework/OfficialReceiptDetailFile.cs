using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class OfficialReceiptDetailFile
    {
        public int Id { get; set; }
        public string Orno { get; set; } = null!;
        public decimal? AmountPaid { get; set; }
        public string? RecStatus { get; set; }
        public string Simno { get; set; } = null!;
        public decimal? AmountDue { get; set; }

        public virtual OfficialReceiptMasterFile OrnoNavigation { get; set; } = null!;
        public virtual SalesInvoiceMasterFile SimnoNavigation { get; set; } = null!;
    }
}
