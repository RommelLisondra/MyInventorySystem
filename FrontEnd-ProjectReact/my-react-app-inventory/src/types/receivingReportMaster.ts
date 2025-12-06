import type { Employee } from "./employee";
import type { PurchaseOrderMaster } from "./purchaseOrderMaster";
import type { PurchaseOrderDetail } from "./purchaseOrderDetail";
import type { Supplier } from "./supplier";
import type { ReceivingReportDetail } from "./receivingReportDetail";

export interface ReceivingReportMaster {
  id: number;
  rrmno: string;
  date?: Date;
  time?: string;
  supNo: string;
  refNo?: string;
  pono: string;
  terms?: string;
  typesOfPay?: string;
  remarks?: string;
  comments?: string;
  termsAndCondition?: string;
  footerText?: string;
  recuring?: string;
  total?: number;
  disPercent?: number;
  disTotal?: number;
  subTotal?: number;
  vat?: number;
  receivedBy: string;
  preturnRecStatus?: string;
  recStatus?: string;

  ponoNavigation: PurchaseOrderMaster;
  receivedByNavigation: Employee;
  supNoNavigation: Supplier;
  purchaseReturnMasterFile?: PurchaseOrderDetail;
  receivingReportDetailFile?: ReceivingReportDetail[];
}