import type { ItemDetail } from "./itemDetail";
import type { PurchaseReturnMaster } from "./purchaseReturnMaster";

export interface PurchaseReturnDetail {
  id: number;
  pretDno: string;
  itemDetailCode: string;
  qtyRet?: number;
  uprice?: number;
  amount?: number;
  recStatus?: string;

  itemDetailCodeNavigation: ItemDetail;
  pretDnoNavigation: PurchaseReturnMaster;
}