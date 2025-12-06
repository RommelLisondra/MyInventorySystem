using Sample.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.DTOs
{
    public class HolidayDto
    {
        public int id { get; set; }                     // Primary key
        public string HolidayName { get; set; } = null!; // Name of the holiday (e.g., Christmas)
        public DateTime HolidayDate { get; set; }        // The date of the holiday
        public int? CompanyId { get; set; }             // Optional: if holiday is company-specific
        public int? BranchId { get; set; }              // Optional: if holiday is branch-specific
        public bool IsRecurring { get; set; } = true;   // True if the holiday occurs every year
        public string? Description { get; set; }        // Optional notes
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime? ModifiedDateTime { get; set; }

        // Navigation properties
        public CompanyDto? Company { get; set; }
        public BranchDto? Branch { get; set; }
    }
}
