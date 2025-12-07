using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class ItemInventory
    {
        public int Id { get; set; }
        public string ItemDetailCode { get; set; } = null!;
        public string WarehouseCode { get; set; } = null!;
        public string LocationCode { get; set; } = null!;
        public int QtyOnHand { get; set; }
        public int QtyOnOrder { get; set; }
        public int QtyReserved { get; set; }
        public int ExtendedQtyOnHand { get; set; }
        public int SalesReturnItemQty { get; set; }
        public int PurchaseReturnItemQty { get; set; }
        public DateTime LastStockCount { get; set; }
        public string? RecStatus { get; set; }

        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public virtual ItemDetail ItemDetailCodeNavigation { get; set; } = null!;
        public virtual Warehouse WarehouseCodeNavigation { get; set; } = null!;
        public virtual Location LocationCodeNavigation { get; set; } = null!;
    }
}
