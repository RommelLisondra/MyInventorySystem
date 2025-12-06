using Sample.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.DTOs
{
    public class CostingHistoryDto
    {
        public virtual int id { get; set; }               // Primary key
        public virtual int ItemId { get; set; }           // FK → ItemMaster / ItemDetail
        public virtual decimal Cost { get; set; }         // Cost at this point in time
        public virtual DateTime EffectiveDate { get; set; } = DateTime.Now;
        public virtual string? Remarks { get; set; }
        public virtual DateTime CreatedDateTime { get; set; } = DateTime.Now;

        // Navigation
        public virtual int BranchId { get; set; }
        public virtual BranchDto Branch { get; set; } = null!;
        public virtual ItemDto Item { get; set; } = null!; 
    }
}
