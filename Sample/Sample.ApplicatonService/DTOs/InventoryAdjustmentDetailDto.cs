using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.ApplicationService.DTOs
{
    public class InventoryAdjustmentDetailDto 
    {
        public int id { get; set; }
        public long AdjustmentId { get; set; }
        public string ItemDetailNo { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Uprice { get; set; }
        public decimal Amount { get; set; }
        public string MovementType { get; set; } = null!;
        public string? RecStatus { get; set; }

        public InventoryAdjustmentDto Adjustment { get; set; } = null!;
    }
}
