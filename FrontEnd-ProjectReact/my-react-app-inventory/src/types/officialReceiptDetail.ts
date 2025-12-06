import type { OfficialReceiptMaster } from "./officialReceiptMaster";
import type { SalesInvoiceMaster } from "./salesInvoiceMaster";

export interface OfficialReceiptDetail {
  id: number;
  ordno: string;
  simno: string;
  amountPaid?: number;
  amountDue?: number;
  recStatus?: string;

  ordnoNavigation: OfficialReceiptMaster;
  salesInvoiceMasterFileNavigation: SalesInvoiceMaster;
}