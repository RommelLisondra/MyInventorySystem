using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class ApprovalHistory
    {
        public int Id { get; set; }
        public string ModuleName { get; set; } = null!;
        public string RefNo { get; set; } = null!;
        public string? ApprovedBy { get; set; }
        public DateTime? DateApproved { get; set; }
        public string? Remarks { get; set; }
        public string? RecStatus { get; set; }

        public virtual Employee? ApprovedByNavigation { get; set; }
    }
}
