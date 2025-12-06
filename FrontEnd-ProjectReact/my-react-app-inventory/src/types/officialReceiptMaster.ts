import type { Customer } from "./customer";
import type { Employee } from "./employee";
import type { OfficialReceiptDetail } from "./officialReceiptDetail";

export interface OfficialReceiptMaster {
  id: number;
  orno: string;
  custNo?: string;
  date?: Date;
  totalAmtPaid?: number;
  disPercent?: number;
  disTotal?: number;
  subTotal?: number;
  vat?: number;
  formPay?: string;
  cashAmt?: number;
  checkAmt?: number;
  checkNo?: string;
  bankName?: string;
  remarks?: string;
  comments?: string;
  termsAndCondition?: string;
  footerText?: string;
  recuring?: string;
  prepBy?: string;
  recStatus?: string;

  custNoNavigation?: Customer;
  prepByNavigation?: Employee;
  officialReceiptDetailFile?: OfficialReceiptDetail[];
}