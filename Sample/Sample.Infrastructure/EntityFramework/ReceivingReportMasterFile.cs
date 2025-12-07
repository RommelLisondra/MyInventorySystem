using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class ReceivingReportMasterFile
    {
        public ReceivingReportMasterFile()
        {
            ReceivingReportDetailFile = new HashSet<ReceivingReportDetailFile>();
        }

        public int Id { get; set; }
        public string Rrno { get; set; } = null!;
        public string Pono { get; set; } = null!;
        public DateTime? Date { get; set; }
        public string? ReceivedBy { get; set; }
        public string? VerifiedBy { get; set; }
        public string? Remarks { get; set; }
        public string? RecStatus { get; set; }
        public string SupplierNo { get; set; } = null!;
        public string? RefNo { get; set; }
        public string? Terms { get; set; }
        public string? TypesOfPay { get; set; }
        public string? Comments { get; set; }
        public string? TermsAndCondition { get; set; }
        public string? FooterText { get; set; }
        public string? Recuring { get; set; }
        public decimal? Total { get; set; }
        public decimal? DisPercent { get; set; }
        public decimal? DisTotal { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Vat { get; set; }
        public string? PreturnRecStatus { get; set; }

        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public virtual PurchaseOrderMasterFile PonoNavigation { get; set; } = null!;
        public virtual Employee? ReceivedByNavigation { get; set; }
        public virtual Employee? VerifiedByNavigation { get; set; }
        public virtual ICollection<ReceivingReportDetailFile> ReceivingReportDetailFile { get; set; }
    }
}
