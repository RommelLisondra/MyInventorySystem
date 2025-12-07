using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.ApplicationService.DTOs
{
    public class ApprovalHistoryDto 
    {
        public int id { get; set; }
        public string ModuleName { get; set; } = null!;
        public string RefNo { get; set; } = null!;
        public string? ApprovedBy { get; set; }
        public DateTime? DateApproved { get; set; }
        public string? Remarks { get; set; }
        public string? RecStatus { get; set; }

        public EmployeeDto? ApprovedByNavigation { get; set; }
    }
}
