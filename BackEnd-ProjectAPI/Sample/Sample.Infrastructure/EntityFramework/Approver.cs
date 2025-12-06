using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class Approver
    {
        public int Id { get; set; }
        public string EmpIdno { get; set; } = null!;
        public int? ApprovalLevel { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? RecStatus { get; set; }

        public virtual Employee EmpIdnoNavigation { get; set; } = null!;
    }
}
