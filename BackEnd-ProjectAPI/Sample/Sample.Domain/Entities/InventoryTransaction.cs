using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.Domain.Entities
{ 
    public class InventoryTransaction : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual long TransactionId { get; set; }
        public virtual DateTime TransactionDate { get; set; }
        public virtual int CompanyId { get; set; }
        public virtual int WarehouseId { get; set; }
        public virtual string ItemDetailNo { get; set; } = null!;
        public virtual string? RefModule { get; set; }
        public virtual string? RefNo { get; set; }
        public virtual long? RefId { get; set; }
        public virtual int Quantity { get; set; }
        public virtual decimal UnitCost { get; set; }
        public virtual string MovementType { get; set; } = null!;
        public virtual string? Remarks { get; set; }
        public virtual string CreatedBy { get; set; } = null!;
        public virtual DateTime CreatedDate { get; set; }
        public virtual string? RecStatus { get; set; }

        public int BranchId { get; set; }
        public int? AccountId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public virtual Company? Company { get; set; }
        public virtual Warehouse? Warehouse { get; set; }

        public virtual ItemDetail? ItemDetail { get; set; }

        public static InventoryTransaction Create(InventoryTransaction inventory, string createdBy)
        {
            //Place your Business logic here
            inventory.Created_by = createdBy;
            inventory.Date_created = DateTime.Now;
            return inventory;
        }

        public static InventoryTransaction Update(InventoryTransaction inventory, string editedBy)
        {
            //Place your Business logic here
            inventory.Edited_by = editedBy;
            inventory.Date_edited = DateTime.Now;
            return inventory;
        }
    }
}
