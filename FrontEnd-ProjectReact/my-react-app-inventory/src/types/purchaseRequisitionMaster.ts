import type { Employee } from "./employee";
import type { PurchaseRequisitionDetail } from "./purchaseRequisitionDetail";

export interface PurchaseRequisitionMaster {
  id: number;
  prmno: string;
  dateRequest?: Date;
  dateNeeded?: Date;
  preparedBy: string;
  apprBy: string;
  remarks?: string;
  comments?: string;
  termsAndCondition?: string;
  footerText?: string;
  recuring?: string;
  recStatus?: string;

  apprByNavigation: Employee;
  preparedByNavigation: Employee;
  purchaseRequisitionDetailFile?: PurchaseRequisitionDetail[];
}