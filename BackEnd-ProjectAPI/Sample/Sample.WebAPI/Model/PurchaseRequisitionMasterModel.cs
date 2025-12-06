using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class PurchaseRequisitionMasterModel
    {
        public int id { get; set; }
        public string Prmno { get; set; } = null!;
        public DateTime? DateRequest { get; set; }
        public DateTime? DateNeeded { get; set; }
        public string PreparedBy { get; set; } = null!;
        public string ApprBy { get; set; } = null!;
        public string? Remarks { get; set; }
        public string? Comments { get; set; }
        public string? TermsAndCondition { get; set; }
        public string? FooterText { get; set; }
        public string? Recuring { get; set; }
        public string? RecStatus { get; set; }
        public int BranchId { get; set; }

        public BranchModel Branch { get; set; } = null!;
        public EmployeeModel ApprByNavigation { get; set; } = null!;
        public EmployeeModel PreparedByNavigation { get; set; } = null!;
        public ICollection<PurchaseRequisitionDetailModel>? PurchaseRequisitionDetailFile { get; set; }
    }
}
