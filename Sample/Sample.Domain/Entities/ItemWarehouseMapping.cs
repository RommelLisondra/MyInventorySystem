using Sample.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Entities
{
    public class ItemWarehouseMapping : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }

        public virtual int ItemId { get; set; }           // FK → ItemMaster / ItemDetail
        public virtual int WarehouseId { get; set; }      // FK → Warehouse
        public virtual int? BranchId { get; set; }        // Optional, can derive from Warehouse

        public virtual bool IsActive { get; set; } = true;
        public virtual DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public virtual DateTime? ModifiedDateTime { get; set; }

        // Navigation properties
        public virtual Item Item { get; set; } = null!;
        public virtual Warehouse Warehouse { get; set; } = null!;
        public virtual Branch? Branch { get; set; }
        public static ItemWarehouseMapping Create(ItemWarehouseMapping warehouseMapping, string createdBy)
        {
            //Place your Business logic here
            warehouseMapping.Created_by = createdBy;
            warehouseMapping.Date_created = DateTime.Now;
            return warehouseMapping;
        }

        public static ItemWarehouseMapping Update(ItemWarehouseMapping warehouseMapping, string editedBy)
        {
            //Place your Business logic here
            warehouseMapping.Edited_by = editedBy;
            warehouseMapping.Date_edited = DateTime.Now;
            return warehouseMapping;
        }
    }
}
