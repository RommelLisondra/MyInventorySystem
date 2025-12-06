import type { ItemDetail } from "./itemDetail";
import type { SalesOrderMaster } from "./salesOrderMaster";

export interface SalesOrderDetail {
  id: number;
  sodno: string;
  itemDetailCode: string;
  qtyOnOrder?: number;
  qtyInvoice?: number;
  uprice?: number;
  amount?: number;
  recStatus?: string;

  itemDetailCodeNavigation: ItemDetail;
  sodnoNavigation: SalesOrderMaster;
}