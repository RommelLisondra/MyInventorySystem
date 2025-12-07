using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ApplicationService.DTOs
{
    public class ItemDetailDto
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

        public ItemDto Item { get; set; } = null!;
        public ItemImageDto? ItemImage { get; set; }
        public ICollection<DeliveryReceiptDetailDto>? DeliveryReceiptDetailFile { get; set; }
        //public ICollection<InventoryAdjustment> InventoryAdjustment { get; set; }
        //public ICollection<InventoryTransfer> InventoryTransfer { get; set; }
        public ICollection<ItemInventoryDto>? ItemInventory { get; set; }
        public ICollection<ItemSupplierDto>? ItemSupplier { get; set; }
        public ICollection<ItemUnitMeasureDto>? ItemUnitMeasure { get; set; }
        public ICollection<PurchaseOrderDetailDto>? PurchaseOrderDetail { get; set; }
        public ICollection<PurchaseRequisitionDetailDto>? PurchaseRequisitionDetail { get; set; }
        public ICollection<PurchaseReturnDetailDto>? PurchaseReturnDetail { get; set; }
        public ICollection<ReceivingReportDetailDto>? ReceivingReportDetail { get; set; }
        public ICollection<SalesInvoiceDetailDto>? SalesInvoiceDetail { get; set; }
        public ICollection<SalesOrderDetailDto>? SalesOrderDetail { get; set; }
        public ICollection<SalesReturnDetailDto>? SalesReturnDetail { get; set; }
        //public ICollection<StockCountDetail> StockCountDetail { get; set; }
    }
}
