using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.DTOs
{
    public class PurchaseRequisitionMasterDto
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

        public BranchDto Branch { get; set; } = null!;
        public EmployeeDto ApprByNavigation { get; set; } = null!;
        public EmployeeDto PreparedByNavigation { get; set; } = null!;
        public ICollection<PurchaseRequisitionDetailDto>? PurchaseRequisitionDetailFile { get; set; }
    }
}
