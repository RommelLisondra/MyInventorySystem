using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class InventoryBalance
    {
        public int Id { get; set; }
        public string ItemDetailNo { get; set; } = null!;
        public int WarehouseId { get; set; }
        public int QuantityOnHand { get; set; }
        public DateTime LastUpdated { get; set; }
        public string? RecStatus { get; set; }
    }
}
