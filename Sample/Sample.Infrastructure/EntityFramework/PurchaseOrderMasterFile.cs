using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class PurchaseOrderMasterFile
    {
        public PurchaseOrderMasterFile()
        {
            PurchaseOrderDetailFile = new HashSet<PurchaseOrderDetailFile>();
            ReceivingReportMasterFile = new HashSet<ReceivingReportMasterFile>();
        }

        public int Id { get; set; }
        public string Pono { get; set; } = null!;
        public string SupplierNo { get; set; } = null!;
        public DateTime? Date { get; set; }
        public string? Terms { get; set; }
        public string? PreparedBy { get; set; }
        public string? ApprovedBy { get; set; }
        public string? Remarks { get; set; }
        public string? RecStatus { get; set; }
        public string Prno { get; set; } = null!;
        public string? TypesofPay { get; set; }
        public DateTime? Time { get; set; }
        public DateTime? DateNeeded { get; set; }
        public string? ReferenceNo { get; set; }
        public decimal? Total { get; set; }
        public decimal? DisPercent { get; set; }
        public decimal? DisTotal { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Vat { get; set; }
        public string? Comments { get; set; }
        public string? TermsAndCondition { get; set; }
        public string? FooterText { get; set; }
        public string? Recuring { get; set; }
        public string? RrrecStatus { get; set; }
        public string? PreturnRecStatus { get; set; }

        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public virtual Employee? ApprovedByNavigation { get; set; }
        public virtual Employee? PreparedByNavigation { get; set; }
        public virtual Supplier SupplierNoNavigation { get; set; } = null!;
        public virtual ICollection<PurchaseOrderDetailFile> PurchaseOrderDetailFile { get; set; }
        public virtual ICollection<ReceivingReportMasterFile> ReceivingReportMasterFile { get; set; }
    }
}
