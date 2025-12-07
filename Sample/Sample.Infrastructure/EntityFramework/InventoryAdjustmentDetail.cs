using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class InventoryAdjustmentDetail
    {
        public int Id { get; set; }
        public int AdjustmentId { get; set; }
        public string ItemDetailNo { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Uprice { get; set; }
        public decimal Amount { get; set; }
        public string MovementType { get; set; } = null!;
        public string? RecStatus { get; set; }

        public virtual InventoryAdjustment Adjustment { get; set; } = null!;
    }
}
