using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.Domain.Entities
{
    public class InventoryAdjustment : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual long AdjustmentId { get; set; }
        public virtual string AdjustmentNo { get; set; } = null!;
        public virtual DateTime AdjustmentDate { get; set; }
        public virtual int CompanyId { get; set; }
        public virtual int WarehouseId { get; set; }
        public virtual string? Remarks { get; set; }
        public virtual string CreatedBy { get; set; } = null!;
        public virtual DateTime CreatedDate { get; set; }
        public virtual string? RecStatus { get; set; }
        public virtual int BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;

        public virtual ICollection<InventoryAdjustmentDetail>? InventoryAdjustmentDetail { get; set; }

        public static InventoryAdjustment Create(InventoryAdjustment inventory, string createdBy)
        {
            //Place your Business logic here
            inventory.Created_by = createdBy;
            inventory.Date_created = DateTime.Now;
            return inventory;
        }

        public static InventoryAdjustment Update(InventoryAdjustment inventory, string editedBy)
        {
            //Place your Business logic here
            inventory.Edited_by = editedBy;
            inventory.Date_edited = DateTime.Now;
            return inventory;
        }

        public void AddDetail(InventoryAdjustmentDetail detail)
        {
            if (detail == null) throw new ArgumentNullException(nameof(detail));

            InventoryAdjustmentDetail ??= new List<InventoryAdjustmentDetail>();

            InventoryAdjustmentDetail.Add(detail);
        }
    }
}
