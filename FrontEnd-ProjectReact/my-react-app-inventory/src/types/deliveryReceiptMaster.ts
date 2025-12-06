import type { DeliveryReceiptDetail } from "./deliveryReceiptDetail";

export interface DeliveryReceiptMaster {
  id: number;
  drmno: string;
  custNo?: string;
  simno: string;
  date?: Date;
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
  deliveryCost?: number;
  subTotal?: number;
  vat?: number;
  prepBy?: string;
  apprBy?: string;
  recStatus?: string;
  
  deliveryReceiptDetail?: DeliveryReceiptDetail[];
}
