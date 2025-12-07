using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            ItemInventory = new HashSet<ItemInventory>();
            Location = new HashSet<Location>();
            StockCountMaster = new HashSet<StockCountMaster>();
            ItemWarehouseMappings = new HashSet<ItemWarehouseMapping>();
        }

        public int Id { get; set; }
        public string WarehouseCode { get; set; } = null!;
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Remarks { get; set; }
        public string? RecStatus { get; set; }
        // FK → Branch
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public virtual ICollection<ItemInventory> ItemInventory { get; set; }
        public virtual ICollection<Location> Location { get; set; }
        public virtual ICollection<StockCountMaster> StockCountMaster { get; set; }
        public virtual ICollection<ItemWarehouseMapping> ItemWarehouseMappings { get; set; }
    }
}
