using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class ApprovalFlow
    {
        public int Id { get; set; }
        public string ModuleName { get; set; } = null!;
        public int Level { get; set; }
        public int ApproverId { get; set; }
        public bool IsFinalLevel { get; set; }
        public string? RecStatus { get; set; }
    }
}
