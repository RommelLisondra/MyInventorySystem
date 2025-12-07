using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.WebAPI.Model
{
    public class InventoryAdjustmentDetailModel 
    {
        public int id { get; set; }
        public long AdjustmentId { get; set; }
        public string ItemDetailNo { get; set; } = null!;
        public decimal Quantity { get; set; }
        public string MovementType { get; set; } = null!;
        public string? RecStatus { get; set; }

        public InventoryAdjustmentModel Adjustment { get; set; } = null!;
    }
}
