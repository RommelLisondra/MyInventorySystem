import type { ItemDetail } from "./itemDetail";
import type { PurchaseOrderMaster } from "./purchaseOrderMaster";

export interface PurchaseOrderDetail {
  id: number;
  podno: string;
  itemDetailCode: string;
  qtyOrder?: number;
  qtyReceived?: number;
  uprice?: number;
  amount?: number;
  rrrecStatus?: string;
  recStatus?: string;

  itemDetailCodeNavigation: ItemDetail;
  podnoNavigation: PurchaseOrderMaster;
}