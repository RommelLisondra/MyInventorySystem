import type { ItemDetail } from "./itemDetail";
import type { ReceivingReportMaster } from "./receivingReportMaster";

export interface ReceivingReportDetail {
  id: number;
  rrdno: string;
  itemDetailCode: string;
  qtyReceived?: number;
  qtyRet?: number;
  uprice?: number;
  amount?: number;
  pretrunRecStatus?: string;
  recStatus?: string;

  itemDetailCodeNavigation: ItemDetail;
  rrdnoNavigation: ReceivingReportMaster;
}