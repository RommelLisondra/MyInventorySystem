using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.ApplicationService.DTOs
{
    public class InventoryBalanceDto 
    {
        public int id { get; set; }
        public string ItemDetailNo { get; set; } = null!;
        public int WarehouseId { get; set; }
        public decimal QuantityOnHand { get; set; }
        public DateTime LastUpdated { get; set; }
        public string? RecStatus { get; set; }
    }
}
