import type { Employee } from "./employee";
import type { Customer } from "./customer";
import type { SalesOrderMaster } from "./salesOrderMaster";
import type { SalesReturnMaster } from "./salesReturnMaster";
import type { SalesInvoiceDetail } from "./salesInvoiceDetail";

export interface SalesInvoiceMaster {
  id: number;
  simno: string;
  somno: string;
  date?: Date;
  custNo: string;
  terms?: string;
  typesOfPay?: string;
  salesRef?: string;
  remarks?: string;
  comments?: string;
  termsAndCondition?: string;
  footerText?: string;
  recuring?: string;
  total?: number;
  disPercent?: number;
  disTotal?: number;
  deliveryCost?: number;
  balance?: number;
  subTotal?: number;
  vat?: number;
  dueDate?: Date;
  prepBy: string;
  apprBy: string;
  orrecStatus?: string;
  drrecStatus?: string;
  srrecStatus?: string;
  recStatus?: string;

  apprByNavigation: Employee;
  custNoNavigation?: Customer;
  prepByNavigation: Employee;
  somnoNavigation: SalesOrderMaster;
  salesReturnMasterFile?: SalesReturnMaster;
  salesInvoiceDetail?: SalesInvoiceDetail[];
}