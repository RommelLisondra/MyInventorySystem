using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.DTOs
{
    public class CompanyDto 
    {
        public virtual int id { get; set; }
        public virtual string CompanyCode { get; set; } = null!;
        public virtual string CompanyName { get; set; } = null!;
        public virtual string? Address { get; set; }
        public virtual string? ContactNo { get; set; }
        public virtual string? Email { get; set; }
        public virtual DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public virtual DateTime? ModifiedDateTime { get; set; }
        public virtual bool IsActive { get; set; } = true;

        // Navigation property
        public virtual ICollection<BranchDto> Branches { get; set; } = new List<BranchDto>();
    }
}
