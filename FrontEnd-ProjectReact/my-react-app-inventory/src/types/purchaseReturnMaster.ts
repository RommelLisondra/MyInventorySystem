import type { Employee } from "./employee";
import type { PurchaseReturnDetail } from "./purchaseReturnDetail";
import type { Supplier } from "./supplier";
import type { ReceivingReportMaster } from "./receivingReportMaster";

export interface PurchaseReturnMaster {
  id: number;
  pretMno: string;
  rrno: string;
  refNo?: string;
  supplierNo: string;
  date?: Date;
  time?: string;
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
  prepBy: string;
  apprBy: string;
  recStatus?: string;

  apprByNavigation: Employee;
  prepByNavigation: Employee;
  rrnoNavigation: ReceivingReportMaster;
  supNoNavigation: Supplier;
  purchaseReturnDetailFile?: PurchaseReturnDetail[];
}