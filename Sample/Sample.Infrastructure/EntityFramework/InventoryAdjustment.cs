using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class InventoryAdjustment
    {
        public InventoryAdjustment()
        {
            InventoryAdjustmentDetail = new HashSet<InventoryAdjustmentDetail>();
        }

        public int Id { get; set; }
        public string AdjustmentNo { get; set; } = null!;
        public DateTime AdjustmentDate { get; set; }
        public int CompanyId { get; set; }
        public int WarehouseId { get; set; }
        public int LocationId { get; set; }
        public string? Remarks { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? RecStatus { get; set; }
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;

        public virtual ICollection<InventoryAdjustmentDetail> InventoryAdjustmentDetail { get; set; }
    }
}
