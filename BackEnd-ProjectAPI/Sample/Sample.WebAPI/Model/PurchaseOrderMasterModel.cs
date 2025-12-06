using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class PurchaseOrderMasterModel 
    {
        public int id { get; set; }
        public string Pomno { get; set; } = null!;
        public string Prno { get; set; } = null!;
        public string SupplierNo { get; set; } = null!;
        public string? TypesofPay { get; set; }
        public string? Terms { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public DateTime? DateNeeded { get; set; }
        public string? ReferenceNo { get; set; }
        public decimal? Total { get; set; }
        public decimal? DisPercent { get; set; }
        public decimal? DisTotal { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Vat { get; set; }
        public string PrepBy { get; set; } = null!;
        public string ApprBy { get; set; } = null!;
        public string? Remarks { get; set; }
        public string? Comments { get; set; }
        public string? TermsAndCondition { get; set; }
        public string? FooterText { get; set; }
        public string? Recuring { get; set; }
        public string? RrrecStatus { get; set; }
        public string? PreturnRecStatus { get; set; }
        public string? RecStatus { get; set; }
        public int BranchId { get; set; }

        public BranchModel Branch { get; set; } = null!;
        public EmployeeModel ApprByNavigation { get; set; } = null!;
        public EmployeeModel PrepByNavigation { get; set; } = null!;
        public PurchaseRequisitionMasterModel PrnoNavigation { get; set; } = null!;
        public SupplierModel SupNoNavigation { get; set; } = null!;
        public ICollection<PurchaseOrderDetailModel>? PurchaseOrderDetailFile { get; set; }
    }
}
