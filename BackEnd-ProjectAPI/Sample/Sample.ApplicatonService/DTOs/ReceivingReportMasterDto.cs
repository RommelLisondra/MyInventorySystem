using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.DTOs
{
    public class ReceivingReportMasterDto 
    {
        public int id { get; set; }
        public string Rrmno { get; set; } = null!;
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        public Guid SupNo { get; set; }
        public string? RefNo { get; set; }
        public string Pono { get; set; } = null!;
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
        public string ReceivedBy { get; set; } = null!;
        public string? PreturnRecStatus { get; set; }
        public string? RecStatus { get; set; }
        public int BranchId { get; set; }
        public BranchDto Branch { get; set; } = null!;

        public PurchaseOrderMasterDto PonoNavigation { get; set; } = null!;
        public EmployeeDto ReceivedByNavigation { get; set; } = null!;
        public SupplierDto SupNoNavigation { get; set; } = null!;
        public PurchaseReturnMasterDto? PurchaseReturnMasterFile { get; set; }
        public ICollection<ReceivingReportDetailDto>? ReceivingReportDetailFile { get; set; }
    }
}
