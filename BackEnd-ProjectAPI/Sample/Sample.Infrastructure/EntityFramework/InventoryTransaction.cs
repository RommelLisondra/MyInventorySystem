using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class InventoryTransaction
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public int CompanyId { get; set; }
        public int WarehouseId { get; set; }
        public string ItemDetailNo { get; set; } = null!;
        public string? RefModule { get; set; }
        public string? RefNo { get; set; }
        public long? RefId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public string MovementType { get; set; } = null!;
        public string? Remarks { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? RecStatus { get; set; }
        public int BranchId { get; set; }
        public int? AccountId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public virtual Company? Company { get; set; }

        public virtual Warehouse? Warehouse { get; set; }

        public virtual ItemDetail? ItemDetail { get; set; }
    }
}
