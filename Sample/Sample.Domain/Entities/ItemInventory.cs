using Sample.Domain.Domain;
using System;
using System.Collections.Generic;

namespace Sample.Domain.Entities
{
    public class ItemInventory : EntityBase, IAggregateRoot
    {
        public int id { get; set; }
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

        public virtual ItemDetail? ItemDetailCodeNavigation { get; set; }
        public virtual Warehouse? WarehouseCodeNavigation { get; set; }
        public virtual Location? LocationCodeNavigation { get; set; }

        public static ItemInventory Create(ItemInventory itemInventory, string createdBy)
        {
            //Place your Business logic here
            itemInventory.Created_by = createdBy;
            itemInventory.Date_created = DateTime.Now;
            return itemInventory;
        }

        public static ItemInventory Update(ItemInventory itemInventory, string editedBy)
        {
            //Place your Business logic here
            itemInventory.Edited_by = editedBy;
            itemInventory.Date_edited = DateTime.Now;
            return itemInventory;
        }

        public void DecreaseOnHand(int qty, string sinNo)
        {
            if (qty < 0) throw new ArgumentException(nameof(qty));
            if (QtyOnHand - qty < 0) throw new InvalidOperationException("Insufficient stock");
            QtyOnHand -= qty;
        }
        public void IncreaseOnHand(int qty, string sinNo)
        {
            if (qty < 0) throw new ArgumentException(nameof(qty));
            if (QtyOnHand - qty < 0) throw new InvalidOperationException("Insufficient stock");
            QtyOnHand -= qty;
        }

        public void IncreaseQuatityOnOrder(int qty)
        {
            if (qty < 0) throw new ArgumentException(nameof(qty));
            if (QtyOnOrder - qty < 0) throw new InvalidOperationException("Insufficient stock");
            QtyOnOrder += qty;
        }

        public void DecreaseQuatityOnOrder(int qty)
        {
            if (qty < 0) throw new ArgumentException(nameof(qty));
            if (QtyOnOrder - qty < 0) throw new InvalidOperationException("Insufficient stock");
            QtyOnOrder -= qty;
            QtyOnHand += qty;
        }

        public void PurchaseReturnQuantity(int qty)
        {
            if (qty < 0) throw new ArgumentException(nameof(qty));
            if (QtyOnOrder - qty < 0) throw new InvalidOperationException("Insufficient stock");
            PurchaseReturnItemQty += qty;
        }
        public void SalesReturnQuantity(int qty)
        {
            if (qty < 0) throw new ArgumentException(nameof(qty));
            if (QtyOnOrder - qty < 0) throw new InvalidOperationException("Insufficient stock");
            SalesReturnItemQty += qty;
        }
    }
}
