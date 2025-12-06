import type { Employee } from "./employee";
import type { Customer } from "./customer";
import type { SalesInvoiceMaster } from "./salesInvoiceMaster";
import type { SalesReturnDetail } from "./salesReturnDetail";

export interface SalesReturnMaster {
  id: number;
  srmno: string;
  custNo?: string;
  simno: string;
  date?: string; // ISO date string
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
  balance?: number;
  subTotal?: number;
  vat?: number;
  dueDate?: string;
  prepBy?: string;
  apprBy?: string;
  recStatus?: string;

  apprByNavigation?: Employee;
  custNoNavigation?: Customer;
  prepByNavigation?: Employee;
  simnoNavigation: SalesInvoiceMaster;
  salesReturnDetailFile?: SalesReturnDetail[];
}