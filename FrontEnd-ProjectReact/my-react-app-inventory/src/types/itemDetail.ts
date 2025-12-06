import type { ItemImage } from "./itemImage";
import type { Item } from "./item";
import type { DeliveryReceiptDetail } from "./deliveryReceiptDetail";
import type { ItemInventory } from "./itemInventory";
import type { ItemSupplier } from "./itemSupplier";
import type { ItemUnitMeasure } from "./itemUnitMeasure";
import type { PurchaseRequisitionDetail } from "./purchaseRequisitionDetail";
import type { PurchaseOrderDetail } from "./purchaseOrderDetail";
import type { PurchaseReturnDetail } from "./purchaseReturnDetail";
import type { ReceivingReportDetail } from "./receivingReportDetail";
import type { SalesInvoiceDetail } from "./salesInvoiceDetail";
import type { SalesOrderDetail } from "./salesOrderDetail";
import type { SalesReturnDetail } from "./salesReturnDetail";
import type { StockCountDetail } from "./stockCountDetail";


export interface ItemDetail {
  id: number;
  ItemDetailCode: string;
  ItemId: string; // Note: If it's an int in C#, change to number
  ItemDetailNo: string;
  Barcode?: string;
  SerialNo?: string;
  PartNo?: string;
  WarehouseCode: string;
  LocationCode: string;
  Volume?: string;
  Size?: string;
  Weight?: string;
  ExpiryDate?: string;
  UnitMeasure?: string;
  Unitprice?: number;
  Warranty?: string;
  ModifiedDateTime: string;
  CreatedDateTime: string;
  Eoq?: number;
  Rop?: number;
  RecStatus?: string;

  Item: Item | null;
  ItemImage: ItemImage | null;
  DeliveryReceiptDetailFile?: DeliveryReceiptDetail[] | null;
  ItemInventory?: ItemInventory[] | null;
  ItemSupplier?: ItemSupplier[] | null;
  ItemUnitMeasure?: ItemUnitMeasure[] | null;
  PurchaseOrderDetail?: PurchaseOrderDetail[] | null;
  PurchaseRequisitionDetail?: PurchaseRequisitionDetail[] | null;
  PurchaseReturnDetail?: PurchaseReturnDetail[] | null;
  ReceivingReportDetail?: ReceivingReportDetail[] | null;
  SalesInvoiceDetail?: SalesInvoiceDetail[] | null;
  SalesOrderDetail?: SalesOrderDetail[] | null;
  SalesReturnDetail?: SalesReturnDetail[] | null;
  StockCountDetail?: StockCountDetail[] | null;
}