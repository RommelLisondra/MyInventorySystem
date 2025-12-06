import type { DeliveryReceiptMaster } from "./deliveryReceiptMaster";
import type { ItemDetail } from "./itemDetail";

export interface DeliveryReceiptDetail {
  id: number;
  drdno: string;
  itemDetailCode: string;
  qtyDel?: number;
  uprice?: number;
  amount?: number;
  recStatus?: string;

  deliveryReceiptNavigation: DeliveryReceiptMaster;
  itemDetailNavigation: ItemDetail;
}