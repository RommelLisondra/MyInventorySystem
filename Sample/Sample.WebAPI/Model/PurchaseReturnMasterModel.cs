using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class PurchaseReturnMasterModel 
    {
        public int id { get; set; }
        public string PretMno { get; set; } = null!;
        public string Rrno { get; set; } = null!;
        public string? RefNo { get; set; }
        public string SupplierNo { get; set; } = null!;
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
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
        public string PrepBy { get; set; } = null!;
        public string ApprBy { get; set; } = null!;
        public string? RecStatus { get; set; }
        public int BranchId { get; set; }

        public BranchModel Branch { get; set; } = null!;
        public EmployeeModel ApprByNavigation { get; set; } = null!;
        public EmployeeModel PrepByNavigation { get; set; } = null!;
        public ReceivingReportMasterModel RrnoNavigation { get; set; } = null!;
        public SupplierModel SupNoNavigation { get; set; } = null!;
        public ICollection<PurchaseReturnDetailModel>? PurchaseReturnDetailFile { get; set; }
    }
}
