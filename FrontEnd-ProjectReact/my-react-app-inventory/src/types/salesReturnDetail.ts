import type { ItemDetail } from "./itemDetail";
import type { SalesReturnMaster } from "./salesReturnMaster";

export interface SalesReturnDetail {
  id: number;
  srdno: string;
  itemDetailCode: string;
  qtyRet?: number;
  uprice?: number;
  amount?: number;
  recStatus?: string;

  itemDetailCodeNavigation: ItemDetail;
  srdnoNavigation: SalesReturnMaster;
}