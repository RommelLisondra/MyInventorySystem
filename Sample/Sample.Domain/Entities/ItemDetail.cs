using System;
using System.Collections.Generic;
using Sample.Domain.Domain;
using Sample.Domain.Entities;

namespace Sample.Domain.Entities
{
    public class ItemDetail : EntityBase, IAggregateRoot
    {
        public virtual int id { get; set; }
        public virtual string ItemDetailCode { get; set; } = null!;
        public virtual string ItemId { get; set; } = null!;
        public virtual string ItemDetailNo { get; set; } = null!;
        public virtual string? Barcode { get; set; }
        public virtual string? SerialNo { get; set; }
        public virtual string? PartNo { get; set; }
        public virtual string WarehouseCode { get; set; } = null!;
        public virtual string LocationCode { get; set; } = null!;
        public virtual string? Volume { get; set; }
        public virtual string? Size { get; set; }
        public virtual string? Weight { get; set; }
        public virtual string? ExpiryDate { get; set; }
        public virtual string? UnitMeasure { get; set; }
        public virtual decimal? Unitprice { get; set; }
        public virtual string? Warranty { get; set; }
        public virtual DateTime ModifiedDateTime { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual int? Eoq { get; set; }
        public virtual int? Rop { get; set; }
        public virtual string? RecStatus { get; set; }


        public virtual Item? ItemMaster { get; set; }
        public virtual ItemImage? ItemImage { get; set; }
        public virtual ICollection<DeliveryReceiptDetail>? DeliveryReceiptDetailFile { get; set; }
        //public virtual ICollection<InventoryAdjustment> InventoryAdjustment { get; set; }
        //public virtual ICollection<InventoryTransfer> InventoryTransfer { get; set; }
        public virtual ICollection<ItemInventory>? ItemInventory { get; set; }
        public virtual ICollection<ItemSupplier>? ItemSupplier { get; set; }
        public virtual ICollection<ItemUnitMeasure>? ItemUnitMeasure { get; set; }
        public virtual ICollection<PurchaseOrderDetail>? PurchaseOrderDetail { get; set; }
        public virtual ICollection<PurchaseRequisitionDetail>? PurchaseRequisitionDetail { get; set; }
        public virtual ICollection<PurchaseReturnDetail>? PurchaseReturnDetail { get; set; }
        public virtual ICollection<ReceivingReportDetail>? ReceivingReportDetail { get; set; }
        public virtual ICollection<SalesInvoiceDetail>? SalesInvoiceDetail { get; set; }
        public virtual ICollection<SalesOrderDetail>? SalesOrderDetail { get; set; }
        public virtual ICollection<SalesReturnDetail>? SalesReturnDetail { get; set; }
        //public virtual ICollection<StockCountDetail> StockCountDetail { get; set; }

        public static ItemDetail Create(ItemDetail itemDetail, string createdBy)
        {
            //Place your Business logic here
            itemDetail.Created_by = createdBy;
            itemDetail.Date_created = DateTime.Now;
            return itemDetail;
        }

        public static ItemDetail Update(ItemDetail itemDetail, string editedBy)
        {
            //Place your Business logic here
            itemDetail.Edited_by = editedBy;
            itemDetail.Date_edited = DateTime.Now;
            return itemDetail;
        }
    }
}
