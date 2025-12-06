import type { DeliveryReceiptMaster } from "./deliveryReceiptMaster";
import type { OfficialReceiptMaster } from "./officialReceiptMaster";
import type { SalesInvoiceMaster } from "./salesInvoiceMaster";
import type { SalesOrderMaster } from "./salesOrderMaster";
import type { SalesReturnMaster } from "./salesReturnMaster";

export interface Customer {
  id: number;
  custNo?: string;
  name: string;
  address1?: string;
  address2?: string;
  address3?: string;
  city?: string;
  postalCode?: string;
  country?: string;
  state?: string;
  emailAddress?: string;
  fax?: string;
  mobileNo?: string;
  accountNo?: string;
  creditCardNo?: string;
  creditCardType?: string;
  creditCardName?: string;
  creditCardExpiry?: string;
  contactNo?: string;
  contactPerson?: string;
  creditLimit?: number;
  balance?: number;
  lastSono?: string;
  lastSino?: string;
  lastDrno?: string;
  lastOr?: string;
  lastSrno?: string;
  recStatus?: string;

  deliveryReceiptMaster?: DeliveryReceiptMaster[] | null;
  officialReceiptMaster?: OfficialReceiptMaster[] | null;
  salesInvoiceMaster?: SalesInvoiceMaster[] | null;
  salesOrderMaster?: SalesOrderMaster[] | null;
  salesReturnMaster?: SalesReturnMaster[] | null;
}