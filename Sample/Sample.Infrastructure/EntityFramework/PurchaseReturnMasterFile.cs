using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class PurchaseReturnMasterFile
    {
        public PurchaseReturnMasterFile()
        {
            PurchaseReturnDetailFile = new HashSet<PurchaseReturnDetailFile>();
        }

        public int Id { get; set; }
        public string Prmno { get; set; } = null!;
        public string SupplierNo { get; set; } = null!;
        public DateTime? Date { get; set; }
        public string? PreparedBy { get; set; }
        public string? ApprovedBy { get; set; }
        public string? RecStatus { get; set; }
        public string Rrno { get; set; } = null!;
        public string? RefNo { get; set; }
        public string? Terms { get; set; }
        public string? TypesOfPay { get; set; }
        public string? Remarks { get; set; }
        public string? Comments { get; set; }
        public string? TermsAndCondition { get; set; }
        public string? FooterText { get; set; }
        public string? Recuring { get; set; }
        public decimal? Total { get; set; }
        public decimal? DisPercent { get; set; }
        public decimal? DisTotal { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Vat { get; set; }
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;

        public virtual Employee? ApprovedByNavigation { get; set; }
        public virtual Employee? PreparedByNavigation { get; set; }
        public virtual Supplier SupplierNoNavigation { get; set; } = null!;
        public virtual ICollection<PurchaseReturnDetailFile> PurchaseReturnDetailFile { get; set; }
    }
}
