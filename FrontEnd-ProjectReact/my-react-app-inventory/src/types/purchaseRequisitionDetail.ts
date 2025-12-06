import type { ItemDetail } from "./itemDetail";
import type { PurchaseRequisitionMaster } from "./purchaseRequisitionMaster";

export interface PurchaseRequisitionDetail {
  id: number;
  prdno: string;
  itemDetailCode: string;
  qtyReq?: number;
  qtyOrder?: number;
  recStatus?: string;

  itemDetailCodeNavigation: ItemDetail;
  prdnoNavigation: PurchaseRequisitionMaster;
}