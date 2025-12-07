using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class OfficialReceiptMasterFile
    {
        public OfficialReceiptMasterFile()
        {
            OfficialReceiptDetailFile = new HashSet<OfficialReceiptDetailFile>();
        }

        public int Id { get; set; }
        public string Orno { get; set; } = null!;
        public DateTime? Date { get; set; }
        public string CustNo { get; set; } = null!;
        public string? PreparedBy { get; set; }
        public string? ApprovedBy { get; set; }
        public string? RecStatus { get; set; }
        public decimal? TotAmtPaid { get; set; }
        public decimal? DisPercent { get; set; }
        public decimal? DisTotal { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Vat { get; set; }
        public string? FormPay { get; set; }
        public decimal? CashAmt { get; set; }
        public decimal? CheckAmt { get; set; }
        public string? CheckNo { get; set; }
        public string? BankName { get; set; }
        public string? Remarks { get; set; }
        public string? Comments { get; set; }
        public string? TermsAndCondition { get; set; }
        public string? FooterText { get; set; }
        public string? Recuring { get; set; }

        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public virtual Employee? ApprovedByNavigation { get; set; }
        public virtual Customer CustNoNavigation { get; set; } = null!;
        public virtual Employee? PreparedByNavigation { get; set; }
        public virtual ICollection<OfficialReceiptDetailFile> OfficialReceiptDetailFile { get; set; }
    }
}
