using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.Domain.Entities
{
    public class InventoryBalance : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string ItemDetailNo { get; set; } = null!;
        public virtual int WarehouseId { get; set; }
        public virtual int QuantityOnHand { get; set; }
        public virtual DateTime LastUpdated { get; set; }
        public virtual string? RecStatus { get; set; }

        public static InventoryBalance Create(InventoryBalance inventory, string createdBy)
        {
            //Place your Business logic here
            inventory.Created_by = createdBy;
            inventory.Date_created = DateTime.Now;
            return inventory;
        }

        public static InventoryBalance Update(InventoryBalance inventory, string editedBy)
        {
            //Place your Business logic here
            inventory.Edited_by = editedBy;
            inventory.Date_edited = DateTime.Now;
            return inventory;
        }
    }
}
