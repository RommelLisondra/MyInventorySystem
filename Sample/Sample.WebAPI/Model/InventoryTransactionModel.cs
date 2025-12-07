using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.WebAPI.Model
{ 
    public class InventoryTransactionModel 
    {
        public int id { get; set; }
        public DateTime TransactionDate { get; set; }
        public int CompanyId { get; set; }
        public int WarehouseId { get; set; }
        public string ItemDetailNo { get; set; } = null!;
        public string? RefModule { get; set; }
        public string? RefNo { get; set; }
        public long? RefId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public string MovementType { get; set; } = null!;
        public string? Remarks { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? RecStatus { get; set; }
        public int BranchId { get; set; }
        public int AccountId { get; set; }

        public BranchModel Branch { get; set; } = null!;
        public AccountModel? Account { get; set; }
        public CompanyModel? Company { get; set; }

        public WarehouseModel? Warehouse { get; set; }

        public ItemDetailModel? ItemDetail { get; set; }
    }
}
