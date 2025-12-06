using Sample.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebAPI.Model
{
    public class ItemDetailModel
    {
        public int id { get; set; }
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

        public ItemModel? Item { get; set; }
        public ItemImageModel? ItemImage { get; set; }
        public ICollection<DeliveryReceiptDetailModel>? DeliveryReceiptDetailFile { get; set; }
        //public ICollection<InventoryAdjustment> InventoryAdjustment { get; set; }
        //public ICollection<InventoryTransfer> InventoryTransfer { get; set; }
        public ICollection<ItemInventoryModel>? ItemInventory { get; set; }
        public ICollection<ItemSupplierModel>? ItemSupplier { get; set; }
        public ICollection<ItemUnitMeasureModel>? ItemUnitMeasure { get; set; }
        public ICollection<PurchaseOrderDetailModel>? PurchaseOrderDetail { get; set; }
        public ICollection<PurchaseRequisitionDetailModel>? PurchaseRequisitionDetail { get; set; }
        public ICollection<PurchaseReturnDetailModel>? PurchaseReturnDetail { get; set; }
        public ICollection<ReceivingReportDetailModel>? ReceivingReportDetail { get; set; }
        public ICollection<SalesInvoiceDetailModel>? SalesInvoiceDetail { get; set; }
        public ICollection<SalesOrderDetailModel>? SalesOrderDetail { get; set; }
        public ICollection<SalesReturnDetailModel>? SalesReturnDetail { get; set; }
        public ICollection<StockCountDetailModel>? StockCountDetail { get; set; }
    }
}
