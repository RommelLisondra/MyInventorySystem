import type { ItemDetail } from "./itemDetail";
import type { SalesInvoiceMaster } from "./salesInvoiceMaster";

export interface SalesInvoiceDetail {
  id: number;
  sidno: string;
  itemDetailCode: string;
  qtyInv?: number;
  qtyRet?: number;
  qtyDel?: number;
  uprice?: number;
  amount?: number;
  srrecStatus?: string;
  drrecStatus?: string;
  recStatus?: string;

  itemDetailCodeNavigation: ItemDetail;
  sidnoNavigation: SalesInvoiceMaster;
}