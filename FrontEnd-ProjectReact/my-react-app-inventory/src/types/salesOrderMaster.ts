import type { Employee } from "./employee";
import type { Customer } from "./customer";
import type { SalesOrderDetail } from "./salesOrderDetail";

export interface SalesOrderMaster {
  id: number;
  somno: string;
  date?: Date;
  custNo?: string;
  terms?: string;
  typesOfPay?: string;
  remarks?: string;
  comments?: string;
  termsAndCondition?: string;
  footerText?: string;
  recuring?: string;
  disPercent?: number;
  disTotal?: number;
  subTotal?: number;
  vat?: number;
  total?: number;
  prepBy: string;
  apprBy: string;
  recStatus?: string;

  apprByNavigation?: Employee;
  custNoNavigation?: Customer;
  prepByNavigation?: Employee;
  salesOrderDetail?: SalesOrderDetail[];
}