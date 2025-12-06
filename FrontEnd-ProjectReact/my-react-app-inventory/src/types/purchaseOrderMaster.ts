import type { Employee } from "./employee";
import type { Supplier } from "./supplier";
import type { PurchaseOrderDetail } from "./purchaseOrderDetail";
import type { PurchaseRequisitionMaster } from "./purchaseRequisitionMaster";

export interface PurchaseOrderMaster {
  id: number;
  pomno: string;
  prno: string;
  supplierNo: string;
  typesofPay?: string;
  terms?: string;
  date?: Date;
  time?: Date;
  dateNeeded?: Date;
  referenceNo?: string;
  total?: number;
  disPercent?: number;
  disTotal?: number;
  subTotal?: number;
  vat?: number;
  prepBy: string;
  apprBy: string;
  remarks?: string;
  comments?: string;
  termsAndCondition?: string;
  footerText?: string;
  recuring?: string;
  rrrecStatus?: string;
  preturnRecStatus?: string;
  recStatus?: string;

  apprByNavigation: Employee;
  prepByNavigation: Employee;
  prnoNavigation: PurchaseRequisitionMaster;
  supNoNavigation: Supplier;
  purchaseOrderDetailFile?: PurchaseOrderDetail[];
}