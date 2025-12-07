using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class PurchaseRequisitionMasterFile
    {
        public PurchaseRequisitionMasterFile()
        {
            PurchaseRequisitionDetailFile = new HashSet<PurchaseRequisitionDetailFile>();
        }

        public int Id { get; set; }
        public string Prno { get; set; } = null!;
        public string RequestedBy { get; set; } = null!;
        public string? Department { get; set; }
        public DateTime? DateRequested { get; set; }
        public string? ApprovedBy { get; set; }
        public string? Remarks { get; set; }
        public string? Comments { get; set; }
        public string? TermsAndCondition { get; set; }
        public string? FooterText { get; set; }
        public string? Recuring { get; set; }
        public string? RecStatus { get; set; }

        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public virtual Employee? ApprovedByNavigation { get; set; }
        public virtual Employee RequestedByNavigation { get; set; } = null!;
        public virtual ICollection<PurchaseRequisitionDetailFile> PurchaseRequisitionDetailFile { get; set; }
    }
}
