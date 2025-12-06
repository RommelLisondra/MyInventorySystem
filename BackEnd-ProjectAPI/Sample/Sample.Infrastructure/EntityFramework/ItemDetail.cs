using Sample.Domain.Entities;
using Sample.Infrastructure.Mapper;
using System;
using System.Collections.Generic;

namespace Sample.Infrastructure.EntityFramework
{
    public partial class ItemDetail
    {
        public ItemDetail()
        {
            DeliveryReceiptDetailFile = new HashSet<DeliveryReceiptDetailFile>();
            ItemInventory = new HashSet<ItemInventory>();
            ItemSupplier = new HashSet<ItemSupplier>();
            ItemUnitMeasure = new HashSet<ItemUnitMeasure>();
            PurchaseOrderDetailFile = new HashSet<PurchaseOrderDetailFile>();
            PurchaseRequisitionDetailFile = new HashSet<PurchaseRequisitionDetailFile>();
            PurchaseReturnDetailFile = new HashSet<PurchaseReturnDetailFile>();
            ReceivingReportDetailFile = new HashSet<ReceivingReportDetailFile>();
            SalesInvoiceDetailFile = new HashSet<SalesInvoiceDetailFile>();
            SalesOrderDetailFile = new HashSet<SalesOrderDetailFile>();
            SalesReturnDetailFile = new HashSet<SalesReturnDetailFile>();
            StockCountDetail = new HashSet<StockCountDetail>();
        }

        public int Id { get; set; }
        public string ItemDetailCode { get; set; } = null!;
        public string ItemId { get; set; } = null!;
        public string ItemDetailNo { get; set; } = null!;
        public string? Barcode { get; set; }
        public string? SerialNo { get; set; }
        public string? PartNo { get; set; }
        public string WarehouseCode { get; set; } = null!;
        public string LocationCode { get; set; } = null!;
        public string? Volume { get; set; }
        public string? Size { get; set; }
        public string? Weight { get; set; }
        public string? ExpiryDate { get; set; }
        public string? UnitMeasure { get; set; }
        public decimal? Unitprice { get; set; }
        public string? Warranty { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? Eoq { get; set; }
        public int? Rop { get; set; }
        public string? RecStatus { get; set; }

        public virtual Item? ItemMaster { get; set; }
        public virtual ItemImage? ItemImage { get; set; }
        public virtual ICollection<DeliveryReceiptDetailFile> DeliveryReceiptDetailFile { get; set; }
        public virtual ICollection<ItemInventory> ItemInventory { get; set; }
        public virtual ICollection<ItemSupplier> ItemSupplier { get; set; }
        public virtual ICollection<ItemUnitMeasure> ItemUnitMeasure { get; set; }
        public virtual ICollection<PurchaseOrderDetailFile> PurchaseOrderDetailFile { get; set; }
        public virtual ICollection<PurchaseRequisitionDetailFile> PurchaseRequisitionDetailFile { get; set; }
        public virtual ICollection<PurchaseReturnDetailFile> PurchaseReturnDetailFile { get; set; }
        public virtual ICollection<ReceivingReportDetailFile> ReceivingReportDetailFile { get; set; }
        public virtual ICollection<SalesInvoiceDetailFile> SalesInvoiceDetailFile { get; set; }
        public virtual ICollection<SalesOrderDetailFile> SalesOrderDetailFile { get; set; }
        public virtual ICollection<SalesReturnDetailFile> SalesReturnDetailFile { get; set; }
        public virtual ICollection<StockCountDetail> StockCountDetail { get; set; }
    }
}
