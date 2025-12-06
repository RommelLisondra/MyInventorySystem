using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class SalesInvoiceMasterFile
    {
        public SalesInvoiceMasterFile()
        {
            OfficialReceiptDetailFile = new HashSet<OfficialReceiptDetailFile>();
            SalesInvoiceDetailFile = new HashSet<SalesInvoiceDetailFile>();
        }

        public int Id { get; set; }
        public string Simno { get; set; } = null!;
        public string Somno { get; set; } = null!;
        public DateTime? Date { get; set; }
        public string CustNo { get; set; } = null!;
        public string? Terms { get; set; }
        public string? TypesOfPay { get; set; }
        public string? SalesRef { get; set; }
        public string? Remarks { get; set; }
        public string? Comments { get; set; }
        public string? TermsAndCondition { get; set; }
        public string? FooterText { get; set; }
        public string? Recuring { get; set; }
        public decimal? Total { get; set; }
        public decimal? DisPercent { get; set; }
        public decimal? DisTotal { get; set; }
        public decimal? DeliveryCost { get; set; }
        public decimal? Balance { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Vat { get; set; }
        public DateTime? DueDate { get; set; }
        public string? OrrecStatus { get; set; }
        public string? DrrecStatus { get; set; }
        public string? SrrecStatus { get; set; }
        public string? PreparedBy { get; set; }
        public string? ApprovedBy { get; set; }
        public string? RecStatus { get; set; }
        public int BranchId { get; set; }


        public virtual Branch Branch { get; set; } = null!;
        public virtual Employee? ApprovedByNavigation { get; set; }
        public virtual Customer CustNoNavigation { get; set; } = null!;
        public virtual Employee? PreparedByNavigation { get; set; }
        public virtual ICollection<OfficialReceiptDetailFile> OfficialReceiptDetailFile { get; set; }
        public virtual ICollection<SalesInvoiceDetailFile> SalesInvoiceDetailFile { get; set; }
    }
}
