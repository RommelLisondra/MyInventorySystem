using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class DeliveryReceiptMasterFile
    {
        public DeliveryReceiptMasterFile()
        {
            DeliveryReceiptDetailFile = new HashSet<DeliveryReceiptDetailFile>();
        }

        public int Id { get; set; }
        public string Drmno { get; set; } = null!;
        public string CustNo { get; set; } = null!;
        public string Simno { get; set; } = null!;
        public DateTime? Date { get; set; }
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
        public decimal? DeliveryCost { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Vat { get; set; }
        public string? PrepBy { get; set; }
        public string? ApprBy { get; set; }
        public string? RecStatus { get; set; }
        public int BranchId { get; set; }

        public virtual Branch Branch { get; set; } = null!;
        public virtual Employee? ApprByNavigation { get; set; }
        public virtual Customer CustNoNavigation { get; set; } = null!;
        public virtual Employee? PrepByNavigation { get; set; }
        public virtual ICollection<DeliveryReceiptDetailFile> DeliveryReceiptDetailFile { get; set; }
    }
}
