using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.WebAPI.Model
{
    public class ApprovalFlowModel 
    {
        public int id { get; set; }
        public string ModuleName { get; set; } = null!;
        public int Level { get; set; }
        public int ApproverId { get; set; }
        public bool IsFinalLevel { get; set; }
        public string? RecStatus { get; set; }
    }
}
