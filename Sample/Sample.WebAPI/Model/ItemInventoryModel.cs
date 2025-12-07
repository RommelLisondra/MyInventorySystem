using Sample.ApplicationService.DTOs;
using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.WebAPI.Model
{
    public class ItemInventoryModel 
    {
        public int id { get; set; }
        public string ItemDetailCode { get; set; } = null!;
        public string WarehouseCode { get; set; } = null!;
        public string LocationCode { get; set; } = null!;
        public int? QtyOnHand { get; set; }
        public int? QtyOnOrder { get; set; }
        public int? QtyReserved { get; set; }
        public int? ExtendedQtyOnHand { get; set; }
        public int? SalesReturnItemQty { get; set; }
        public int? PurchaseReturnItemQty { get; set; }
        public DateTime LastStockCount { get; set; }
        public string? RecStatus { get; set; }

        public ItemDetailModel ItemDtailCodeNavigation { get; set; } = null!;
        public WarehouseModel WarehouseCodeNavigation { get; set; } = null!;
        public LocationModel LocationCodeNavigation { get; set; } = null!;
    }
}
